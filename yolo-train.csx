#!/usr/bin/env dotnet-script
/* run this script using the command: dotnet script yolo-train.csx
   To use on another computer you need to install the .NET Core 3.1 SDK and the dotnet script global tool (command: dotnet tool install -g dotnet-script)
   See https://github.com/filipw/dotnet-script */
   
#r "LabellingDB.dll"
#r "nuget: System.Data.SqlClient, 4.6.1"

using System;
using System.Collections.Generic;
using System.ComponentModel;
using LabellingDB;
using System.Data.SqlClient;
using System.IO;

/* These are fractions of the total, best to make them add up to about 100, e.g. 75, 20, 5 */
int train_set_fraction =100; //This fraction used for training
int val_set_fraction = 0;
int test_set_fraction = 0;

string output_dir=@"..\image-library\";				//Where to put the generated files, relative to where the script is ran from

string file_prefix = @"\home\openworks\image-library\";		//The prefix to give the references to the training files, should be blank or an absolute path on the target machine
var image_file_prefix = @"\home\openworks\image-library\";	//The Prefix to give to references to the images, for the target machine
bool to_linux = true;					//If true convert windows path separate \ to linux path separator /	

string definition_file = "test_train.yml";   //The file that holds the references to the training, validation and test image lists, and the definition of the classes
string train_file = "train_set.txt";			  //Holds the list of images and their ground truth bounding boxes
string validate_file = "validate_set.txt";
string test_file = "test_set.txt";

var classes = new string[] { "Drone" };	//The classes we want to train for, e.g. "Drone", "Tree Branch"
int sensor_type = 0;					//0 = Daylight Camera, 1 = CheetIR Camera
var image_tags = new string[] {};		//Select only images with these tags. E.G. "uniform cloud", "tree background", "day time", if empty selects all images

/*End of script options */
string labels_where_clause = string.Join(" OR ",classes.Select(x => "label_trees.name = '" + x +"'"));
string image_tags_where_clause = string.Join("",image_tags.Select(x => "AND images.tags LIKE '%" + x +"%'"));
var query = $@"
USE ImageDatabase

DROP TABLE IF EXISTS #temp_selection;
DROP TABLE IF EXISTS #temp_labels;

SELECT ROW_NUMBER() OVER (ORDER BY label_trees.id)-1 AS 'root_id', lft, rgt, name
INTO #temp_selection
FROM label_trees
WHERE parent_id > -1
	AND ({labels_where_clause})

SELECT	#temp_selection.root_id AS 'root_id',
		#temp_selection.name AS 'root_name',
		label_trees.id AS 'label_id',
		label_trees.name AS 'label_name'
INTO #temp_labels
FROM label_trees, #temp_selection
WHERE label_trees.lft >= #temp_selection.lft AND label_trees.rgt <= #temp_selection.rgt
	AND (SELECT COUNT(bbox_labels.id) FROM bbox_labels WHERE bbox_labels.label_id = label_trees.id) > 0

--(SELECT 0 AS 'rand_id', STRING_AGG(CONCAT('''', #temp_selection.name, ''''), ','), '' FROM #temp_selection)
--UNION
(SELECT RAND(CHECKSUM(NEWID())) AS 'rand_id', CONCAT('{image_file_prefix}', TRIM(images.filepath)) AS 'filepath', STRING_AGG(CONCAT(#temp_labels.root_id, ' ', bbox_labels.location_x, ' ',bbox_labels.location_y, ' ', bbox_labels.size_width, ' ',bbox_labels.size_height), ','),
	images.image_width,
	images.image_height
FROM images, bbox_labels, #temp_labels
WHERE images.labelling_complete = 1
	AND bbox_labels.image_id = images.id
	AND #temp_labels.label_id = bbox_labels.label_id
	AND images.sensor_type = {sensor_type.ToString()}
	{image_tags_where_clause}
GROUP BY images.filepath, images.image_width, images.image_height)
ORDER BY 'rand_id'
";

var label_index_query = "SELECT #temp_selection.name FROM #temp_selection ORDER BY #temp_selection.root_id";

var dataset = new List<string>();
var connectionString = "Data Source=.\\SQLEXPRESS; Database=ImageDatabase; User Id=Exporter; Password=ProjectPirateShip; Integrated Security=False; Persist Security Info=False; Pooling=False;MultipleActiveResultSets=False; Connect Timeout=60; Encrypt=False; TrustServerCertificate=False";
var orderedClasses = new List<string>();

using (var conn = new SqlConnection(connectionString))
{
	conn.Open();
	using (var cmd = new SqlCommand(query, conn))
	using (var rdr = cmd.ExecuteReader())
	{
		while (rdr.Read())
		{
			var imagePath = rdr.GetString(1);
			var labels = rdr.GetString(2);
			int width = rdr.GetInt32(3);
			int height = rdr.GetInt32(4);

			if (to_linux) { imagePath = imagePath.Replace(@"\","/"); }
			
			var line = imagePath + "|" + $"{width} {height}" + "|" + labels;
			dataset.Add(line);
		}
	}
	/* Get the list of classes as they were ordered by the SQL query */
	using (var cmd = new SqlCommand(label_index_query, conn))
	using (var rdr = cmd.ExecuteReader())
	{
		while (rdr.Read())
			orderedClasses.Add(rdr.GetString(0));
	}
}

var total = train_set_fraction + val_set_fraction + test_set_fraction;

var training_imgs = new List<string>();
var validate_imgs = new List<string>();
var test_imgs = new List<string>();
var rng = new Random();

while (dataset.Count() > 0)
{
    var i = rng.Next(total);
    // 0 = train, 1 = validate, 2 = test
    var set = (i < train_set_fraction) ? 0 : ((i < (train_set_fraction+val_set_fraction)) ? 1 : 2);

    var index = (int)Math.Floor(rng.NextDouble() * dataset.Count());

    if (set == 0)
        training_imgs.Add(dataset[index]);
    else if (set == 1)
        validate_imgs.Add(dataset[index]);
    else if (set == 2)
        test_imgs.Add(dataset[index]);

    dataset.RemoveAt(index);
}

/* Writes the set of files to a data set file */
Action<string[],string> write_set = (string[] images, string filePath) => {
	using (var writer = new StreamWriter(filePath))
	{
		foreach(var img in images)
			writer.WriteLine(img);
	}
};

var definitionFilePath = System.IO.Path.Combine(output_dir, definition_file);
write_set(training_imgs.ToArray(), System.IO.Path.Combine(output_dir, train_file));
write_set(validate_imgs.ToArray(), System.IO.Path.Combine(output_dir, validate_file));
write_set(test_imgs.ToArray(), System.IO.Path.Combine(output_dir, test_file));

if (to_linux) { file_prefix = file_prefix.Replace(@"\","/"); }

/* Generate the yaml file defining the test set */
using (var writer = new StreamWriter(definitionFilePath))
{
	writer.WriteLine("#Training, validation and test data sets");
	writer.WriteLine("train: " + System.IO.Path.Combine(file_prefix, train_file));
	writer.WriteLine("val: " + System.IO.Path.Combine(file_prefix, validate_file));
	writer.WriteLine("test: " + System.IO.Path.Combine(file_prefix, test_file));
	writer.WriteLine("");
	writer.WriteLine("# number of classes");
	writer.WriteLine("nc: " + classes.Length.ToString());
	writer.WriteLine("");
	writer.WriteLine("# class names");
	writer.WriteLine("names: ['" + string.Join("','", orderedClasses) + "']");
}

Console.WriteLine($"Training Images: {training_imgs.Count()}, Validation Images: {validate_imgs.Count()}, Test Images: {test_imgs.Count()}");



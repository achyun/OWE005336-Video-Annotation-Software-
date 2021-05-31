using LabellingDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace OWE005336__Video_Annotation_Software_
{
     /// <summary>
     // Handles exporting a train/validate/test dataset as a configuration yaml file and 3 files listing the images and their BBOXes. Based on Yolov5 format but modified for our purposes
     /// </summary>
    public class DatasetSerializer
    {
        public DatasetSerializerSettings Settings { get; set; } = new DatasetSerializerSettings();

        public List<LabelledImage> TrainingDataset { get; } = new List<LabelledImage>();
        public List<LabelledImage> ValidationDataset { get; } = new List<LabelledImage>();
        public List<LabelledImage> TestDataset { get; } = new List<LabelledImage>();

        public DatasetSerializer(DatasetSerializerSettings settings = null)
        {
            this.Settings = settings ?? this.Settings;
        }

        public void Serialize(string directory, string name, DateTime? dateCreated = null)
        {
            if (!dateCreated.HasValue)
                dateCreated = DateTime.Now;

            //Serialize each list of images into a separate file
            SerializeDataset(Path.Combine(directory, Settings.TrainingDatasetFileName), TrainingDataset);
            SerializeDataset(Path.Combine(directory, Settings.ValidationDatasetFileName), ValidationDataset);
            SerializeDataset(Path.Combine(directory, Settings.TestDatasetFileName), TestDataset);

            //Generate the configuration file
            var configuration = new DatasetConfiguration() {
                TrainingDataset = Settings.TrainingDatasetFileName,
                ValidationDataset = Settings.ValidationDatasetFileName,
                TestDataset = Settings.TestDatasetFileName,
                Name = name,
                DateCreated = dateCreated.Value
            };

            //Serialize the configuration file
            var yamlSerializer = (new SerializerBuilder()).WithNamingConvention(PascalCaseNamingConvention.Instance).Build();
            System.IO.File.AppendAllText(Path.Combine(directory, Settings.DefinitionFileName), yamlSerializer.Serialize(configuration));
        }

        protected void SerializeDataset(string filePath, IEnumerable<LabelledImage> images)
        {
            using(var x = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (var img in images)
                    x.WriteLine(SerializeLabelledImage(img, Settings.ImageDirectoryPrefix, Settings.ToLinux));
            }
        }

        public static string SerializeLabelledImage(LabelledImage img, string directoryPrefix, bool to_linux)
        {
            var imagePath = Path.Combine(directoryPrefix, img.Filepath);

            if (to_linux)
                imagePath = imagePath.Replace(@"\", "/");

            var imgSize = $"{img.ImageSize.Width} {img.ImageSize.Height}";
            var labels = string.Join("|", img.LabelledROIs.Select(x => $"{x.LabelName},{x.ROI.X},{x.ROI.Y},{x.ROI.Width},{x.ROI.Height}"));
            var line = $"{imagePath}|{imgSize}|{labels}";
            return line;
        }
    }

    public class DatasetSerializerSettings
    {
        public string DatasetFilePath { get; set; } = "";
        public string ImageDirectoryPrefix { get; set; } = "";
        public bool ToLinux { get; set; } = true;

        public string DefinitionFileName { get; set; } = "dataset.yaml";
        public string TrainingDatasetFileName { get; set; } = "training.yolo.txt";
        public string ValidationDatasetFileName { get; set; } = "validation.yolo.txt";
        public string TestDatasetFileName { get; set; } = "test.yolo.txt";
    }

    public class DatasetConfiguration
    {
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public string TrainingDataset { get; set; }
        public string ValidationDataset { get; set; }
        public string TestDataset { get; set; }
    }
}

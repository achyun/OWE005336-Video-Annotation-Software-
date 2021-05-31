using LabellingDB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWE005336__Video_Annotation_Software_.Classes
{
    /// <summary>
    /// When given a folder, will load all the images into a list of LabelledImages.
    /// When asked to load images, will load thumbnails async and call the thumbnailLoaded event
    /// </summary>
    public class ImageFolderLoader
    {
        public string[] ImageFileExtensions = new string[] { ".png", ".jpg", ".bmp", ".gif" };
        public string[] MetadataFileExtensions = new string[] { ".txt" };

        public Dictionary<int, string> Detector_LabelMap = new Dictionary<int, string>() { { 0, "Rotary" }, { 1, "Fixed" }, { 2, "Bird" }, { 3,  "Tree" } }; //Mapping from the detector class IDs to labels

        public string FolderPath { get; protected set; }
        public ImageFolderLoader() { }

        public void LoadFolder(string folderPath)
        {
            FolderPath = folderPath;

            if (!Directory.Exists(folderPath))
                throw new ArgumentException("folderPath is not a directory", nameof(folderPath));

            foreach (var filePath in Directory.GetFiles(folderPath))
            {
                if (!ImageFileExtensions.Contains(Path.GetExtension(filePath)))
                    continue;   //Ignore files that are not images

                Image imgInfo;

                //This should avoid loading the full image into memory to speed up the file reading
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    imgInfo = Image.FromStream(fs, false, false);

                LabelledImage img = new LabelledImage(imgInfo.Size);

                //Look for metadata file containing ROIs
                var roiDataFilePath = GetMetadataFile(folderPath, filePath);

                if (roiDataFilePath != null)
                {

                }
            }
        }

        private string GetMetadataFile(string folderPath, string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            string metadataFilePath = null;

            foreach (var extension in MetadataFileExtensions)
            {
                var testFilePath = Path.Combine(folderPath, fileName + extension);
                if (File.Exists(testFilePath))
                {
                    metadataFilePath = testFilePath;
                    break;
                }
            }
            return metadataFilePath;
        }

        public LabelledROI[] ReadROI(string filePath)
        {
            List<LabelledROI> rois = new List<LabelledROI>();

            var lines = File.ReadAllLines(filePath);

            foreach(var line in lines)
            {
                var fields = line.Split(' ');
                if (fields.Length < 5)
                    continue;

                try
                {
                    int center_x = int.Parse(fields[1]);
                    int center_y = int.Parse(fields[2]);
                    int width = int.Parse(fields[3]);
                    int height = int.Parse(fields[4]);
                    //float confidence = float.Parse(fields[5]); // Not used

                    string label = fields[0];
                    int label_ID;
                    if (int.TryParse(fields[0], out label_ID))
                    {
                        //This has come from the detector with just a class ID. Map to a label
                        label = Detector_LabelMap[label_ID];
                    }

                    rois.Add(new LabelledROI(-1, -1, new Rectangle()));
                }
                catch (FormatException)
                { 
                    //One of the fields was invalid, ignore this line
                }
            }
        }
    }
}

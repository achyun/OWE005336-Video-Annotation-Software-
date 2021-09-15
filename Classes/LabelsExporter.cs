using LabellingDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OWE005336__Video_Annotation_Software_.Classes
{
    public static class LabelsExporter
    {
        public static void Export()
        {
            var labelsTree = Program.ImageDatabase.LabelTree_LoadAll();
            List<LabelNode> labels = new List<LabelNode>();

            //Convert to a flat list
            void flatten(LabelNode label)
            {
                labels.Add(label);
                foreach (var childLabel in label.Children)
                {
                    flatten(childLabel);
                }
            };

            foreach (var label in labelsTree)
                flatten(label);

            labels.Sort((a,b) => a.ID.CompareTo(b.ID));

            using (var dlg = new SaveFileDialog())
            {
                dlg.InitialDirectory = Properties.Settings.Default.DefaultFileLocation;
                dlg.Filter = "yaml files (*.yaml)|*.yaml";
                dlg.FileName = "classes.yaml";
                dlg.RestoreDirectory = true;
                
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var filePath = dlg.FileName;

                    using (var f = new StreamWriter(filePath))
                    {

                        foreach (var label in labels)
                            f.WriteLine($"{label.ID}: {label.TextID}");
                    }
                }
            }
        }
    }
}

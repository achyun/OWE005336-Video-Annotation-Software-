using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabellingDB;
using Accord;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Core;

namespace OWE005336__Video_Annotation_Software_
{
    public partial class fClassificationExport : Form
    {
        public fClassificationExport()
        {
            InitializeComponent();

            RefreshTaskGrid();

            txtOutputDir.Text = Properties.Settings.Default.ClassificationExportLocation;
            btnExport.Enabled = dgvResults.Rows.Count > 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count > 0)
            {
                ClassificationExportTask task = (ClassificationExportTask)dgvTasks.SelectedRows[0].DataBoundItem;

                task.Domains = domainLabelsSelector.SelectedLabels;
                task.Outputs = outputLabelsSelector.SelectedLabels;
                task.Projects = projectSelector.SelectedProjects;
                task.SQL = txtSQL.Text;
                
                task.PadTrainingData = ckbPadTrainData.Checked;
                task.PadValidationData = ckbPadValidationData.Checked;
                task.PadTestData = ckbPadTestData.Checked;

                double total = (double)(nudTrainPct.Value + nudValidPct.Value + nudTestPct.Value);
                if (total > 0)
                {
                    task.TrainingPercent = 100.0 * (double)nudTrainPct.Value / total;
                    task.ValidationPercent = 100.0 * (double)nudValidPct.Value / total;
                    task.TestPercent = 100.0 * (double)nudTestPct.Value / total;
                }

                task.MinPixelsTrain = (int)nudMinPixelsTrain.Value;
                task.MinPixelsValidation = (int)nudMinPixelsValidation.Value;
                task.MinPixelsTest = (int)nudMinPixelsTest.Value;

                if (task.ID >= 0) //IF ID < 0 this isn't a task stored in the database (e.g. a historic task loaded from a file), do not attempt to save
                {
                    if (!Program.ImageDatabase.UpdateClassificationExportTask(task))
                    {
                        MessageBox.Show("Update Failed");
                    }
                }

                PaintClassificationExportTask(task);
                RunTaskQuery(task);
            }
        }

        private void dgvTasks_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            string name = "";
            ClassificationExportTask task = Program.ImageDatabase.AddClassificationExportTask(name);

            if (task != null)
            {
                PaintClassificationExportTask(task);
            }
            else
            {
                MessageBox.Show("Add Failed");
                RefreshTaskGrid();
            }
        }

        private void RefreshTaskGrid()
        {
            List<ClassificationExportTask> tasks = Program.ImageDatabase.LoadClassificationExportTasks(false);
            dgvTasks.DataSource = new BindingList<ClassificationExportTask>(tasks);
            //foreach (var t in tasks)
            //{
            //    dgvTasks.Rows.Add(t.Name);
            //    dgvTasks.Rows[dgvTasks.Rows.Count - 1].Tag = t.Clone();
            //}
        }

        private void PaintClassificationExportTask(ClassificationExportTask task)
        {
            txtSQL.Text = task.SQL;
            domainLabelsSelector.SetLabels(task.Domains);
            outputLabelsSelector.SetLabels(task.Outputs);
            projectSelector.SetProjects(task.Projects);

            ckbPadTrainData.Checked = task.PadTrainingData;
            ckbPadValidationData.Checked = task.PadValidationData;
            ckbPadTestData.Checked = task.PadTestData;

            nudTrainPct.Value = (decimal)task.TrainingPercent;
            nudValidPct.Value = (decimal)task.ValidationPercent;
            nudTestPct.Value = (decimal)task.TestPercent;

            nudMinPixelsTrain.Value = (decimal)task.MinPixelsTrain;
            nudMinPixelsValidation.Value = (decimal)task.MinPixelsValidation;
            nudMinPixelsTest.Value = (decimal)task.MinPixelsTest;

        }

        private void dgvTasks_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            foreach (var r in dgvTasks.SelectedRows)
            {
                ClassificationExportTask t = (ClassificationExportTask)dgvTasks.SelectedRows[0].DataBoundItem;
                Program.ImageDatabase.DeleteClassificationExportTask(t.ID);
            }
        }

        private void dgvTasks_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ClassificationExportTask t = (ClassificationExportTask)dgvTasks.Rows[e.RowIndex].DataBoundItem;

            if (t.ID >= 0) //IF ID < 0 this isn't a task stored in the database (e.g. a historic task loaded from a file), do not attempt to save
            {
                if (!Program.ImageDatabase.UpdateClassificationExportTask(t))
                {
                    MessageBox.Show("Update Failed");
                }
            }
        }

        private void dgvTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count > 0)
            {
                ClassificationExportTask t = (ClassificationExportTask)dgvTasks.SelectedRows[0].DataBoundItem;
                PaintClassificationExportTask(t);
            }
            
        }

        private void RunTaskQuery(ClassificationExportTask task)
        {
            //Generate Filter for given labels
            string labelsFilter = string.Join(" OR ", task.Outputs.Select(x => $"label_trees.name = '{x.Name}'\n"));
            labelsFilter = "(" + labelsFilter + ")";

            string projectsFilter = string.Join(",", task.Projects.Select(x => x.ID.ToString()));
            projectsFilter = "(" + projectsFilter + ")";

            string sql = task.SQL.Replace("${LabelsFilter}", labelsFilter)
                                .Replace("${ProjectsFilter}", projectsFilter);

            string err;
            DataTable dt = Program.ImageDatabase.RunCustomQuery(sql, out err);

            if (err == "")
            {
                lblSummary.Text = dt.Rows.Count.ToString() + " rows retrieved";
                foreach (var label in task.Outputs)
                {
                    int numberOfOccurences = 0;
                    foreach (var row in dt.Rows.Cast<DataRow>())
                    {
                        string[] labelsInImage = row.ItemArray[2].ToString().Split('|');
                        numberOfOccurences += labelsInImage.Sum(x => x.Contains(label.TextID) ? 1 : 0);
                    }
                    lblSummary.Text += $"\n {label.Name}: {numberOfOccurences}";
                }
                dgvResults.DataSource = dt;
            }
            else
            {
                lblSummary.Text = err;
                dgvResults.DataSource = null;
            }
            btnExport.Enabled = dgvResults.Rows.Count > 0;
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            if ((nudTrainPct.Value + nudTestPct.Value + nudTestPct.Value) <= 0)
            {
                MessageBox.Show("Invalid train/validate/test percentages");
                return;
            }
            else if (!Directory.Exists(txtOutputDir.Text))
            {
                MessageBox.Show("Output directory doesn't exist");
                return;
            }

            ClassificationExportTask t = (dgvTasks.SelectedRows.Count > 0) ? (ClassificationExportTask)dgvTasks.SelectedRows[0].DataBoundItem : new ClassificationExportTask() { Name = "" };
            t.SQL = txtSQL.Text;
            t.PadTrainingData = ckbPadTrainData.Checked;
            t.PadValidationData = ckbPadValidationData.Checked;
            t.PadTestData = ckbPadTestData.Checked;
            t.TrainingPercent = (double)nudTrainPct.Value;
            t.ValidationPercent = (double)nudValidPct.Value;
            t.TestPercent = (double)nudTestPct.Value;
            t.MinPixelsTrain = (int)nudMinPixelsTrain.Value;
            t.MinPixelsValidation = (int)nudMinPixelsValidation.Value;
            t.MinPixelsTest = (int)nudMinPixelsTest.Value;
            t.Domains = domainLabelsSelector.SelectedLabels;
            t.Outputs = outputLabelsSelector.SelectedLabels;
            t.Projects = projectSelector.SelectedProjects;

            string imageDirPath = Program.ImageDatabase.Settings_Get(ImageDatabaseAccess.SETTING_IMAGE_DIR);
            DateTime localDate = DateTime.Now;
            string exportName = localDate.Year.ToString() + localDate.Month.ToString("00") + localDate.Day.ToString("00") + "_" + dgvTasks.SelectedRows[0].Cells[0].Value.ToString();
            string subDirPath = Path.Combine(txtOutputDir.Text, exportName);
            bool generateImages = chkGenerateImages.Checked;
            DataTable dt = (DataTable)dgvResults.DataSource;
            int rowIndex = 0;
            float percent;
            string trainDirPath = Path.Combine(subDirPath, "train");
            string validDirPath = Path.Combine(subDirPath, "valid");
            string testDirPath = Path.Combine(subDirPath, "test");


            btnSave_Click(null, null);

            try
            {
                Directory.CreateDirectory(subDirPath);
                if (generateImages)
                {
                    Directory.CreateDirectory(trainDirPath);
                    Directory.CreateDirectory(validDirPath);
                    Directory.CreateDirectory(testDirPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create directory '" + subDirPath + "': " + ex.Message);
                return;
            }

            //Save the SQL query used for this task as a .sql file in the root folder
            var yamlSerializer = (new SerializerBuilder())
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .WithTypeConverter(new Classes.LabelNodeYamlTypeConverter())
                .Build();

            System.IO.File.AppendAllText(Path.Combine(subDirPath, "exportSettings.yaml"), yamlSerializer.Serialize(t));

            fProgressBar progress = new fProgressBar("Exporting images", "Exporting images from row 1 of " + dt.Rows.Count);
            progress.Show();

                
            Random randGen = new Random();
            double total = t.TrainingPercent + t.ValidationPercent + t.TestPercent;
            double boundTrain = t.TrainingPercent / total;
            double boundValid = boundTrain + t.ValidationPercent / total;

            var datasetSerializer = new DatasetSerializer(new DatasetSerializerSettings() { });

            await Task.Run(() =>
            {
                
                foreach (DataRow r in dt.Rows)
                {
                    string imgPath = Path.Combine(imageDirPath, r.ItemArray[1].ToString());
                    try
                    {
                        //Debug.WriteLine("Start new: " + imgPath);
                        if (File.Exists(imgPath))
                        {
                            //Debug.WriteLine("File exists...");
                            //Debug.WriteLine("File loaded...");

                            //Select the dataset for this image
                            //Note that for YOLO we need to group by image, for classifier in theory we could split out each bbox and put it in a separate dataset
                            double rand = (double)r.ItemArray[0]; //randGen.NextDouble();
                            string targetDir;
                            bool doPad = false;
                            int minPixels;
                            List<LabelledImage> targetDataset;
                            if (rand < boundTrain) { targetDir = trainDirPath; doPad = t.PadTrainingData; minPixels = t.MinPixelsTrain; targetDataset = datasetSerializer.TrainingDataset; }
                            else if (rand < boundValid) { targetDir = validDirPath; doPad = t.PadValidationData; minPixels = t.MinPixelsValidation; targetDataset = datasetSerializer.ValidationDataset; }
                            else { targetDir = testDirPath; doPad = t.PadTestData; minPixels = t.MinPixelsTest; targetDataset = datasetSerializer.TestDataset; }

                            string bbox_string = r.ItemArray[2].ToString();
                            //Debug.WriteLine("BBox String: " + bbox_string);
                            string[] bboxes = bbox_string.Split('|');
                            var img_size = new Size((int)r.ItemArray[3], (int)r.ItemArray[4]);

                            LabelledImage img = new LabelledImage(img_size);
                            img.Filepath = r.ItemArray[1].ToString();

                            //Parse information about the bouding boxes in the image
                            for (int i = 0; i < bboxes.Length; i++)
                            {
                                //Debug.WriteLine("Idx: " + i.ToString());
                                try
                                {
                                    string[] values = bboxes[i].Split(',');
                                    //Debug.WriteLine(bboxes[i]);
                                    string catDirPath = Path.Combine(targetDir, values[0]);
                                    string newPath = Path.Combine(catDirPath, Path.GetFileNameWithoutExtension(imgPath) + "_" + i.ToString() + ".png");
                                    //Debug.WriteLine(newPath);
                                    if (!Directory.Exists(catDirPath))
                                    {
                                        Directory.CreateDirectory(catDirPath);
                                    }
                                    
                                    Rectangle cropRect = new Rectangle(int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]), int.Parse(values[4]));
                                    if (cropRect.Width >= minPixels || cropRect.Height >= minPixels)
                                    {
                                        if (doPad) { cropRect = PadRectangleToSquare(cropRect); }

                                        if (generateImages)
                                        {   //Save an image of just the selected region in the correct folder for its label
                                            //Debug.WriteLine(newPath + ", " + cropRect.ToString());
                                            Bitmap origImg = new Bitmap(imgPath);
                                            Bitmap croppedbmp = CropBitmap(origImg, cropRect);
                                            croppedbmp.Save(newPath);
                                            //Debug.WriteLine("Save Complete");
                                        }

                                        img.LabelledROIs.Add(new LabelledROI(0, 0, cropRect) { LabelName = values[0] });
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error exporting image from: " + imgPath + "\r\n   With bounding box string: " + bbox_string + "\r\n" + ex.Message);
                                }
                            }

                            //Do not include images that have had all their labels filtered out (e.g. the label is too small for this dataset)
                            if (img.LabelledROIs.Count() > 0)
                                targetDataset.Add(img);
                        }
                        rowIndex += 1;

                        percent = 100 * rowIndex / dt.Rows.Count;
                        progress.BeginInvoke((Action)(() => progress.UpdateProgress(percent, "Exporting images from row " + rowIndex.ToString() + " of " + dt.Rows.Count)));
                        if (progress.Cancelled) { break; }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Corrupted file: " + imgPath);
                    }
                }
                //Write configuration data
                datasetSerializer.Serialize(subDirPath, exportName, t);
            });

            progress.Close();
        }

        private Rectangle PadRectangleToSquare(Rectangle rect)
        {
            if (rect.Width > rect.Height)
            {
                rect.Y = rect.Y + (int)((rect.Height - rect.Width) / 2);
                rect.Height = rect.Width;
            }
            else if (rect.Height > rect.Width)
            {
                rect.X = rect.X + (int)((rect.Width - rect.Height) / 2);
                rect.Width = rect.Height;
            }

            return rect;
        }

        private Bitmap CropBitmap(Bitmap src, Rectangle cropRect)
        {
            int left = cropRect.Left;
            int right = cropRect.Right;
            int top = cropRect.Top;
            int bottom = cropRect.Bottom;

            left = Math.Min(Math.Max(0, left), src.Width - 1);
            right = Math.Min(Math.Max(left + 1, right), src.Width);
            top = Math.Min(Math.Max(0, top), src.Height - 1);
            bottom = Math.Min(Math.Max(top + 1, bottom), src.Height);

            cropRect.X = left;
            cropRect.Y = top;
            cropRect.Width = right - left;
            cropRect.Height = bottom - top;

            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }

            return target;
        }

        private void txtOutputDir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (Directory.Exists(txtOutputDir.Text))
                {
                    Properties.Settings.Default.ClassificationExportLocation = txtOutputDir.Text;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    MessageBox.Show("Invalid directory");
                }
            }
        }

        private void btnSelectDirectory_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputDir.Text = folderBrowserDialog.SelectedPath;
                    Properties.Settings.Default.ClassificationExportLocation = txtOutputDir.Text;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void btnOpenPrevScript_Click(object sender, EventArgs e)
        {
            //Allow the user to select a *.sql file representing a previous run, and run that
            string filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = txtOutputDir.Text;
                openFileDialog.Filter = "yaml files (*.yaml)|*.yaml|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }

            var deserializer = new DeserializerBuilder().WithNamingConvention(PascalCaseNamingConvention.Instance).Build();
            ClassificationExportTask task;
            try
            {
                task = deserializer.Deserialize<ClassificationExportTask>(File.ReadAllText(filePath));
            }
            catch(YamlException ex)
            {
                MessageBox.Show($"Error reading task: {ex}", "Error Reading Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Set ID to -1 to show that this task isn't stored in the database
            task.ID = -1;

            var taskList = dgvTasks.DataSource as BindingList<ClassificationExportTask>;
            taskList.Add(task);
        }
    }
}

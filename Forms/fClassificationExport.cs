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

namespace OWE005336__Video_Annotation_Software_
{
    public partial class fClassificationExport : Form
    {
        public fClassificationExport()
        {
            InitializeComponent();

            RefreshTaskGrid();

            txtOutputDir.Text = Properties.Settings.Default.ClassificationExportLocation;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count > 0)
            {
                ClassificationExportTask task = (ClassificationExportTask)dgvTasks.SelectedRows[0].DataBoundItem;
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

                if (!Program.ImageDatabase.UpdateClassificationExportTask(task))
                {
                    MessageBox.Show("Update Failed");
                }
                else
                {
                    PaintClassificationExportTask(task);
                }
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

            ckbPadTrainData.Checked = task.PadTrainingData;
            ckbPadValidationData.Checked = task.PadValidationData;
            ckbPadTestData.Checked = task.PadTestData;

            nudTrainPct.Value = (decimal)task.TrainingPercent;
            nudValidPct.Value = (decimal)task.ValidationPercent;
            nudTestPct.Value = (decimal)task.TestPercent;

            nudMinPixelsTrain.Value = (decimal)task.MinPixelsTrain;
            nudMinPixelsValidation.Value = (decimal)task.MinPixelsValidation;
            nudMinPixelsTest.Value = (decimal)task.MinPixelsTest;

            RunTaskQuery(task);
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

            if (!Program.ImageDatabase.UpdateClassificationExportTask(t))
            {
                MessageBox.Show("Update Failed");
            }
        }

        private void dgvTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count > 0)
            {
                ClassificationExportTask t = (ClassificationExportTask)dgvTasks.SelectedRows[0].DataBoundItem;
                PaintClassificationExportTask(t);
                RunTaskQuery(t);
            }
            
        }

        private void RunTaskQuery(ClassificationExportTask task)
        {
            string err;
            DataTable dt = Program.ImageDatabase.RunCustomQuery(task.SQL, out err);

            if (err == "")
            {
                lblSummary.Text = dt.Rows.Count.ToString() + " rows retrieved";
                dgvResults.DataSource = dt;
            }
            else
            {
                lblSummary.Text = err;
                dgvResults.DataSource = null;
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            double total = (double)(nudTrainPct.Value + nudValidPct.Value + nudTestPct.Value);

            if (total <= 0)
            {
                MessageBox.Show("Invalid train/validate/test percentages");
                return;
            }
            else if (!Directory.Exists(txtOutputDir.Text))
            {
                MessageBox.Show("Output directory doesn't exist");
                return;
            }

            bool padTrain = ckbPadTrainData.Checked;
            bool padValid = ckbPadValidationData.Checked;
            bool padTest = ckbPadTestData.Checked;
            double boundTrain = (double)nudTrainPct.Value / total;
            double boundValid = boundTrain + (double)nudValidPct.Value / total;
            string imageDirPath = Program.ImageDatabase.Settings_Get(ImageDatabaseAccess.SETTING_IMAGE_DIR);
            DateTime localDate = DateTime.Now;
            string subDirPath = localDate.Year.ToString() + localDate.Month.ToString("00") + localDate.Day.ToString("00") + "_" + localDate.Hour.ToString("00") + localDate.Minute.ToString("00") + localDate.Second.ToString("00") + "_" + dgvTasks.SelectedRows[0].Cells[0].Value.ToString();
            subDirPath = Path.Combine(txtOutputDir.Text, subDirPath);
            DataTable dt = (DataTable)dgvResults.DataSource;
            int rowIndex = 0;
            float percent;
            string trainDirPath = Path.Combine(subDirPath, "train");
            string validDirPath = Path.Combine(subDirPath, "valid");
            string testDirPath = Path.Combine(subDirPath, "test");
            int minPixelsTrain = (int)nudMinPixelsTrain.Value;
            int minPixelsValid = (int)nudMinPixelsValidation.Value;
            int minPixelsTest = (int)nudMinPixelsTest.Value;

            btnSave_Click(null, null);

            try
            {
                Directory.CreateDirectory(subDirPath);
                Directory.CreateDirectory(trainDirPath);
                Directory.CreateDirectory(validDirPath);
                Directory.CreateDirectory(testDirPath);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to create directory '" + subDirPath + "': " + ex.Message);
                return;
            }

            fProgressBar progress = new fProgressBar("Exporting images", "Exporting images from row 1 of " + dt.Rows.Count);
            progress.Show();

                
            Random randGen = new Random();

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
                            Bitmap origImg = new Bitmap(imgPath);
                            //Debug.WriteLine("File loaded...");
                            string bbox_string = r.ItemArray[2].ToString();
                            //Debug.WriteLine("BBox String: " + bbox_string);
                            string[] bboxes = bbox_string.Split('|');

                            for (int i = 0; i < bboxes.Length; i++)
                            {
                                double rand = randGen.NextDouble();
                                string targetDir;
                                bool doPad = false;
                                int minPixels;
                                if (rand < boundTrain) { targetDir = trainDirPath; doPad = padTrain; minPixels = minPixelsTrain; }
                                else if (rand < boundValid) { targetDir = validDirPath; doPad = padValid; minPixels = minPixelsValid; }
                                else { targetDir = testDirPath; doPad = padTest; minPixels = minPixelsTest; }
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
                                        //Debug.WriteLine(newPath + ", " + cropRect.ToString());
                                        Bitmap croppedbmp = CropBitmap(origImg, cropRect);
                                        croppedbmp.Save(newPath);
                                        //Debug.WriteLine("Save Complete");
                                                       
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error exporting image from: " + imgPath + "\r\n   With bounding box string: " + bbox_string + "\r\n" + ex.Message);
                                }
                            }
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
    }
}

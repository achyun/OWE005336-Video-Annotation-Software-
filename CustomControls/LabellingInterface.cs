using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OWE005336__Video_Annotation_Software_
{
    public partial class LabellingInterface : UserControl
    {
        private ROISelector _ROISelector = new ROISelector();
        private bool _PaintDataInProgress = false;
        public LabellingInterface()
        {
            InitializeComponent();
            pnlImage.Controls.Add(_ROISelector);
            _ROISelector.Dock = DockStyle.Fill;

            cmbSensorType.DataSource = Enum.GetValues(typeof(SensorTypeEnum));

            _ROISelector.NewROIAdded += _ROISelector_NewROIAdded;
            _ROISelector.ROIChanged += _ROISelector_ROIChanged;
            _ROISelector.ROIDeleted += _ROISelector_ROIDeleted;
            _ROISelector.ROISelectionChanged += _ROISelector_ROISelectionChanged;
            dgvLabels.KeyDown += dgvLabels_KeyDown;
            dgvLabels.SelectionChanged += dgvLabels_SelectionChanged;
            dgvLabels.CellValueChanged += dgvLabels_CellValueChanged;

            cmbSensorType.SelectedIndexChanged += cmbSensorType_SelectedIndexChanged;

            tbxTags.TagsChanged += tbxTags_TagsChanged;
        }

        #region "GUI Events"
        private void tbxTags_TagsChanged(TagBox sender, EventArgs e)
        {
            if (!_PaintDataInProgress)
            {
                LabelledImage.Tags = sender.ToString();
                Program.ImageDatabase.Images_Update(LabelledImage);
            }
        }

        private void cmbSensorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_PaintDataInProgress)
            {
                LabelledImage.SensorType = (SensorTypeEnum)cmbSensorType.SelectedItem;
                Program.ImageDatabase.Images_Update(LabelledImage);
            }
        }

        private void dgvLabels_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            LabelledROI lroi = (LabelledROI)dgvLabels.Rows[e.RowIndex].Tag;

            if (e.ColumnIndex == dgvLabels.Columns["Label"].Index)
            { 
                //lroi.LabelID = ((LabelNode)dgvLabels.Rows[e.RowIndex].Cells[e.ColumnIndex].Value).ID;
            }
            else if (e.ColumnIndex == dgvLabels.Columns["Truncated"].Index)
            {
                if (!_PaintDataInProgress)
                {
                    lroi.Truncated = (bool)dgvLabels.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    Program.ImageDatabase.BBoxLabels_UpdateLabel(lroi);
                }
            }
            else if (e.ColumnIndex == dgvLabels.Columns["Occluded"].Index)
            {
                if (!_PaintDataInProgress)
                {
                    lroi.Occluded = (bool)dgvLabels.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    Program.ImageDatabase.BBoxLabels_UpdateLabel(lroi);
                }
            }            
        }

        private void dgvLabels_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLabels.SelectedRows.Count > 0)
            {
                _ROISelector.SelectedROIIndex = dgvLabels.SelectedRows[0].Index;
            }
            else
            {
                _ROISelector.SelectedROIIndex = -1;
            }
            
        }

        private void dgvLabels_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvLabels.SelectedRows.Count > 0 && e.KeyCode == Keys.Delete)
            {
                List<DataGridViewRow> toBeDeleted = new List<DataGridViewRow>();

                for (int i = dgvLabels.SelectedRows.Count - 1; i >= 0; i--)
                {
                    int index = dgvLabels.SelectedRows[i].Index;
                    Program.ImageDatabase.BBoxLabels_DeleteLabelByID(LabelledImage.LabelledROIs[index].ID);
                    LabelledImage.LabelledROIs.RemoveAt(index);
                    _ROISelector.RemoveROIByIndex(index);
                    toBeDeleted.Add(dgvLabels.SelectedRows[i]);
                }

                foreach (DataGridViewRow dr in toBeDeleted)
                {
                    dgvLabels.Rows.Remove(dr);
                }
            }
        }
        private void dgvLabels_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == dgvLabels.Columns["Label"].Index)
            {
                using (fLabelSelector labelSelector = new fLabelSelector())
                {
                    if (labelSelector.ShowDialog() == DialogResult.OK)
                    {
                        LabelNode l = labelSelector.SelectedLabel;
                        if (l.ParentID > -1)
                        {
                            LabelledROI lroi = (LabelledROI)dgvLabels.SelectedRows[0].Tag;
                            lroi.LabelID = l.ID;
                            lroi.LabelName = l.Name;
                            if (Program.ImageDatabase.BBoxLabels_UpdateLabel(lroi))
                            {
                                dgvLabels.SelectedRows[0].Cells["Label"].Value = l.Name;
                            }
                        }
                    }
                    dgvLabels.EndEdit();
                    SendKeys.Send("{ENTER}");
                }
            }
        }
        private void _ROISelector_ROIDeleted(ROISelector sender, int index)
        {
            int id = LabelledImage.LabelledROIs[index].ID;

            if (Program.ImageDatabase.BBoxLabels_DeleteLabelByID(id))
            {
                LabelledImage.LabelledROIs.RemoveAt(index);
                PaintLabelledImageInfo(LabelledImage);
            }
            
        }
        private void _ROISelector_ROIChanged(ROISelector sender, int index, Rectangle roi)
        {
            LabelledImage.LabelledROIs[index].ROI = roi;
            Program.ImageDatabase.BBoxLabels_UpdateLabel(LabelledImage.LabelledROIs[index]);
            UpdateROIFieldsInLabelGrid(LabelledImage.LabelledROIs[index], index);
        }

        private void _ROISelector_NewROIAdded(ROISelector sender, Rectangle roi)
        {
            LabelledROI newLabelledROI = Program.ImageDatabase.BBoxLabels_AddLabel(LabelledImage.ID, roi);
            if (newLabelledROI != null)
            {
                LabelledImage.LabelledROIs.Add(newLabelledROI);
                AddLabelToGrid(newLabelledROI);
            }
        }

        private void _ROISelector_ROISelectionChanged(ROISelector sender, int index)
        {
            for (int i = 0; i < dgvLabels.Rows.Count; i++)
            {
                dgvLabels.Rows[i].Selected = (i == index);
            }
        }
        private void btnSelectLabel_Click(object sender, EventArgs e)
        {
            fLabelSelector labelSelector = new fLabelSelector();
            if (labelSelector.ShowDialog() == DialogResult.OK)
            {
                LabelNode l = labelSelector.SelectedLabel;
                if (l.ParentID > -1)
                {
                    LabelledImage.LabelID = l.ID;
                    LabelledImage.LabelName = l.Name;
                    txtLabel.Text = l.Name;
                    if (Program.ImageDatabase.Images_Update(LabelledImage))
                    {
                        dgvLabels.SelectedRows[0].Cells["Label"].Value = l.Name;
                    }
                }
            }
        }

        private void txtLabel_DoubleClick(object sender, EventArgs e)
        {
            btnSelectLabel_Click(sender, e);
        }
        #endregion

        public LabelledImage LabelledImage { get; private set; } = null;

        public void SetLabelledImage(LabelledImage image)
        {
            LabelledImage = image;
            PaintLabelledImageInfo(LabelledImage);
        }

        private async void PaintLabelledImageInfo(LabelledImage image)
        {
            _PaintDataInProgress = true; // This allows code elsewhere to distinguish between user changes and prgorammatic changes to the controls

            if (image != null)
            {
                Bitmap frame = null;

                frame = (Bitmap)(await Program.ImageDatabase.Images_LoadImageFile(image));

                List<Rectangle> rois = new List<Rectangle>();

                

                foreach (LabelledROI lroi in image.LabelledROIs)
                {
                    rois.Add(lroi.ROI);
                }

                _ROISelector.LinkToLabelledImage(rois, frame);
                txtLabel.Text = image.LabelName;
                tbxTags.PopulateTagsFromString(image.Tags);
                cmbSensorType.SelectedItem = image.SensorType;
                PaintROIsGrid(image);
            }
            else
            {
                _ROISelector.LinkToLabelledImage(new List<Rectangle>(), null);
                txtLabel.Text = "";
                tbxTags.PopulateTagsFromString("");
                cmbSensorType.SelectedItem = SensorTypeEnum.Unknown;
                dgvLabels.Rows.Clear();
            }
            

            _PaintDataInProgress = false;
        }

        private void PaintROIsGrid(LabelledImage image)
        {
            dgvLabels.Rows.Clear();

            foreach (LabelledROI lroi in image.LabelledROIs)
            {
                AddLabelToGrid(lroi);
            }
        }

        private void AddLabelToGrid(LabelledROI lroi)
        {
            string loc = lroi.ROI.X.ToString() + ", " + lroi.ROI.Y.ToString();
            string s = lroi.ROI.Width.ToString() + ", " + lroi.ROI.Height.ToString();
            dgvLabels.Rows.Add(new object[] { lroi.LabelName, lroi.Truncated, lroi.Occluded, loc, s });
            dgvLabels.Rows[dgvLabels.Rows.Count - 1].Tag = lroi;
        }

        private void UpdateROIFieldsInLabelGrid(LabelledROI lroi, int index)
        {
            string loc = lroi.ROI.X.ToString() + ", " + lroi.ROI.Y.ToString();
            string s = lroi.ROI.Width.ToString() + ", " + lroi.ROI.Height.ToString();

            dgvLabels.Rows[index].Cells["ROILocation"].Value = loc;
            dgvLabels.Rows[index].Cells["ROISize"].Value = s;
        }

        
    }

   
}

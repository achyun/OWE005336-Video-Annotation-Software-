using LabellingDB;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OWE005336__Video_Annotation_Software_
{
    public partial class LabelTree : TreeView
    {
        LastTreeStateSettings _LastTreeStateSettings = new LastTreeStateSettings();
        ContextMenuStrip _ContextMenu = new ContextMenuStrip();
        ToolStripMenuItem _AddFamilyButton = new ToolStripMenuItem("Add Family");
        ToolStripMenuItem _ImportFamilyButton = new ToolStripMenuItem("Import Family");
        ToolStripMenuItem _AddLabelButton = new ToolStripMenuItem("Add Label");
        ToolStripMenuItem _RenameButton = new ToolStripMenuItem("Rename");
        ToolStripMenuItem _DeleteButton = new ToolStripMenuItem("Delete");
        ToolStripMenuItem _ShortcutButton = new ToolStripMenuItem("Set Shortcut");

        public LabelTree()
        {
            InitializeComponent();
            _ContextMenu.Items.Add(_AddFamilyButton);
            _ContextMenu.Items.Add(_ImportFamilyButton);
            _ContextMenu.Items.Add(new ToolStripSeparator());
            _ContextMenu.Items.Add(_AddLabelButton);
            _ContextMenu.Items.Add(_RenameButton);
            _ContextMenu.Items.Add(_DeleteButton);
            _ContextMenu.Items.Add(new ToolStripSeparator());
            _ContextMenu.Items.Add(_ShortcutButton);

            for (int i = 0; i < 10; i++)
            {
                LabelNode label;
                string labelName = "";
                if (Program.LabelShortcuts[i] > -1)
                { 
                    label = Program.ImageDatabase.LabelTree_LoadByID(Program.LabelShortcuts[i]);
                    if (label != null) { labelName = " (" + label.Name + ")"; }
                }
                ToolStripMenuItem tsmi = new ToolStripMenuItem(i.ToString() + labelName);
                tsmi.Tag = i;
                tsmi.Click += ShortCutMenuItem_Click;
                _ShortcutButton.DropDownItems.Add(tsmi);
            }

            this.ContextMenuStrip = _ContextMenu;
            this.ImageList = imgLabels;

            _ContextMenu.Opening += _ContextMenu_Opening;
            _AddFamilyButton.Click += _AddFamilyButton_Click;
            _ImportFamilyButton.Click += _ImportFamilyButton_Click;
            _AddLabelButton.Click += _AddButton_Click;
            _RenameButton.Click += _RenameButton_Click;
            _DeleteButton.Click += _DeleteButton_Click;

            this.MouseDown += LabelTree_MouseDown;
        }

        public void SaveTreeState()
        {
            if (this.SelectedNode != null)
            {
                _LastTreeStateSettings.SelectedNodeID = ((LabelNode)this.SelectedNode.Tag).ID;
            }
            else
            {
                _LastTreeStateSettings.SelectedNodeID = -1;
            }

            if (_LastTreeStateSettings.ExpandedNodeIDs == null) { _LastTreeStateSettings.ExpandedNodeIDs = new List<int>(); }
            else { _LastTreeStateSettings.ExpandedNodeIDs.Clear(); }

            if (this.Nodes != null)
            {
                foreach (TreeNode n in this.Nodes)
                {
                    if (n.IsExpanded)
                    {
                        _LastTreeStateSettings.ExpandedNodeIDs.Add(((LabelNode)n.Tag).ID);
                        RecursivelyCheckForExpandedNodes(n);
                    }
                }
            }

            _LastTreeStateSettings.Save();
        }

        private void RecursivelyCheckForExpandedNodes(TreeNode node)
        {
            foreach (TreeNode n in node.Nodes)
            {
                if (n.IsExpanded)
                {
                    _LastTreeStateSettings.ExpandedNodeIDs.Add(((LabelNode)n.Tag).ID);
                    RecursivelyCheckForExpandedNodes(n);
                }
            }
        }

        private void ShortCutMenuItem_Click(object sender, EventArgs e)
        {
            int index = (int)(((ToolStripMenuItem)sender).Tag);
            LabelNode ln = (LabelNode)this.SelectedNode.Tag;
            Program.LabelShortcuts[index] = ln.ID;

            string labelName = " (" + ln.Name + ")";
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            tsmi.Text = index.ToString() + labelName;

            Properties.Settings.Default.Shortcuts = String.Join(",", Program.LabelShortcuts);
            Properties.Settings.Default.Save();
        }

        private void LabelTree_MouseDown(object sender, MouseEventArgs e)
        {
            this.SelectedNode = this.GetNodeAt(this.PointToClient(Control.MousePosition));
        }

        private void _ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (this.SelectedNode == null)
            {
                _AddFamilyButton.Enabled = true;
                _ImportFamilyButton.Enabled = true;
                _AddLabelButton.Enabled = false;
                _RenameButton.Enabled = false;
                _DeleteButton.Enabled = false;
                _ShortcutButton.Enabled = false;
            }
            else
            {
                _AddFamilyButton.Enabled = false;
                _ImportFamilyButton.Enabled = false;
                _AddLabelButton.Enabled = true;
                _RenameButton.Enabled = true;
                _DeleteButton.Enabled = true;

                LabelNode ln = (LabelNode)this.SelectedNode.Tag;
                if (ln.ParentID == -1) { _ShortcutButton.Enabled = false; }
                else { _ShortcutButton.Enabled = true; }
            }
        }

        private void _AddFamilyButton_Click(object sender, EventArgs e)
        {
            using (fGetString getString = new fGetString("Labelling Scheme Name"))
            {
                if (getString.ShowDialog() == DialogResult.OK)
                {
                    LabelNode newLabel = Program.ImageDatabase.LabelTree_AddLabel(-1, getString.ReturnString);
                    if (newLabel != null)
                    {
                        TreeNode n = new TreeNode(newLabel.Name);
                        n.Tag = newLabel;
                        n.ImageIndex = 0;
                        n.SelectedImageIndex = 0;
                        this.Nodes.Add(n);
                    }
                }
            }
        }

        private void _ImportFamilyButton_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Properties.Settings.Default.DefaultFileLocation;
                openFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    Properties.Settings.Default.DefaultFileLocation = System.IO.Path.GetDirectoryName(filePath);

                }
            }
        }

        private void _AddButton_Click(object sender, EventArgs e)
        {
            using (fGetString getString = new fGetString("Label Name"))
            {
                if (getString.ShowDialog() == DialogResult.OK)
                {
                    LabelNode parentLabel = (LabelNode)this.SelectedNode.Tag;
                    LabelNode newLabel = Program.ImageDatabase.LabelTree_AddLabel(parentLabel.ID, getString.ReturnString);
                    if (newLabel != null)
                    {
                        TreeNode n = new TreeNode(newLabel.Name);
                        n.ImageIndex = 2;
                        n.SelectedImageIndex = 2;
                        n.Tag = newLabel;
                        this.SelectedNode.Nodes.Add(n);
                        if (parentLabel.ParentID > -1)
                        {
                            this.SelectedNode.ImageIndex = 1;
                            this.SelectedNode.SelectedImageIndex = 1;
                        }

                    }
                }
            }
        }
        private void _RenameButton_Click(object sender, EventArgs e)
        {
            using (fGetString getString = new fGetString("Label Name"))
            {
                if (getString.ShowDialog() == DialogResult.OK)
                {
                    LabelNode n = (LabelNode)this.SelectedNode.Tag;
                    if (Program.ImageDatabase.LabelTree_RenameLabel(n.ID, getString.ReturnString))
                    {
                        n.Name = getString.ReturnString;
                        this.SelectedNode.Text = getString.ReturnString;
                    }
                }
            }
        }
        private void _DeleteButton_Click(object sender, EventArgs e)
        {
            LabelNode n = (LabelNode)this.SelectedNode.Tag;
            if (Program.ImageDatabase.LabelTree_DeleteLabel(n))
            {
                TreeNode parentNode = this.SelectedNode.Parent;
                if (parentNode == null)
                {
                    this.Nodes.Remove(this.SelectedNode);
                }
                else
                {
                    parentNode.Nodes.Remove(this.SelectedNode);
                    LabelNode pl = (LabelNode)parentNode.Tag;
                    if (pl.ParentID > -1)
                    {
                        if (parentNode.Nodes.Count == 0) { parentNode.ImageIndex = 2; parentNode.SelectedImageIndex = 2; }
                    }

                }
            }
        }

        public void PopulateLabels()
        {
            var labels = Program.ImageDatabase.LabelTree_LoadAll();
            foreach (LabelNode lbl in labels)
            {
                var n = new TreeNode(lbl.Name);
                n.ImageIndex = 0;
                n.SelectedImageIndex = 0;
                n.Tag = lbl;
                RecursivelyAddTreeNodes(ref n, lbl);

                this.Nodes.Add(n);
                if (_LastTreeStateSettings.ExpandedNodeIDs != null)
                {
                    if (_LastTreeStateSettings.ExpandedNodeIDs.Contains(lbl.ID)) { n.Expand(); }
                    if (_LastTreeStateSettings.SelectedNodeID == lbl.ID) { this.SelectedNode = n; }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void RecursivelyAddTreeNodes(ref TreeNode node, LabelNode label)
        {
            foreach (LabelNode c in label.Children)
            {
                TreeNode n = new TreeNode(c.Name);
                n.Tag = c;
                if (c.Children.Count > 0) { n.ImageIndex = 1; n.SelectedImageIndex = 1; }
                else { n.ImageIndex = 2; n.SelectedImageIndex = 2; }
                RecursivelyAddTreeNodes(ref n, c);
                
                node.Nodes.Add(n);
                if (_LastTreeStateSettings.ExpandedNodeIDs != null)
                {
                    if (_LastTreeStateSettings.ExpandedNodeIDs.Contains(c.ID)) { n.Expand(); }
                    if (_LastTreeStateSettings.SelectedNodeID == c.ID) { this.SelectedNode = n; }
                }
            }
        }
    }
    sealed class LastTreeStateSettings : ApplicationSettingsBase
    {
        [UserScopedSettingAttribute()]
        //[DefaultSettingValueAttribute("null")]
        public List<int> ExpandedNodeIDs
        {
            get { return (List<int>)this["ExpandedNodeIDs"]; }
            set { this["ExpandedNodeIDs"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("-1")]
        public int SelectedNodeID
        {
            get { return (int)this["SelectedNodeID"]; }
            set { this["SelectedNodeID"] = value; }
        }
    }
}

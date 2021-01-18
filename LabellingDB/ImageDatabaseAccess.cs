using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;

namespace LabellingDB
{
    public class ImageDatabaseAccess
    {
        public const string SETTING_IMAGE_DIR = "image_dir";
        public const string SETTING_VIDEO_ARCHIVE_DIR = "video_archive_dir";
        public const string SETTING_PROCESSED_FILE_ARCHIVE_DIR = "processed_file_archive_dir";
        string _ConnectionString = null;

        public ImageDatabaseAccess()
        {
         
        }

        public bool VerifyLoginCredentials(string server, string database, string userName, string password)
        {
            bool success = false;
            _ConnectionString = "Data Source=" + server + "; Database=" + database + "; User Id=" + userName + "; Password=" + password + "; Integrated Security=False; Persist Security Info=False; Pooling=False;MultipleActiveResultSets=False; Connect Timeout=60; Encrypt=False; TrustServerCertificate=False";
            SqlConnection conn = new SqlConnection(_ConnectionString);
            try
            {
                conn.Open();
                success = true;
            }
            catch { }
            finally { conn.Close(); }

            if (success)
            {
                // Check that the label tree Lft/Rgt structure is ok
                if (LabelTree_TreeHasInvalidLeftRightValues()) { LabelTree_ResetLeftRightValues(); }
            }

            return success;
        }

        public string GetCurrentUser()
        {
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand("SELECT CURRENT_USER", conn);
            string currentUser = "";

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    currentUser = rdr.GetString(0);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'GetCurrentUser' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            return currentUser;
        }

        private string GetTodaysDirName()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public bool MoveFileToArchive(string sourceFileName)
        {
            bool success = false;
            string imageDirPath = Settings_Get(SETTING_PROCESSED_FILE_ARCHIVE_DIR);
            string todaysDirPath = GetTodaysDirName();
            string fullDirPath = Path.Combine(imageDirPath, todaysDirPath);

            Directory.CreateDirectory(fullDirPath);

            try
            {
                File.Move(sourceFileName, Path.Combine(fullDirPath, Path.GetFileName(sourceFileName)));
                success = true;
            }
            catch { }

            return success;
        }

        public List<string> CheckForMissingFiles()
        {
            List<string> missingFiles = new List<string>();
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT filepath FROM images ORDER BY filepath", conn);
            SqlDataReader rdr = null;
            string dirPath = Settings_Get(SETTING_IMAGE_DIR);
            string filePath;
            cmd.CommandType = CommandType.Text;

            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    filePath = Path.Combine(dirPath, rdr.GetString(0));
                    if (!File.Exists(filePath))
                    {
                        missingFiles.Add(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'CheckForMissingFiles' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            return missingFiles;
        }

        public void CropImages(string filepathFilter, Size filterSize, Size croppedSize)
        {
            List<string> imgsToCrop = new List<string>();
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT images.id, filepath, MIN(bbox_labels.location_x) AS 'minx', MAX(bbox_labels.location_x + bbox_labels.size_width) AS 'maxx', MIN(bbox_labels.location_y) AS 'miny', MAX(bbox_labels.location_y + bbox_labels.size_height) AS 'maxy', images.rand_id " +
                                                "FROM images, bbox_labels " +
                                                "WHERE bbox_labels.image_id = images.id " +
                                                    "AND images.filepath LIKE @filepath " +
                                                    "AND image_width = @filter_width AND image_height = @filter_height " +
                                                    "GROUP BY images.id, images.filepath, images.rand_id", conn);
            //SqlCommand cmd = new SqlCommand("SELECT * " + //images.id, filepath, bbox_labels.location_x AS 'minx', bbox_labels.location_x + bbox_labels.size_width AS 'maxx', bbox_labels.location_y AS 'miny', bbox_labels.location_y + bbox_labels.size_height AS 'maxy', images.rand_id " +
            //                                    "FROM images, bbox_labels " +
            //                                    "WHERE bbox_labels.image_id = images.id " +
            //                                        "AND images.filepath LIKE @filepath " +
            //                                        "AND image_width = @filter_width AND image_height = @filter_height", conn);
            //SqlCommand cmd = new SqlCommand("SELECT * " + //images.id, filepath, bbox_labels.location_x AS 'minx', bbox_labels.location_x + bbox_labels.size_width AS 'maxx', bbox_labels.location_y AS 'miny', bbox_labels.location_y + bbox_labels.size_height AS 'maxy', images.rand_id " +
            //                                    "FROM images " +
            //                                    "WHERE images.filepath LIKE @filepath " +
            //                                        "AND image_width = @filter_width AND image_height = @filter_height", conn);

            SqlDataReader rdr = null;
            string dirPath = Settings_Get(SETTING_IMAGE_DIR);
            string filePath;
            int id, minX, maxX, minY, maxY;
            double randID;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@filepath", filepathFilter);
            cmd.Parameters.AddWithValue("@filter_width", filterSize.Width);
            cmd.Parameters.AddWithValue("@filter_height", filterSize.Height);

            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    id = rdr.GetInt32(0);
                    filePath = Path.Combine(dirPath, rdr.GetString(1));
                    minX = rdr.GetInt32(2);
                    maxX = rdr.GetInt32(3);
                    minY = rdr.GetInt32(4);
                    maxY = rdr.GetInt32(5);
                    randID = rdr.GetDouble(6);
                    if (File.Exists(filePath))
                    {
                        System.Diagnostics.Debug.WriteLine(filePath);
                        CropImage(id, filePath, filterSize, minX, maxX, minY, maxY, randID, croppedSize);
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                str += "Test";
                int i = 4;
                if (i > str.Length)
                    i += 3;
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }
        }

        private void CropImage(int imageID, string filepath, Size imgSize, int minX, int maxX, int minY, int maxY, double randID, Size cropSize)
        {
            int cropRightMax = Math.Max(Math.Min(minX + cropSize.Width, imgSize.Width), maxX);
            int cropLeftMin = Math.Min(Math.Max(maxX - cropSize.Width, 0), minX);
            int cropBottomMax = Math.Max(Math.Min(minY + cropSize.Height, imgSize.Height), maxY);
            int cropTopMin = Math.Min(Math.Max(maxY - cropSize.Height, 0), minY);
            int cropLeft, cropRight, cropTop, cropBottom;
            int deltaX = cropRightMax - cropLeftMin;
            int deltaY = cropBottomMax - cropTopMin;
            int newWidth, newHeight;

            if (deltaX > cropSize.Width)
            {
                cropLeft = Math.Min(cropLeftMin + (int)(randID * (deltaX - cropSize.Width)), minX);
                cropRight = Math.Max(cropLeft + cropSize.Width, maxX + 1);
                newWidth = cropRight - cropLeft;
            }
            else
            {
                cropLeft = cropLeftMin;
                cropRight = cropRightMax;
                newWidth = cropRight - cropLeft;
            }

            if (deltaY > cropSize.Height)
            {
                cropTop = Math.Min(cropTopMin + (int)(randID * (deltaY - cropSize.Height)), minY);
                cropBottom = Math.Max(cropTop + cropSize.Height, maxY + 1);
                newHeight = cropBottom - cropTop;
            }
            else
            {
                cropTop = cropTopMin;
                cropBottom = cropBottomMax;
                newHeight = cropBottom - cropTop;
            }

            byte[] b = File.ReadAllBytes(filepath);
            MemoryStream ms = new MemoryStream(b);
            Image origianlImage = Image.FromStream(ms);

            Image croppedImage = new Bitmap(newWidth, newHeight);
            Rectangle cropRect = new Rectangle(new Point(cropLeft, cropTop), new Size(newWidth, newHeight));
            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(origianlImage, new Rectangle(0, 0, croppedImage.Width, croppedImage.Height),
                               cropRect,
                               GraphicsUnit.Pixel);
            }

            try
            {
                System.Diagnostics.Debug.WriteLine(cropRect.ToString());
                croppedImage.Save(filepath);
                CropUpdateBboxLocations(imageID, cropLeft, cropTop, newWidth, newHeight);
            }
            catch (Exception ex) 
            {
            
            }
            
        }

        private void CropUpdateBboxLocations(int imageID, int x, int y, int width, int height)
        {
            SqlConnection conn = new SqlConnection(_ConnectionString);

            string commandStringBbox = "UPDATE bbox_labels SET bbox_labels.location_x = bbox_labels.location_x - @x, bbox_labels.location_y = bbox_labels.location_y - @y WHERE image_id = @image_id";
            string commandStringImgs = "UPDATE images SET image_width = @width, image_height = @height WHERE id = @image_id";
            SqlCommand cmdBbox = new SqlCommand(commandStringBbox, conn);
            SqlCommand cmdImgs = new SqlCommand(commandStringImgs, conn);
            cmdBbox.Parameters.Add("@image_id", SqlDbType.Int).Value = imageID;
            cmdBbox.Parameters.Add("@x", SqlDbType.Int).Value = x;
            cmdBbox.Parameters.Add("@y", SqlDbType.Int).Value = y;
            cmdImgs.Parameters.Add("@image_id", SqlDbType.Int).Value = imageID;
            cmdImgs.Parameters.Add("@width", SqlDbType.Int).Value = width;
            cmdImgs.Parameters.Add("@height", SqlDbType.Int).Value = height;

            cmdBbox.CommandType = CommandType.Text;
            cmdImgs.CommandType = CommandType.Text;

            try
            {
                conn.Open();

                
                cmdBbox.ExecuteNonQuery();
                cmdImgs.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'Images_Update' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }
        }

        #region "LabelTree"
        public List<LabelNode> LabelTree_LoadByParentID(int parentID = -1)
        {
            List<LabelNode> labels = new List<LabelNode>();
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand("SELECT id, name, lft, rgt FROM label_trees WHERE parent_id = @parent_id", conn);
            cmd.Parameters.AddWithValue("@parent_id", parentID);
            
            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    LabelNode ln = new LabelNode(rdr.GetInt32(0), rdr.GetString(1), parentID);
                    ln.Left = rdr.GetInt32(2);
                    ln.Right = rdr.GetInt32(3);
                    labels.Add(ln);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'LabelTree_LoadByParentID' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            return labels;
        }
        public LabelNode LabelTree_LoadByID(int id)
        {
            LabelNode label = null;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand("SELECT id, name, parent_id, lft, rgt FROM label_trees WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    label = new LabelNode(rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2));
                    label.Left = rdr.GetInt32(3);
                    label.Right = rdr.GetInt32(4);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'LabelTree_LoadByID' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            return label;
        }
        public List<LabelNode> LabelTree_LoadAll()
        {
            List<LabelNode> labels = LabelTree_LoadByParentID();

            for (int i = 0; i < labels.Count; i++)
            {
                LabelNode label = labels[i];
                LabelTree_RecursiveLoadLabels(ref label);
            }

            return labels;
        }
        public LabelNode LabelTree_AddLabel(int parentID, string name)
        {
            int new_id = -1;
            LabelNode newNode = null;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO label_trees (parent_id, name) VALUES(@parent_id, @name); " +
                                            "SELECT SCOPE_IDENTITY()", conn);
            SqlDataReader rdr = null;
            cmd.Parameters.Add("@parent_id", SqlDbType.Int).Value = parentID;
            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = name;

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    new_id = Decimal.ToInt32(rdr.GetDecimal(0));
                    newNode = new LabelNode(new_id, name, parentID);
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'LabelTree_AddLabel' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            LabelTree_ResetLeftRightValues();

            return newNode;
        }
        public bool LabelTree_UpdateLabel(LabelNode labelNode)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("UPDATE label_trees SET name = @name, parent_id = @parent_id WHERE id = @id", conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = labelNode.ID;
            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = labelNode.Name;
            cmd.Parameters.Add("@parent_id", SqlDbType.Int).Value = labelNode.ParentID;

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                success = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'LabelTree_RenameLabel' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            LabelTree_ResetLeftRightValues();

            return success;
        }
        public bool LabelTree_UpdateLeftRight(LabelNode node)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("UPDATE label_trees SET lft = @lft, rgt = @rgt WHERE id = @id", conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = node.ID;
            cmd.Parameters.Add("@lft", SqlDbType.Int).Value = node.Left;
            cmd.Parameters.Add("@rgt", SqlDbType.Int).Value = node.Right;

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                success = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'LabelTree_UpdateLeftRight' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return success;
        }
        public bool LabelTree_CheckIfLabelsAreUsed(LabelNode node)
        {
            return LabelTree_RecursiveCheckIfLabelsAreUsed(node);
        }

        private bool LabelTree_RecursiveCheckIfLabelsAreUsed(LabelNode node)
        {
            bool labelIsUsed = LabelTree_CheckifLabelIsUsed(node);

            if (!labelIsUsed)
            {
                node.Children = LabelTree_LoadByParentID(node.ID);

                foreach (LabelNode ln in node.Children)
                {
                    labelIsUsed = LabelTree_RecursiveCheckIfLabelsAreUsed(ln);
                    if (labelIsUsed) { break; }
                }
            }

            return labelIsUsed;
        }

        private bool LabelTree_CheckifLabelIsUsed(LabelNode node)
        {
            bool labelIsUsed = true;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(ID) FROM bbox_labels WHERE label_id = @id", conn);
            SqlDataReader rdr = null;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = node.ID;
            cmd.CommandType = CommandType.Text;

            try
            {
                conn.Open();
                
                rdr = cmd.ExecuteReader();
                
                if (rdr.Read())
                {
                    if (rdr.GetInt32(0) == 0) { labelIsUsed = false; }
                    else { labelIsUsed = true; }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'LabelTree_CheckifLabelIsUsed' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return labelIsUsed;
        }

        public bool LabelTree_DeleteLabel(LabelNode node)
        {
            bool success = false;

            success = LabelTree_DeleteLabelByID(node.ID);

            if (success)
            {
                success = LabelTree_RecursiveDeleteLabels(node);
            }

            LabelTree_ResetLeftRightValues();

            return success;
        }
        private void LabelTree_RecursiveLoadLabels(ref LabelNode node)
        {
            node.Children = LabelTree_LoadByParentID(node.ID);

            for (int i = 0; i < node.Children.Count; i++)
            {
                LabelNode child = node.Children[i];
                LabelTree_RecursiveLoadLabels(ref child);
            }
        }
        private bool LabelTree_RecursiveDeleteLabels(LabelNode node)
        {
            bool success = false;

            // Load all of this node's children so we can iterate through them and delete their children.
            node.Children = LabelTree_LoadByParentID(node.ID);

            // Delete all this node's children.
            success = LabelTree_DeleteLabelsByParentID(node.ID);

            if (success)
            {
                // Iterate through the children, deleting all of their children, etc...
                for (int i = 0; i < node.Children.Count; i++)
                {
                    LabelNode child = node.Children[i];
                    success = LabelTree_RecursiveDeleteLabels(child);
                }
            }
            
            return success;
        }
        public List<LabelNode> LabelTree_LoadLabelList()
        {
            List<LabelNode> labels = new List<LabelNode>();
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand("SELECT id, name, parent_id FROM label_trees WHERE parent_id > -1", conn);

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    labels.Add(new LabelNode(rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2)));
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'LabelTree_LoadLabelList' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            return labels;
        }
        private bool LabelTree_DeleteLabelByID(int id)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM label_trees WHERE id = @id", conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                success = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'LabelTree_DeleteLabelByID' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return success;
        }
        private bool LabelTree_DeleteLabelsByParentID(int parentID)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM label_trees WHERE parent_id = @parent_id", conn);
            cmd.Parameters.Add("@parent_id", SqlDbType.Int).Value = parentID;

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                success = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'LabelTree_DeleteLabelsByParentID' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return success;
        }
        private bool LabelTree_TreeHasInvalidLeftRightValues()
        {
            bool invalid = true;

            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand("SELECT COUNT(id) FROM label_trees WHERE lft = 0 OR rgt = 0", conn);

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (rdr.GetInt32(0) == 0) { invalid = false; }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'LabelTree_TreeHasInvalidLeftRightValues' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            return invalid;
        }
        private bool LabelTree_ResetLeftRightValues()
        {
            bool success = true;
            int index = 1;
            List<LabelNode> topLevelFolders = LabelTree_LoadAll();

            foreach (LabelNode ln in topLevelFolders)
            {
                success = LabelTree_RecursivelySetLeftRightValues(ln, ref index);
                if (!success) { break; }
            }

            return success;
        }
        private bool LabelTree_RecursivelySetLeftRightValues(LabelNode node, ref int index)
        {
            bool success = true;

            node.Left = index++;
            foreach (LabelNode ln in node.Children)
            {
                success = LabelTree_RecursivelySetLeftRightValues(ln, ref index);
                if (!success) { break; }
            }
            node.Right = index++;

            LabelTree_UpdateLeftRight(node);

            return success;
        }
        #endregion

        #region "Settings"
        public string Settings_Get(string settingName)
        {
            string settingValue = null;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand("SELECT setting_value FROM settings WHERE setting_name = @setting_name", conn);
            cmd.Parameters.AddWithValue("@setting_name", settingName);
            cmd.CommandType = CommandType.Text;

            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    settingValue = rdr.GetString(0);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'Settings_Get' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            return settingValue;
        }

        public bool Settings_Set(string settingName, string settingValue)
        {
            bool success = false;
            string value = Settings_Get(settingName);

            if (value != null)
            {
                success = Settings_Modify(settingName, settingValue);
            }
            else
            {
                success = Settings_Create(settingName, settingValue);
            }

            return success;
        }

        private bool Settings_Modify(string settingName, string settingValue)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("UPDATE settings SET setting_value = @setting_value WHERE setting_name = @setting_name", conn);
            cmd.Parameters.AddWithValue("@setting_value", settingValue);
            cmd.Parameters.AddWithValue("@setting_name", settingName);
            cmd.CommandType = CommandType.Text;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'Settings_Modify' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }
            return success;
        }

        private bool Settings_Create(string settingName, string settingValue)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO settings (setting_name, setting_value) VALUES(@setting_name, @setting_value)", conn);
            cmd.Parameters.AddWithValue("@setting_value", settingValue);
            cmd.Parameters.AddWithValue("@setting_name", settingName);
            cmd.CommandType = CommandType.Text;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'Settings_Create' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }
            return success;
        }

        //private string Settings_GetDestImageDir()
        //{
        //    string todaysFolderName = DateTime.Now.ToString("yyyy-MM-dd");
        //    string imgDir = Settings_Get(ImageDatabaseAccess.SETTING_IMAGE_DIR);
        //    string fullDirPath = Path.Combine(imgDir, todaysFolderName);
        //    Directory.CreateDirectory(fullDirPath); // This includes a check to see if it already exists

        //    return fullDirPath;
        //}

        //private string Settings_GetDestVideoArchiveDir()
        //{
        //    string todaysFolderName = DateTime.Now.ToString("yyyy-MM-dd");
        //    string imgDir = Settings_Get(ImageDatabaseAccess.SETTING_VIDEO_ARCHIVE_DIR);
        //    string fullDirPath = Path.Combine(imgDir, todaysFolderName);
        //    Directory.CreateDirectory(fullDirPath); // This includes a check to see if it already exists

        //    return fullDirPath;
        //}

        //private string Settings_GetDestProcessedFileArchiveDir()
        //{
        //    string todaysFolderName = DateTime.Now.ToString("yyyy-MM-dd");
        //    string imgDir = Settings_Get(ImageDatabaseAccess.SETTING_PROCESSED_FILE_ARCHIVE_DIR);
        //    string fullDirPath = Path.Combine(imgDir, todaysFolderName);
        //    Directory.CreateDirectory(fullDirPath); // This includes a check to see if it already exists

        //    return fullDirPath;
        //}

        #endregion

        #region "Videos"
        public Task<Video> Videos_AddToVideoArchive(string sourceFliePath, Size videoSize, SensorTypeEnum sensorType = SensorTypeEnum.Unknown, int labelID = -1, string tags = "")
        {
            return Task.Run(() =>
            {
                Video video = null;

                if (File.Exists(sourceFliePath))
                {
                    string videoDirPath = Settings_Get(SETTING_VIDEO_ARCHIVE_DIR);
                    string todaysDirPath = GetTodaysDirName();
                    string fullDirPath = Path.Combine(videoDirPath, todaysDirPath);
                    string fileName = Path.GetFileName(sourceFliePath);

                    string dbPath = Path.Combine(todaysDirPath, fileName);
                    string newFilePath = Path.Combine(fullDirPath, fileName);

                    Directory.CreateDirectory(fullDirPath);

                    try
                    {
                        File.Copy(sourceFliePath, newFilePath);
                        video = Videos_AddDBEntry(dbPath, videoSize, sensorType, labelID, tags);
                    }
                    catch (Exception ex) { }
                }

                return video;
            });
        }
        private Video Videos_AddDBEntry(string fileName, Size imageSize, SensorTypeEnum sensorType, int labelID, string tags)
        {
            Video video = null;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO videos (filepath, sensor_type, label_id, tags, video_width, video_height) VALUES(@filepath, @sensor_type, @label_id, @tags, @video_width, @video_height); " +
                                            "SELECT SCOPE_IDENTITY()", conn);
            SqlDataReader rdr = null;

            cmd.Parameters.AddWithValue("@filepath", fileName);
            cmd.Parameters.AddWithValue("@sensor_type", sensorType);
            cmd.Parameters.AddWithValue("@label_id", labelID);
            cmd.Parameters.AddWithValue("@video_width", imageSize.Width);
            cmd.Parameters.AddWithValue("@video_height", imageSize.Height);
            cmd.Parameters.Add("@tags", SqlDbType.NVarChar).Value = tags;
            cmd.CommandType = CommandType.Text;

            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    video = new Video(imageSize);
                    video.ID = Decimal.ToInt32(rdr.GetDecimal(0));
                    video.Filepath = fileName;
                    video.SensorType = sensorType;
                    video.Tags = tags;
                    video.LabelID = labelID;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'Images_AddDBEntry' (" + fileName + ") : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return video;
        }
        #endregion

        #region "Images"
        public Task<LabelledImage> Images_Add(string sourceFliePath, bool archiveSourceFile, SensorTypeEnum sensorType = SensorTypeEnum.Unknown, int labelID = -1, string tags = "")
        {
            return Task.Run(() => {
                LabelledImage lImage = null;

                if (File.Exists(sourceFliePath))
                {
                    string imageDirPath = Settings_Get(SETTING_IMAGE_DIR);
                    string todaysDirPath = GetTodaysDirName();
                    string fullDirPath = Path.Combine(imageDirPath, todaysDirPath);
                    string fileName = Path.GetFileName(sourceFliePath);

                    string dbPath = Path.Combine(todaysDirPath, fileName);
                    string newFilePath = Path.Combine(fullDirPath, fileName);

                    Directory.CreateDirectory(fullDirPath);
                    
                    try
                    {
                        Image img = Image.FromFile(sourceFliePath);
                        Size imgSize = img.Size;
                        img.Dispose();

                        File.Copy(sourceFliePath, newFilePath);
                        lImage = Images_AddDBEntry(dbPath, imgSize, sensorType, labelID, tags);
                        if (archiveSourceFile)
                        {
                            MoveFileToArchive(sourceFliePath);
                        }
                    }
                    catch (Exception ex) 
                    { }
                }

                return lImage;
            });
        }
        public Task<LabelledImage> Images_Add(Image img, string fileName, SensorTypeEnum sensorType = SensorTypeEnum.Unknown, int labelID = -1, string tags = "")
        {
            return Task.Run(() => {
                LabelledImage lImage = null;

                if (img != null)
                {
                    string imageDirPath = Settings_Get(SETTING_IMAGE_DIR);
                    string todaysDirPath = GetTodaysDirName();
                    string fullDirPath = Path.Combine(imageDirPath, todaysDirPath);

                    string dbPath = Path.Combine(todaysDirPath, fileName);
                    string newFilePath = Path.Combine(fullDirPath, fileName);

                    Directory.CreateDirectory(fullDirPath);

                    try
                    {
                        Bitmap bmp = new Bitmap(img);
                        bmp.Save(newFilePath);
                        lImage = Images_AddDBEntry(dbPath, img.Size, sensorType, labelID, tags);
                    }
                    catch { }
                }

                return lImage;
            });
        }
        private LabelledImage Images_AddDBEntry(string fileName, Size imageSize, SensorTypeEnum sensorType, int labelID, string tags)
        {
            LabelledImage limg = null;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO images (filepath, sensor_type, label_id, tags, image_width, image_height) VALUES(@filepath, @sensor_type, @label_id, @tags, @image_width, @image_height); " +
                                            "SELECT SCOPE_IDENTITY()", conn);
            SqlDataReader rdr = null;

            cmd.Parameters.AddWithValue("@filepath", fileName);
            cmd.Parameters.AddWithValue("@sensor_type", sensorType);
            cmd.Parameters.AddWithValue("@label_id", labelID);
            cmd.Parameters.AddWithValue("@image_width", imageSize.Width);
            cmd.Parameters.AddWithValue("@image_height", imageSize.Height);
            cmd.Parameters.Add("@tags", SqlDbType.NVarChar).Value = tags;
            cmd.CommandType = CommandType.Text;

            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    limg = new LabelledImage(imageSize);
                    limg.ID = Decimal.ToInt32(rdr.GetDecimal(0));
                    limg.Filepath = fileName;
                    limg.SensorType = sensorType;
                    limg.Tags = tags;
                    limg.LabelID = labelID;

                    if (labelID > -1)
                    {
                        LabelNode ln = LabelTree_LoadByID(labelID);
                        limg.LabelName = ln.Name;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'Images_AddDBEntry' (" + fileName + ") : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return limg;
        }
        public List<LabelledImage> Images_Get(bool filterForIncomplete, bool filterForNoLabels, bool filterForThisUser, bool filterByLabel, int labelID, string filePathSearchString, int maxResults = 30)
        {
            DataTable dt = new DataTable();
            List<LabelledImage> imageList = new List<LabelledImage>();
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("SELECT TOP " + maxResults.ToString() + " images.id, filepath, labelling_complete, sensor_type, tags, label_id, image_width, image_height, label_trees.name FROM images LEFT JOIN label_trees ON label_trees.id = images.label_id", conn);
            int filterCount = 0;
            bool andRequired = false;

            if (filterForIncomplete) { filterCount++; }
            if (filterForNoLabels) { filterCount++; }
            if (filterForThisUser) { filterCount++; }
            if (filterByLabel) { filterCount++; }
            if (filePathSearchString != "") { filterCount++; }

            if (filterCount > 0) { cmd.CommandText += " WHERE"; }

            if (filterForIncomplete)
            { 
                cmd.CommandText += " labelling_complete = @labelling_complete";
                cmd.Parameters.Add("@labelling_complete", SqlDbType.Bit).Value = !filterForIncomplete;
                filterCount--;
                andRequired = true;
            }

            if (filterForNoLabels)
            {
                if (andRequired) { cmd.CommandText += " AND"; }
                cmd.CommandText += " (SELECT COUNT(id) FROM bbox_labels WHERE bbox_labels.image_id = images.id) = 0";
                filterCount--;
                andRequired = true;
            }

            if (filterForThisUser)
            {
                if (andRequired) { cmd.CommandText += " AND"; }
                cmd.CommandText += " images.created_by = CURRENT_USER";
                filterCount--;
                andRequired = true;
            }

            if (filterByLabel)
            {
                if (andRequired) { cmd.CommandText += " AND"; }
                cmd.CommandText += " (SELECT COUNT(bbox_labels.id) FROM bbox_labels WHERE bbox_labels.label_id = @label_id AND bbox_labels.image_id = images.id) > 0";
                cmd.Parameters.Add("@label_id", SqlDbType.Int).Value = labelID;
                filterCount--;
                andRequired = true;
            }

            if (filePathSearchString != "")
            {
                if (andRequired) { cmd.CommandText += " AND"; }
                cmd.CommandText += " images.filepath LIKE @search_string";
                cmd.Parameters.Add("@search_string", SqlDbType.NVarChar).Value = filePathSearchString;
                filterCount--;
                andRequired = true;
            }

            cmd.CommandText += " ORDER BY images.id";
            if (!filterForIncomplete) { cmd.CommandText += " DESC"; }

            adapter.SelectCommand = cmd;           
            cmd.CommandType = CommandType.Text;

            try
            {
                conn.Open();
                adapter.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    int width = (int)dr.ItemArray[6];
                    int height = (int)dr.ItemArray[7];
                    LabelledImage img = new LabelledImage(new Size(width, height));
                    
                    img.ID = (int)(dr.ItemArray[0]);
                    img.Filepath = (string)(dr.ItemArray[1]);
                    img.Completed = (bool)(dr.ItemArray[2]);
                    img.SensorType = (SensorTypeEnum)(dr.ItemArray[3]);
                    img.Tags = (string)(dr.ItemArray[4]);
                    img.LabelID = (int)dr.ItemArray[5];
                    if (!dr.ItemArray[8].Equals(System.DBNull.Value)) { img.LabelName = (string)dr.ItemArray[8]; }
                    imageList.Add(img);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'Images_Get' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                adapter.Dispose();
            }

            return imageList;
        }

        public LabelledImage Images_SearchByFileName(string fileName)
        {
            LabelledImage lImg = null;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand("SELECT id, filepath, image_width, image_height FROM images WHERE filepath LIKE @file_path", conn);
            cmd.Parameters.Add("@file_path", SqlDbType.NVarChar).Value = fileName;

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    int width = rdr.GetInt32(2);
                    int height = rdr.GetInt32(3);
                    lImg = new LabelledImage(new Size(width, height));
                    lImg.ID = rdr.GetInt32(0);
                    lImg.Filepath = rdr.GetString(1);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'Images_SearchByFileName' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            return lImg;
        }
        public Task<Image> Images_LoadImageFile(LabelledImage lImg)
        {         

            return Task.Run(() => {
                Image image = null;
                string dirPath = Settings_Get(SETTING_IMAGE_DIR);
                string fullFilePath = Path.Combine(dirPath, lImg.Filepath);

                try
                {
                    byte[] b = File.ReadAllBytes(fullFilePath);
                    MemoryStream ms = new MemoryStream(b);
                    image = Image.FromStream(ms);
                    if (image.Width != lImg.ImageSize.Width || image.Height != lImg.ImageSize.Height)
                    {
                        lImg.ImageSize = new Size(image.Width, image.Height);
                        Images_UpdateSize(lImg);
                    }
                }
                catch { }

                return image;
            });            
        }
        public Task<Image> Images_LoadImageThumbnail(LabelledImage lImg, Size maxSize)
        {
            return Task.Run(async () => {
                Task<Image> imageTask = Images_LoadImageFile(lImg);
                Image image = await imageTask;
                if (image != null) { image = image.GetThumbnailImage(maxSize.Width, maxSize.Height, null, IntPtr.Zero); }

                return image;
            });
        }

        public Task<Image> Images_LoadImageThumbnail(string fileName, Size maxSize)
        {
            return Task.Run(async () => {
                LabelledImage lImg = Images_SearchByFileName(fileName);
                Task<Image> imageTask = Images_LoadImageFile(lImg);
                Image image = await imageTask;
                if (image != null) { image = image.GetThumbnailImage(maxSize.Width, maxSize.Height, null, IntPtr.Zero); }

                return image;
            });
        }
        private bool Images_GetCurrentCompletedStatus(LabelledImage lImg)
        {
            bool labelling_complete = false;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT labelling_complete FROM images WHERE id = @id", conn);
            SqlDataReader rdr = null;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = lImg.ID;
            cmd.CommandType = CommandType.Text;

            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    labelling_complete = rdr.GetBoolean(0);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'Images_Update' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            return labelling_complete;
        }
        public bool Images_Update(LabelledImage image)
        {
            bool success = false;
            bool oldCompleteStatus = Images_GetCurrentCompletedStatus(image);
            bool addCompleteDetails = (!oldCompleteStatus && image.Completed);
            SqlConnection conn = new SqlConnection(_ConnectionString);

            string commandString = "UPDATE images SET label_id = @label_id, sensor_type = @sensor_type, labelling_complete = @labelling_complete, tags = @tags, modified_by = CURRENT_USER, modified_date = CURRENT_TIMESTAMP";
            if(addCompleteDetails) { commandString += ", completed_by = CURRENT_USER, completed_date = CURRENT_TIMESTAMP"; }
            commandString += " WHERE id = @id";

            SqlCommand cmd = new SqlCommand(commandString, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = image.ID;
            cmd.Parameters.Add("@label_id", SqlDbType.Int).Value = image.LabelID;
            cmd.Parameters.Add("@sensor_type", SqlDbType.Int).Value = (int)image.SensorType;
            cmd.Parameters.Add("@labelling_complete", SqlDbType.Bit).Value = image.Completed;
            cmd.Parameters.Add("@tags", SqlDbType.NVarChar).Value = image.Tags;

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                success = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'Images_Update' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return success;
        }

        public bool Images_UpdateSize(LabelledImage lImg)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(_ConnectionString);

            string commandString = "UPDATE images SET image_width = @width, image_height = @height WHERE id = @id";

            SqlCommand cmd = new SqlCommand(commandString, conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = lImg.ID;
            cmd.Parameters.Add("@width", SqlDbType.Int).Value = lImg.ImageSize.Width;
            cmd.Parameters.Add("@height", SqlDbType.Int).Value = lImg.ImageSize.Height;

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                success = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'Images_UpdateSize' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return success;
        }
        public bool Images_DeleteImage(LabelledImage img)
        {
            bool success = false;
            string dirPath = Settings_Get(SETTING_IMAGE_DIR);
            string filePath = Path.Combine(dirPath, img.Filepath);

           
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                Images_DeleteDBEntry(img.ID);
                success = true;
            }
            catch
            {

            }
            return success;
        }
        public bool Images_DeleteDBEntry(int id)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM images WHERE id = @id", conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM bbox_labels WHERE image_id = @id";
                cmd.ExecuteNonQuery();

                success = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'Images_DeleteDBEntry' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return success;
        }
        #endregion

        #region "BBoxLabels"
        public List<LabelledROI> BBoxLabels_LoadByImageID(int imageID)
        {
            List<LabelledROI> labels = new List<LabelledROI>();
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand("SELECT bbox_labels.id, image_id, label_id, location_x, location_y, size_width, size_height, truncated, occluded, out_of_focus, label_trees.name FROM bbox_labels LEFT JOIN label_trees ON label_trees.id = bbox_labels.label_id WHERE bbox_labels.image_id = @image_id", conn);
            cmd.Parameters.AddWithValue("@image_id", imageID);

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int id = rdr.GetInt32(0);
                    Rectangle r = new Rectangle();
                    r.X = rdr.GetInt32(3);
                    r.Y = rdr.GetInt32(4);
                    r.Width = rdr.GetInt32(5);
                    r.Height = rdr.GetInt32(6);

                    LabelledROI lroi = new LabelledROI(id, imageID, r);
                    
                    lroi.LabelID = rdr.GetInt32(2);
                    lroi.Truncated = (bool)rdr.GetValue(7);
                    lroi.Occluded = (bool)rdr.GetValue(8);
                    lroi.OutOfFocus = (bool)rdr.GetValue(9);
                    lroi.ImageID = imageID;
                    if (rdr.GetValue(10) != DBNull.Value) { lroi.LabelName = rdr.GetString(10); }
                    
                    labels.Add(lroi);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'BBoxLabels_LoadByImageID' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }
            return labels;
        }

        public LabelledROI BBoxLabels_AddLabel(int imageID, Rectangle roi, int labelID = -1)
        {
            int newID = -1;
            LabelledROI lroi = null;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO bbox_labels (image_id, label_id, location_x, location_y, size_width, size_height) VALUES(@image_id, @label_id, @location_x, @location_y, @size_width, @size_height); " +
                                            "SELECT SCOPE_IDENTITY()", conn);
            SqlDataReader rdr = null;
            cmd.Parameters.Add("@image_id", SqlDbType.Int).Value = imageID;
            cmd.Parameters.Add("@label_id", SqlDbType.Int).Value = labelID;
            cmd.Parameters.Add("@location_x", SqlDbType.Int).Value = roi.X;
            cmd.Parameters.Add("@location_y", SqlDbType.Int).Value = roi.Y;
            cmd.Parameters.Add("@size_width", SqlDbType.Int).Value = roi.Width;
            cmd.Parameters.Add("@size_height", SqlDbType.Int).Value = roi.Height;

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    newID = Decimal.ToInt32(rdr.GetDecimal(0));
                    lroi = new LabelledROI(newID, imageID, roi);
                    lroi.LabelID = labelID; 
                    if (labelID > -1)
                    {
                        LabelNode ln = LabelTree_LoadByID(labelID);
                        lroi.LabelName = ln.Name;
                    }   
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'BBoxLabels_AddLabel' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            return lroi;
        }

        public bool BBoxLabels_UpdateLabel(LabelledROI lroi)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("UPDATE bbox_labels SET label_id = @label_id, location_x = @location_x, location_y = @location_y, size_width = @size_width, size_height = @size_height, truncated = @truncated, occluded = @occluded, out_of_focus = @out_of_focus WHERE id = @id; " +
                                            "UPDATE images SET modified_by = CURRENT_USER, modified_date = CURRENT_TIMESTAMP WHERE id = @image_id", conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = lroi.ID;
            cmd.Parameters.Add("@label_id", SqlDbType.Int).Value = lroi.LabelID;
            cmd.Parameters.Add("@image_id", SqlDbType.Int).Value = lroi.ImageID;
            cmd.Parameters.Add("@location_x", SqlDbType.Int).Value = lroi.ROI.X;
            cmd.Parameters.Add("@location_y", SqlDbType.Int).Value = lroi.ROI.Y;
            cmd.Parameters.Add("@size_width", SqlDbType.Int).Value = lroi.ROI.Width;
            cmd.Parameters.Add("@size_height", SqlDbType.Int).Value = lroi.ROI.Height;
            cmd.Parameters.Add("@truncated", SqlDbType.Bit).Value = lroi.Truncated;
            cmd.Parameters.Add("@occluded", SqlDbType.Bit).Value = lroi.Occluded;
            cmd.Parameters.Add("@out_of_focus", SqlDbType.Bit).Value = lroi.OutOfFocus;

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                success = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'BBoxLabels_UpdateLabel' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return success;
        }

        public bool BBoxLabels_DeleteLabelByID(int id)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM bbox_labels WHERE id = @id", conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                success = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'BBoxLabels_DeleteLabelByID' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return success;
        }

        public bool BBoxLabels_DeleteLabelsByImageID(int imageID)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM bbox_labels WHERE image_id = @image_id", conn);
            cmd.Parameters.Add("@image_id", SqlDbType.Int).Value = imageID;

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                success = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'BBoxLabels_DeleteLabelsByImageID' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return success;
        }
        #endregion

        #region "Tags"
        public string[] Tags_Load()
        {
            var tags = new List<string>();
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand("SELECT tag_text FROM tags", conn);

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    tags.Add(rdr.GetString(0));
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'Tags_Load' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            return tags.ToArray();
        }

        public bool Tags_Add(string tagText)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO tags (tag_text) VALUES(@tag_text)", conn);
            cmd.Parameters.AddWithValue("@tag_text", tagText);

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                success = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in 'Tags_Add' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return success;
        }
        #endregion
    }

    public class LabelNode
    {
        public LabelNode(int id, string name, int parentID)
        {
            ID = id;
            ParentID = parentID;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Name { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public List<LabelNode> Children { get; set; }
    }

    public class LabelledROI
    {
        public LabelledROI(int id, int imageID, Rectangle roi)
        {
            ID = id;
            ImageID = ImageID;
            LabelID = -1;
            LabelName = null;
            ROI = roi;
            Truncated = false;
            Occluded = false;
            OutOfFocus = false;
        }
        public int ID { get; set; }
        public int ImageID { get; set; }
        public int LabelID { get; set; }
        public string LabelName { get; set; }
        public Rectangle ROI { get; set; }
        public bool Truncated { get; set; }
        public bool Occluded { get; set; }
        public bool OutOfFocus { get; set; }
    }

    public class LabelledImage
    {
        public int ID { get; set; }
        public string Filepath { get; set; }
        public bool Completed { get; set; }
        public List<LabelledROI> LabelledROIs { get; set; }
        public Bitmap Thumbnail { get; set; }
        public SensorTypeEnum SensorType { get; set; }
        public string Tags { get; set; } = "";
        public int LabelID { get; set; }
        public string LabelName { get; set; }
        public Size ImageSize { get; set; }
        public LabelledImage(Size imageSize)
        {
            LabelledROIs = new List<LabelledROI>();
            SensorType = SensorTypeEnum.Unknown;
            LabelID = -1;
            ImageSize = imageSize;
        }
    }

    public class Video
    {
        public int ID { get; set; }
        public string Filepath { get; set; }
        public bool Completed { get; set; }
        public Bitmap Thumbnail { get; set; }
        public SensorTypeEnum SensorType { get; set; }
        public string Tags { get; set; } = "";
        public int LabelID { get; set; }
        public string LabelName { get; set; }
        public Size VideoSize { get; set; }
        public Video(Size videoSize)
        {
            SensorType = SensorTypeEnum.Unknown;
            LabelID = -1;
            VideoSize = videoSize;
        }
    }

    public class Tag
    {
        public int ID { get; set; }
        public string TagText { get; set; }
    }

    public enum SensorTypeEnum
    {
        Unknown = -1,
        Daylight = 0,
        IR = 1
    }
}

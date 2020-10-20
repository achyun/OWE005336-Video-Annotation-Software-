using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OWE005336__Video_Annotation_Software_
{
    public class ImageDatabaseAccess
    {
        public const string SETTING_IMAGE_DIR = "image_dir";
        public const string SETTING_VIDEO_ARCHIVE_DIR = "video_archive_dir";
        string _ConnectionString = null;

        public ImageDatabaseAccess()
        {
         
        }

        public bool VerifyLoginCredentials(string server, string database, string userName, string password)
        {
            bool success = false;
            //Data Source=10.55.55.104,50040;User ID=chris.down;Password=********;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
            _ConnectionString = "Data Source=" + server + "; Database=" + database + "; User Id=" + userName + "; Password=" + password + "; Integrated Security=False;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";
            SqlConnection conn = new SqlConnection(_ConnectionString);
            try
            {
                conn.Open();
                success = true;
            }
            catch { }
            finally { conn.Close(); }

            return success;
        }

        #region "LabelTree"
        public List<LabelNode> LabelTree_Load(int parentID = -1)
        {
            List<LabelNode> labels = new List<LabelNode>();
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand("SELECT id, name FROM label_trees WHERE parent_id = @parent_id", conn);
            cmd.Parameters.AddWithValue("@parent_id", parentID);
            
            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    labels.Add(new LabelNode(rdr.GetInt32(0), rdr.GetString(1), parentID));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in 'LabelTree_Load' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            return labels;
        }

        public List<LabelNode> LabelTree_LoadAll()
        {
            List<LabelNode> labels = LabelTree_Load();

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
                MessageBox.Show("Error in 'LabelTree_AddLabel' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            return newNode;
        }

        public bool LabelTree_RenameLabel(int id, string new_name)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("UPDATE label_trees SET name = @new_name WHERE id = @id", conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@new_name", SqlDbType.NVarChar, 50).Value = new_name;

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in 'LabelTree_RenameLabel' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return success;
        }

        public bool LabelTree_DeleteLabel(LabelNode node)
        {
            bool success = false;

            success = LabelTree_DeleteLabelByID(node.ID);

            if (success)
            {
                success = LabelTree_RecursiveDeleteLabels(node);
            }

            return success;
        }

        private void LabelTree_RecursiveLoadLabels(ref LabelNode node)
        {
            node.Children = LabelTree_Load(node.ID);

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
            node.Children = LabelTree_Load(node.ID);

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
                MessageBox.Show("Error in 'LabelTree_LoadLabelList' : \r\n\r\n" + ex.ToString());
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
                MessageBox.Show("Error in 'LabelTree_DeleteLabelByID' : \r\n\r\n" + ex.ToString());
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
                MessageBox.Show("Error in 'LabelTree_DeleteLabelsByParentID' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

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
                MessageBox.Show("Error in 'Settings_Get' : \r\n\r\n" + ex.ToString());
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
                MessageBox.Show("Error in 'Settings_Modify' : \r\n\r\n" + ex.ToString());
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
                MessageBox.Show("Error in 'Settings_Create' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }
            return success;
        }

        #endregion

        #region "Videos"
        
        public Task<Video> Videos_Add(string sourceFliePath, string destFilePath, SensorTypeEnum sensorType = SensorTypeEnum.Unknown, int labelID = -1, string tags = "")
        {
            return Task.Run(() =>
            {
                Video video = null;

                if (File.Exists(sourceFliePath))
                {
                    string dirPath = Settings_Get(SETTING_VIDEO_ARCHIVE_DIR);
                    string newFilePath = Path.Combine(dirPath, destFilePath);

                    try
                    {
                        File.Copy(sourceFliePath, newFilePath);
                        video = Videos_AddDBEntry(destFilePath, sensorType, labelID, tags);
                    }
                    catch (Exception ex) { }
                }

                return video;
            });
        }
        private Video Videos_AddDBEntry(string fileName, SensorTypeEnum sensorType, int labelID, string tags)
        {
            Video video = null;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO videos (filepath, sensor_type, label_id, tags) VALUES(@filepath, @sensor_type, @label_id, @tags); " +
                                            "SELECT SCOPE_IDENTITY()", conn);
            SqlDataReader rdr = null;

            cmd.Parameters.AddWithValue("@filepath", fileName);
            cmd.Parameters.AddWithValue("@sensor_type", sensorType);
            cmd.Parameters.AddWithValue("@label_id", labelID);
            cmd.Parameters.Add("@tags", SqlDbType.NVarChar).Value = tags;
            cmd.CommandType = CommandType.Text;

            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    video = new Video();
                    video.ID = Decimal.ToInt32(rdr.GetDecimal(0));
                    video.Filepath = fileName;
                    video.SensorType = sensorType;
                    video.Tags = tags;
                    video.LabelID = labelID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in 'Images_AddDBEntry' (" + fileName + ") : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return video;
        }
        #endregion

        #region "Images"
        public Task<LabelledImage> Images_Add(string sourceFliePath, string destFilePath, SensorTypeEnum sensorType = SensorTypeEnum.Unknown, int labelID = -1, string tags = "")
        {
            return Task.Run(() => {
                LabelledImage lImage = null;

                if (File.Exists(sourceFliePath))
                {
                    string dirPath = Settings_Get(SETTING_IMAGE_DIR);
                    string newFilePath = Path.Combine(dirPath, destFilePath);

                    try
                    {
                        File.Copy(sourceFliePath, newFilePath);
                        lImage = Images_AddDBEntry(destFilePath, sensorType, labelID, tags);
                    }
                    catch { }
                }

                return lImage;
            });
        }

        public Task<LabelledImage> Images_Add(Image img, string destFilePath, SensorTypeEnum sensorType = SensorTypeEnum.Unknown, int labelID = -1, string tags = "")
        {
            return Task.Run(() => {
                LabelledImage lImage = null;

                if (img != null)
                {
                    string dirPath = Settings_Get(SETTING_IMAGE_DIR);
                    string newFilePath = Path.Combine(dirPath, destFilePath);

                    try
                    {
                        Bitmap bmp = new Bitmap(img);
                        bmp.Save(newFilePath);
                        lImage = Images_AddDBEntry(destFilePath, sensorType, labelID, tags);
                    }
                    catch { }
                }

                return lImage;
            });
        }

        private LabelledImage Images_AddDBEntry(string fileName, SensorTypeEnum sensorType, int labelID, string tags)
        {
            LabelledImage limg = null;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO images (filepath, sensor_type, label_id, tags) VALUES(@filepath, @sensor_type, @label_id, @tags); " +
                                            "SELECT SCOPE_IDENTITY()", conn);
            SqlDataReader rdr = null;

            cmd.Parameters.AddWithValue("@filepath", fileName);
            cmd.Parameters.AddWithValue("@sensor_type", sensorType);
            cmd.Parameters.AddWithValue("@label_id", labelID);
            cmd.Parameters.Add("@tags", SqlDbType.NVarChar).Value = tags;
            cmd.CommandType = CommandType.Text;

            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    limg = new LabelledImage();
                    limg.ID = Decimal.ToInt32(rdr.GetDecimal(0));
                    limg.Filepath = fileName;
                    limg.SensorType = sensorType;
                    limg.Tags = tags;
                    limg.LabelID = labelID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in 'Images_AddDBEntry' (" + fileName + ") : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return limg;
        }

        public List<LabelledImage> Images_Get(bool filterForIncomplete, bool filterForNoLabels)
        {
            DataTable dt = new DataTable();
            List<LabelledImage> imageList = new List<LabelledImage>();
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("SELECT images.id, filepath, labelling_complete, sensor_type, tags, label_id, label_trees.name FROM images LEFT JOIN label_trees ON label_trees.id = images.label_id", conn);

            if (filterForIncomplete || filterForNoLabels) { cmd.CommandText += " WHERE"; }

            if (filterForIncomplete)
            { 
                cmd.CommandText += " labelling_complete = @labelling_complete";
                cmd.Parameters.Add("@labelling_complete", SqlDbType.Bit).Value = !filterForIncomplete;
            }

            if (filterForIncomplete && filterForNoLabels) { cmd.CommandText += " AND"; }

            if (filterForNoLabels)
            {
                cmd.CommandText += " (SELECT COUNT(id) FROM bbox_labels WHERE bbox_labels.image_id = images.id) = 0 ";
            }
            adapter.SelectCommand = cmd;           
            cmd.CommandType = CommandType.Text;

            try
            {
                conn.Open();
                adapter.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    LabelledImage img = new LabelledImage();
                    img.ID = (int)(dr.ItemArray[0]);
                    img.Filepath = (string)(dr.ItemArray[1]);
                    img.Completed = (bool)(dr.ItemArray[2]);
                    img.SensorType = (SensorTypeEnum)(dr.ItemArray[3]);
                    img.Tags = (string)(dr.ItemArray[4]);
                    img.LabelID = (int)dr.ItemArray[5];
                    if (!dr.ItemArray[6].Equals(System.DBNull.Value)) { img.LabelName = (string)dr.ItemArray[6]; }
                    imageList.Add(img);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in 'Images_Get' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                adapter.Dispose();
            }

            return imageList;
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
                MessageBox.Show("Error in 'Images_Update' : \r\n\r\n" + ex.ToString());
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
                MessageBox.Show("Error in 'Images_Update' : \r\n\r\n" + ex.ToString());
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
                MessageBox.Show("Error in 'Images_DeleteDBEntry' : \r\n\r\n" + ex.ToString());
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
            SqlCommand cmd = new SqlCommand("SELECT bbox_labels.id, image_id, label_id, location_x, location_y, size_width, size_height, truncated, occluded, label_trees.name FROM bbox_labels LEFT JOIN label_trees ON label_trees.id = bbox_labels.label_id WHERE bbox_labels.image_id = @image_id", conn);
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
                    lroi.ImageID = imageID;
                    if (rdr.GetValue(9) != DBNull.Value) { lroi.LabelName = rdr.GetString(9); }
                    
                    labels.Add(lroi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in 'BBoxLabels_LoadByImageID' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }
            return labels;
        }

        public LabelledROI BBoxLabels_AddLabel(int imageID, Rectangle roi)
        {
            int newID = -1;
            LabelledROI lroi = null;
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO bbox_labels (image_id, location_x, location_y, size_width, size_height) VALUES(@image_id, @location_x, @location_y, @size_width, @size_height); " +
                                            "SELECT SCOPE_IDENTITY()", conn);
            SqlDataReader rdr = null;
            cmd.Parameters.Add("@image_id", SqlDbType.Int).Value = imageID;
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
                }
                lroi = new LabelledROI(newID, imageID, roi);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in 'BBoxLabels_AddLabel' : \r\n\r\n" + ex.ToString());
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
            SqlCommand cmd = new SqlCommand("UPDATE bbox_labels SET label_id = @label_id, location_x = @location_x, location_y = @location_y, size_width = @size_width, size_height = @size_height, truncated = @truncated, occluded = @occluded WHERE id = @id; " +
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

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in 'BBoxLabels_UpdateLabel' : \r\n\r\n" + ex.ToString());
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
                MessageBox.Show("Error in 'BBoxLabels_DeleteLabelByID' : \r\n\r\n" + ex.ToString());
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
                MessageBox.Show("Error in 'BBoxLabels_DeleteLabelsByImageID' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }

            return success;
        }
        #endregion

        #region "Tags"
        public AutoCompleteStringCollection Tags_Load()
        {
            AutoCompleteStringCollection tags = new AutoCompleteStringCollection();
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
                MessageBox.Show("Error in 'Tags_Load' : \r\n\r\n" + ex.ToString());
            }
            finally
            {
                if (conn != null) { conn.Close(); }
                if (rdr != null) { rdr.Close(); }
            }

            return tags;
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
                MessageBox.Show("Error in 'Tags_Add' : \r\n\r\n" + ex.ToString());
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
        }
        public int ID { get; set; }
        public int ImageID { get; set; }
        public int LabelID { get; set; }
        public string LabelName { get; set; }
        public Rectangle ROI { get; set; }
        public bool Truncated { get; set; }
        public bool Occluded { get; set; }
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
        public LabelledImage()
        {
            LabelledROIs = new List<LabelledROI>();
            SensorType = SensorTypeEnum.Unknown;
            LabelID = -1;
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
        public Video()
        {
            SensorType = SensorTypeEnum.Unknown;
            LabelID = -1;
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

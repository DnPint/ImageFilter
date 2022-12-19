/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageEdgeDetection
{
    public partial class MainForm : Form
    {
        //the original image
        private Bitmap originalBitmap = null;
        private System.Drawing.Image Origin;
        private Image filtered;
        private string xFilter;
        private string yFilter;
        private ToolBox toolBox = new ToolBox();

        public MainForm()
        {
            InitializeComponent();

            //make the list for XY edges disable before selecting a filter
            disableXY();
        }

        public void disableXY()
        {
            listBoxXFilter.Enabled = false;
            listBoxYFilter.Enabled = false;
            trackBarThreshold.Enabled = false;
        }

        public void enableXY()
        {
            listBoxXFilter.Enabled = true;
            listBoxYFilter.Enabled = true;
            trackBarThreshold.Enabled = true;
        }

        public void LoadImage(object sender, EventArgs e)
        {
            //Open the dialog box to get an image on the screen
            OpenFileDialog op = InitializeOpenFileDialog();
            DialogResult dr = op.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string path = op.FileName;
                Origin = Image.FromFile(path);
                originalBitmap = (Bitmap)Bitmap.FromFile(path);
                picPreview.Image = originalBitmap;
            }
        }
       
        private OpenFileDialog InitializeOpenFileDialog()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select an image file.";
            op.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            op.Filter += "|Bitmap Images(*.bmp)|*.bmp";
            return op;
        }

        //The file can be named directly in the SaveDialog box
        /*public void SaveImage()
        {
            SaveFileDialog saveFileDialog = InitializeSaveFileDialog();
            if (saveFileDialog.FileName != "")
                SaveImageAppropriateFormat(saveFileDialog);
        }*/

        private SaveFileDialog InitializeSaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.ShowDialog();
            return saveFileDialog;
        }

        private void SaveImageAppropriateFormat(SaveFileDialog saveFileDialog)
        {
            System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
            switch (saveFileDialog.FilterIndex)
            {
                case 1:
                    filtered.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case 2:
                    filtered.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
            }
            fs.Close();
        }

        public void buttonFilter_Click(object sender, EventArgs e)
        {
            if (picPreview.Image != null)
            {
                
                switch (((Button)sender).Name)
                {
                    case "buttonNightFilter":
                        filtered = toolBox.ApplyFilter(new Bitmap(Origin), 1, 1, 1, 25);
                        break;
                    case "buttonMiamiFilter":
                        filtered = toolBox.ApplyFilter(new Bitmap(Origin), 1, 1, 10, 1);
                        break;
                    case "buttonMagicMosaic":
                        filtered = toolBox.MagicMosaic(new Bitmap(Origin));
                        break;
                }
                picPreview.Image = filtered;

                //Since a filter is chosen, make XY lists available
                enableXY();
            }
            else
            {
                MessageBox.Show("There is no image to filter");
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            picPreview.Image = Origin;

            //make the list for XY edges disable before selecting a filter
            disableXY();

        }

        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
            if (originalBitmap != null)
            {
                SaveFileDialog saveFileDialog = InitializeSaveFileDialog();
                if (saveFileDialog.FileName != "")
                    SaveImageAppropriateFormat(saveFileDialog);
            }
            else
            {
                MessageBox.Show("There is no image to save");
            }
        }

        private void listBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxXFilter.SelectedItem != null && listBoxYFilter.SelectedItem != null)
            {
                xFilter = listBoxXFilter.SelectedItem.ToString();
                yFilter = listBoxYFilter.SelectedItem.ToString();
                filter(xFilter, yFilter);
               
            }
                
        }

        public void filter(string xfilter, string yfilter)
        {
            try
            {
               picPreview.Image = toolBox.XyFilter(xfilter, yfilter, filtered, Convert.ToInt32(trackBarThreshold.Value));             
            }
            catch (Exception e)
            {
                MessageBox.Show("There is no image to filter");
            }

        }


        private void trackBarThreshold_Scroll(object sender, EventArgs e)
        {
            //Verify first if the x,y filter are choosen then refilter with the treshold cursor
            if(xFilter != null && yFilter != null)
                filter(xFilter, yFilter);
        }

    }
}


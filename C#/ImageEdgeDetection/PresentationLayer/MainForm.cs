/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using DocumentFormat.OpenXml.Drawing.Charts;
using ImageEdgeDetection.BusinessLayer;
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
        private Image filtered;
        private string xFilter;
        private string yFilter;
        private IFilter xIFilter = new Filter();
        private IFilter yIFilter = new Filter();
        private IToolBox toolBox = new ToolBox();

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
            originalBitmap = toolBox.LoadImage(sender, e);
            picPreview.Image = originalBitmap;
        }

        public void buttonFilter_Click(object sender, EventArgs e)
        {
            if (picPreview.Image != null)
            {
                switch (((Button)sender).Name)
                {
                    case "buttonNightFilter":
                        filtered = toolBox.ApplyFilter(new Bitmap(originalBitmap), 1, 1, 1, 25);
                        break;
                    case "buttonMiamiFilter":
                        filtered = toolBox.ApplyFilter(new Bitmap(originalBitmap), 1, 1, 10, 1);
                        break;
                    case "buttonMagicMosaic":
                        filtered = toolBox.MagicMosaic(new Bitmap(originalBitmap));
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
            picPreview.Image = originalBitmap;

            //make the list for XY edges disable before selecting a filter
            disableXY();
        }

        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
            if (originalBitmap != null && filtered != null)
            {
                toolBox.SaveImageAppropriateFormat(filtered);
            }
            else
            {
                MessageBox.Show("There is no filtered image to save");
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
            xIFilter.setFilterName(xfilter);
            yIFilter.setFilterName(yfilter);
            try
            {
               picPreview.Image = toolBox.XyFilter(xIFilter, yIFilter, filtered, Convert.ToInt32(trackBarThreshold.Value));             
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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


/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using DocumentFormat.OpenXml.Presentation;

namespace ImageEdgeDetection
{
    public partial class MainForm : Form
    {
        //the original image
        private Bitmap originalBitmap = null;

        //The modified image
        private Bitmap previewBitmap = null;

        //Created just before saving
        private Bitmap resultBitmap = null;
        
        System.Drawing.Image Origin;
        Bitmap map;

        public MainForm()
        {
            InitializeComponent();

            //The DropBox
            cmbEdgeDetection.SelectedIndex = 0;
        }

        //On "Load Image" button press
        private void btnOpenOriginal_Click(object sender, EventArgs e)
        {
            ////filters for image files
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Title = "Select an image file.";
            //ofd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            //ofd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            ////Once we select an image on the windows navigator
            //if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    StreamReader streamReader = new StreamReader(ofd.FileName);
            //    originalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
            //    streamReader.Close();

            //    //First method of ExtBitmaps.cs
            //    previewBitmap = originalBitmap.CopyToSquareCanvas(picPreview.Width);
            //    //picPreview is the name of the canvas
            //    picPreview.Image = previewBitmap;

            //    ApplyFilter(true);
            //}
            LoadImage();
        }

        public void LoadImage()
        {
            OpenFileDialog op = FilterImageFile();
            DialogResult dr = op.ShowDialog();
           
            if (dr == DialogResult.OK)
            {
                string path = op.FileName;
                picPreview.Load(path);
                Bitmap temp = new Bitmap(picPreview.Image,
                   new Size(picPreview.Width, picPreview.Height));
                picPreview.Image = temp;
                map = new Bitmap(picPreview.Image);
                Origin = picPreview.Image;
            }
        }

        private OpenFileDialog FilterImageFile()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select an image file.";
            op.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            op.Filter += "|Bitmap Images(*.bmp)|*.bmp";
            return op;
        }

        public void SaveImage()
        {
            picPreview.SizeMode = PictureBoxSizeMode.AutoSize;
            FolderBrowserDialog fl = new FolderBrowserDialog();
            if (fl.ShowDialog() != DialogResult.Cancel)
            {
                picPreview.Image.Save(fl.SelectedPath + @"\" + textBoxNameFile.Text + @".png", System.Drawing.Imaging.ImageFormat.Png);
            };
            picPreview.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        //Applies the filter, is called on image load, save and when the filter is changed
        private void ApplyFilter(bool preview)
        {
            //Check that there is an image and a filter selected
            if (previewBitmap == null || cmbEdgeDetection.SelectedIndex == -1)
            {
                return;
            }

            Bitmap selectedSource = null;
            Bitmap bitmapResult = null;

            //preview is false only when method is called from "Save Image" method
            if (preview == true)
            {
                //apply filter on the preview image
                selectedSource = previewBitmap;
            }
            else
            {
                //To apply filter on the original image, only used when we save
                selectedSource = originalBitmap;
            }

            //Hell
            //Check the dropbox and apply filter to the selected source accodingly
            if (selectedSource != null)
            {
                if (cmbEdgeDetection.SelectedItem.ToString() == "None")
                {
                    bitmapResult = selectedSource;
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3")
                {
                    bitmapResult = selectedSource.Laplacian3x3Filter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3 Grayscale")
                {
                    bitmapResult = selectedSource.Laplacian3x3Filter(true);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5")
                {
                    bitmapResult = selectedSource.Laplacian5x5Filter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5 Grayscale")
                {
                    bitmapResult = selectedSource.Laplacian5x5Filter(true);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian of Gaussian")
                {
                    bitmapResult = selectedSource.LaplacianOfGaussianFilter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3 of Gaussian 3x3")
                {
                    bitmapResult = selectedSource.Laplacian3x3OfGaussian3x3Filter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3 of Gaussian 5x5 - 1")
                {
                    bitmapResult = selectedSource.Laplacian3x3OfGaussian5x5Filter1();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3 of Gaussian 5x5 - 2")
                {
                    bitmapResult = selectedSource.Laplacian3x3OfGaussian5x5Filter2();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5 of Gaussian 3x3")
                {
                    bitmapResult = selectedSource.Laplacian5x5OfGaussian3x3Filter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5 of Gaussian 5x5 - 1")
                {
                    bitmapResult = selectedSource.Laplacian5x5OfGaussian5x5Filter1();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5 of Gaussian 5x5 - 2")
                {
                    bitmapResult = selectedSource.Laplacian5x5OfGaussian5x5Filter2();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Sobel 3x3")
                {
                    bitmapResult = selectedSource.Sobel3x3Filter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Sobel 3x3 Grayscale")
                {
                    bitmapResult = selectedSource.Sobel3x3Filter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Prewitt")
                {
                    bitmapResult = selectedSource.PrewittFilter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Prewitt Grayscale")
                {
                    bitmapResult = selectedSource.PrewittFilter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Kirsch")
                {
                    bitmapResult = selectedSource.KirschFilter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Kirsch Grayscale")
                {
                    bitmapResult = selectedSource.KirschFilter();
                }
            }

            //can only be null if someting goes wrong with the code
            if (bitmapResult != null)
            {
                if (preview == true)
                {
                    //change image
                    picPreview.Image = bitmapResult;
                }
                else
                {
                    //will not be null anymore, happens only if we try to save
                    resultBitmap = bitmapResult;
                }
            }
        }

        //Listener for the dropBox
        private void NeighbourCountValueChangedEventHandler(object sender, EventArgs e)
        {
            ApplyFilter(true);
        }

        private void listBoxXFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonNightFilter_Click(object sender, EventArgs e)
        {
            picPreview.Image = Origin;
            picPreview.Image = ImageFilters.ApplyFilter(new Bitmap(picPreview.Image), 1, 1, 1, 25);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            picPreview.Image = Origin;
        }

        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void buttonMiamiFilter_Click(object sender, EventArgs e)
        {
            picPreview.Image = Origin;
            picPreview.Image = ImageFilters.ApplyFilter(new Bitmap(picPreview.Image), 1, 1, 10, 1);
        }

        private void textBoxNameFile_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

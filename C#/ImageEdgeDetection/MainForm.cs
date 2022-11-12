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
using System.Runtime.InteropServices;
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
                originalBitmap = (Bitmap)Bitmap.FromFile(path);
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

            //Check the dropbox and apply filter to the selected source accodingly
            if (selectedSource != null)
            {
                _MethodInfo _MethodInfo = bitmapResult.GetType().GetMethod(cmbEdgeDetection.SelectedIndex.ToString());
                _MethodInfo.Invoke(this, null);
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
        

        //Box y listener
        private void listBoxYFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //the two boxes mus be selected - might change it ----------------------
            if (listBoxXFilter.SelectedItem.ToString().Length > 0 && listBoxYFilter.SelectedItem.ToString().Length > 0)
            {
                filter(listBoxXFilter.SelectedItem.ToString(), listBoxYFilter.SelectedItem.ToString());
            }
        }

        //Same as above
        private void listBoxXFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxXFilter.SelectedItem != null && listBoxYFilter.SelectedItem !=null)
            {
                filter(listBoxXFilter.SelectedItem.ToString(), listBoxYFilter.SelectedItem.ToString());
            }
        }

        public void filter(string xfilter, string yfilter)
        {
            double[,] xFilterMatrix;
            double[,] yFilterMatrix;
            Matrix matrix = new Matrix();

            //xFilterMatrix = matrix.GetType().GetProperty(xfilter).GetValue();
            //GetProperty("PropertyName").GetValue(yourInstance);

            xFilterMatrix = (double[,])matrix.GetType().GetProperty(xfilter).GetValue(matrix, null);
            yFilterMatrix = (double[,])matrix.GetType().GetProperty(yfilter).GetValue(matrix, null);


            if (picPreview.Image.Size.Height > 0)
            {
                Bitmap newbitmap = originalBitmap;
                BitmapData newbitmapData = new BitmapData();
                newbitmapData = newbitmap.LockBits(new Rectangle(0, 0, newbitmap.Width, newbitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);

                byte[] pixelbuff = new byte[newbitmapData.Stride * newbitmapData.Height];
                byte[] resultbuff = new byte[newbitmapData.Stride * newbitmapData.Height];

                Marshal.Copy(newbitmapData.Scan0, pixelbuff, 0, pixelbuff.Length);
                newbitmap.UnlockBits(newbitmapData);


                double blue = 0.0;
                double green = 0.0;
                double red = 0.0;

                //int filterWidth = filterMatrix.GetLength(1);
                //int filterHeight = filterMatrix.GetLength(0);

                //int filterOffset = (filterWidth - 1) / 2;
                //int calcOffset = 0;

                //int byteOffset = 0;

                double blueX = 0.0;
                double greenX = 0.0;
                double redX = 0.0;

                double blueY = 0.0;
                double greenY = 0.0;
                double redY = 0.0;

                double blueTotal = 0.0;
                double greenTotal = 0.0;
                double redTotal = 0.0;

                int filterOffset = 1;
                int calcOffset = 0;

                int byteOffset = 0;

                for (int offsetY = filterOffset; offsetY <
                    newbitmap.Height - filterOffset; offsetY++)
                {
                    for (int offsetX = filterOffset; offsetX <
                        newbitmap.Width - filterOffset; offsetX++)
                    {
                        blueX = greenX = redX = 0;
                        blueY = greenY = redY = 0;

                        blueTotal = greenTotal = redTotal = 0.0;

                        byteOffset = offsetY *
                                     newbitmapData.Stride +
                                     offsetX * 4;

                        for (int filterY = -filterOffset;
                            filterY <= filterOffset; filterY++)
                        {
                            for (int filterX = -filterOffset;
                                filterX <= filterOffset; filterX++)
                            {
                                calcOffset = byteOffset +
                                             (filterX * 4) +
                                             (filterY * newbitmapData.Stride);

                                blueX += (double)(pixelbuff[calcOffset]) *
                                          xFilterMatrix[filterY + filterOffset,
                                                  filterX + filterOffset];

                                greenX += (double)(pixelbuff[calcOffset + 1]) *
                                          xFilterMatrix[filterY + filterOffset,
                                                  filterX + filterOffset];

                                redX += (double)(pixelbuff[calcOffset + 2]) *
                                          xFilterMatrix[filterY + filterOffset,
                                                  filterX + filterOffset];

                                blueY += (double)(pixelbuff[calcOffset]) *
                                          yFilterMatrix[filterY + filterOffset,
                                                  filterX + filterOffset];

                                greenY += (double)(pixelbuff[calcOffset + 1]) *
                                          yFilterMatrix[filterY + filterOffset,
                                                  filterX + filterOffset];

                                redY += (double)(pixelbuff[calcOffset + 2]) *
                                          yFilterMatrix[filterY + filterOffset,
                                                  filterX + filterOffset];
                            }
                        }

                        //uncomment to change colors of filtered image to original 

                        //blueTotal = Math.Sqrt((blueX * blueX) + (blueY * blueY));
                        blueTotal = 0;
                        greenTotal = Math.Sqrt((greenX * greenX) + (greenY * greenY));
                        //redTotal = Math.Sqrt((redX * redX) + (redY * redY));
                        redTotal = 0;

                        if (blueTotal > 255)
                        { blueTotal = 255; }
                        else if (blueTotal < 0)
                        { blueTotal = 0; }

                        if (greenTotal > 255)
                        { greenTotal = 255; }
                        else if (greenTotal < 0)
                        { greenTotal = 0; }

                        if (redTotal > 255)
                        { redTotal = 255; }
                        else if (redTotal < 0)
                        { redTotal = 0; }

                        resultbuff[byteOffset] = (byte)(blueTotal);
                        resultbuff[byteOffset + 1] = (byte)(greenTotal);
                        resultbuff[byteOffset + 2] = (byte)(redTotal);
                        resultbuff[byteOffset + 3] = 255;
                    }
                }

                Bitmap resultbitmap = new Bitmap(newbitmap.Width, newbitmap.Height);

                BitmapData resultData = resultbitmap.LockBits(new Rectangle(0, 0,
                                         resultbitmap.Width, resultbitmap.Height),
                                                          ImageLockMode.WriteOnly,
                                                      PixelFormat.Format32bppArgb);

                Marshal.Copy(resultbuff, 0, resultData.Scan0, resultbuff.Length);
                resultbitmap.UnlockBits(resultData);
                picPreview.Image = resultbitmap;
            }
            else
            {
                Console.WriteLine("Something wrong in filter xy");
                //error message on label
                //labelErrors.Text = "You must load an image";
            }
        }

    }
}

/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageEdgeDetection
{
    public partial class MainForm : Form
    {
        //the original image
        private Bitmap originalBitmap = null;
        private System.Drawing.Image Origin;
        private Image imageFiltered;

        public MainForm()
        {
            InitializeComponent();
        }

        public void LoadImage(object sender, EventArgs e)
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
                Bitmap map = new Bitmap(picPreview.Image);
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
            SaveFileDialog saveFileDialog = ConfigureSaveFileDialog();
            if (saveFileDialog.FileName != "")
                SaveImageAppropriateFormat(saveFileDialog);
        }

        private SaveFileDialog ConfigureSaveFileDialog()
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
                    picPreview.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case 2:
                    picPreview.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
            }
            fs.Close();
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            if (picPreview.Image != null)
            {
                picPreview.Image = Origin;
                switch (((Button)sender).Name)
                {
                    case "buttonNightFilter":
                        picPreview.Image = ImageFilters.ApplyFilter(new Bitmap(picPreview.Image), 1, 1, 1, 25);
                        imageFiltered = picPreview.Image;
                        break;
                    case "buttonMiamiFilter":
                        picPreview.Image = ImageFilters.ApplyFilter(new Bitmap(picPreview.Image), 1, 1, 10, 1);
                        imageFiltered = picPreview.Image;
                        break;
                }
            }
            else
            {
                MessageBox.Show("There is no image to filter");
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            picPreview.Image = Origin;
        }

        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void listBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxXFilter.SelectedItem != null && listBoxYFilter.SelectedItem != null)
                filter(listBoxXFilter.SelectedItem.ToString(), listBoxYFilter.SelectedItem.ToString());
        }

        public void filter(string xfilter, string yfilter)
        {
            double[,] xFilterMatrix;
            double[,] yFilterMatrix;
            Matrix matrix = new Matrix();

            xFilterMatrix = (double[,])matrix.GetType().GetProperty(xfilter).GetValue(matrix, null);
            yFilterMatrix = (double[,])matrix.GetType().GetProperty(yfilter).GetValue(matrix, null);

            if (picPreview.Image != null)
            {
                if (picPreview.Image.Size.Height > 0)
                {
                    // Bitmap newbitmap = originalBitmap;
                    Bitmap newbitmap = new Bitmap(imageFiltered);
                    BitmapData newbitmapData = new BitmapData();
                    newbitmapData = newbitmap.LockBits(new Rectangle(0, 0, newbitmap.Width, newbitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);

                    byte[] pixelbuff = new byte[newbitmapData.Stride * newbitmapData.Height];
                    byte[] resultbuff = new byte[newbitmapData.Stride * newbitmapData.Height];

                    Marshal.Copy(newbitmapData.Scan0, pixelbuff, 0, pixelbuff.Length);
                    newbitmap.UnlockBits(newbitmapData);

                    double greenX;
                    double greenY;

                    double blueTotal = 0.0;
                    double greenTotal;
                    double redTotal = 0.0;

                    int filterOffset = 1;
                    int calcOffset;

                    int byteOffset;

                    for (int offsetY = filterOffset; offsetY <
                        newbitmap.Height - filterOffset; offsetY++)
                    {
                        for (int offsetX = filterOffset; offsetX <
                            newbitmap.Width - filterOffset; offsetX++)
                        {
                            greenX = 0;
                            greenY = 0;

                            greenTotal = 0.0;

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

                                    greenX += (double)(pixelbuff[calcOffset + 1]) *
                                              xFilterMatrix[filterY + filterOffset,
                                                      filterX + filterOffset];

                                    greenY += (double)(pixelbuff[calcOffset + 1]) *
                                              yFilterMatrix[filterY + filterOffset,
                                                      filterX + filterOffset];
                                }
                            }

                            greenTotal = Math.Sqrt((greenX * greenX) + (greenY * greenY));

                            if (greenTotal > 255)
                            { greenTotal = 255; }
                            else if (greenTotal < 0)
                            { greenTotal = 0; }

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
            }
            else
            {
                MessageBox.Show("There is no image to filter");
            }
        }

    }
}


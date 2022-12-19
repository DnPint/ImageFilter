﻿using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEdgeDetection
{ 
    
    public class ToolBox : IToolBox
    {
        private IMatrix matrix = new Matrix();
        
        //apply color filter at your own taste
        public Bitmap ApplyFilter(Bitmap bmp, int alpha, int red, int blue, int green)
        {
            if (alpha < 0 || alpha > 255 || red < 0 || red > 255 || blue < 0 || blue > 255 || green < 0 || green > 255)
            {
                return null;
            }

            if (bmp != null)
            {
                Bitmap temp = new Bitmap(bmp.Width, bmp.Height);

                for (int i = 0; i < bmp.Width; i++)
                {
                    for (int x = 0; x < bmp.Height; x++)
                    {
                        Color c = bmp.GetPixel(i, x);
                        Color cLayer = Color.FromArgb(c.A / alpha, c.R / red, c.G / green, c.B / blue);
                        temp.SetPixel(i, x, cLayer);
                    }
                }
                return temp;
            }
            return null;
        }

        public Image XyFilter(string xfilter, string yfilter, Image Original, int value)
        {
            double[,] xFilterMatrix;
            double[,] yFilterMatrix;

            // Assign user choices for x and y filters
            xFilterMatrix = (double[,])matrix.GetType().GetProperty(xfilter).GetValue(matrix, null);
            yFilterMatrix = (double[,])matrix.GetType().GetProperty(yfilter).GetValue(matrix, null);

            Bitmap originalBitmap = new Bitmap(Original);

            try
            {
                if (originalBitmap.Size.Height > 0)
                {
                    Bitmap newbitmap = originalBitmap;
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

                            if (greenTotal < value)
                                greenTotal = 0;
                            else
                                greenTotal = 255;



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
                    return resultbitmap;
                }
            }
            catch
            {
                throw new Exception("Error");
            }
            return null;
        }

        public Bitmap MagicMosaic(Bitmap bmp)
        {
            if (bmp.Height > bmp.Width)
            {
                int razX = Convert.ToInt32(bmp.Width / 3);
                int razY = Convert.ToInt32(bmp.Height / 3);

                Bitmap temp = new Bitmap(bmp.Width, bmp.Height);

                for (int i = 0; i < bmp.Width - 1; i++)
                {
                    for (int x = 0; x < bmp.Height - 1; x++)
                    {
                        if (i < razX)
                        {
                            if (x < razY)
                                temp.SetPixel(i, x, bmp.GetPixel(i, x));
                            else if (x < razY * 2)
                                temp.SetPixel(i, x, bmp.GetPixel(x, i));
                            else if (x < razY * 3)
                                temp.SetPixel(i, x, bmp.GetPixel(i, x));
                        }
                        else if (i < razX * 2)
                        {
                            if (x < razY)
                                temp.SetPixel(i, x, bmp.GetPixel(x, i));
                            else if (x < razY * 2)
                                temp.SetPixel(i, x, bmp.GetPixel(i, x));
                            else if (x < razY * 3)
                                temp.SetPixel(i, x, bmp.GetPixel(x / 3, i / 3));
                        }
                        else if (i < razX * 3)
                        {
                            if (x < razY)
                                temp.SetPixel(i, x, bmp.GetPixel(i, x));
                            else if (x < razY * 2)
                                temp.SetPixel(i, x, bmp.GetPixel(x, i));
                            else if (x < razY * 3)
                                temp.SetPixel(i, x, bmp.GetPixel(i, x));
                        }
                    }
                }
                return temp;
            }
            else
            {
                int razX = Convert.ToInt32(bmp.Width / 3);
                int razY = Convert.ToInt32(bmp.Height / 3);

                Bitmap temp = new Bitmap(bmp.Width, bmp.Height);

                for (int i = 0; i < bmp.Width - 1; i++)
                {
                    for (int x = 0; x < bmp.Height - 1; x++)
                    {
                        if (i < razX)
                        {
                            if (x < razY)
                                temp.SetPixel(i, x, bmp.GetPixel(i, x));
                            else if (x < razY * 2)
                                temp.SetPixel(i, x, bmp.GetPixel(x, i));
                            else if (x < razY * 3)
                                temp.SetPixel(i, x, bmp.GetPixel(i, x));
                        }
                        else if (i < razX * 2)
                        {
                            if (x < razY)
                                temp.SetPixel(i, x, bmp.GetPixel(x / 3, i / 3));
                            else if (x < razY * 2)
                                temp.SetPixel(i, x, bmp.GetPixel(i, x));
                            else if (x < razY * 3)
                                temp.SetPixel(i, x, bmp.GetPixel(x / 3, i / 3));
                        }
                        else if (i < razX * 3)
                        {
                            if (x < razY)
                                temp.SetPixel(i, x, bmp.GetPixel(i, x));
                            else if (x < razY * 2)
                                temp.SetPixel(i, x, bmp.GetPixel(x / 3, i / 3));
                            else if (x < razY * 3)
                                temp.SetPixel(i, x, bmp.GetPixel(i, x));
                        }
                    }
                }
                return temp;
            }
        }

        public Bitmap LoadImage(object sender, EventArgs e)
        {
            //Open the dialog box to get an image on the screen
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select an image file.";
            op.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            op.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            DialogResult dr = op.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string path = op.FileName;
                return (Bitmap)Bitmap.FromFile(path);              
            }
            return null;
        }

        public void SaveImageAppropriateFormat(Image filtered, SaveFileDialog saveFileDialog)
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

    }
}
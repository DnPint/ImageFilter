using ImageEdgeDetection.BusinessLayer;
using ImageEdgeDetection.LoadSaveLayer;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageEdgeDetection
{
    public class ToolBox : IToolBox
    {
        private IMatrix matrix = new Matrix();
        private ILoadSave loadSave = new LoadSave();

        public Bitmap ChooseFilter(IFilter filterName, Bitmap originalBitmap)
        {
            Bitmap filtered = null;
            string nameFilter = filterName.getFilterName();

            switch (nameFilter)
            {
                case "NightFilter":
                    filtered = ApplyFilter(new Bitmap(originalBitmap), 1, 1, 1, 25);
                    break;
                case "MiamiFilter":
                    filtered = ApplyFilter(new Bitmap(originalBitmap), 1, 1, 10, 1);
                    break;
                case "MagicMosaic":
                    filtered = MagicMosaic(new Bitmap(originalBitmap));
                    break;
            }
            return filtered;
        }

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

        public Bitmap XyFilter(IFilter xfilter, IFilter yfilter, Image Original, int value)
        {
            double[,] xFilterMatrix;
            double[,] yFilterMatrix;

            try
            {
            string xFilterName = xfilter.getFilterName();
            string yFilterName = yfilter.getFilterName();

            // Assign user choices for x and y filters
            xFilterMatrix = (double[,])matrix.GetType().GetProperty(xFilterName).GetValue(matrix, null);
            yFilterMatrix = (double[,])matrix.GetType().GetProperty(yFilterName).GetValue(matrix, null);

            
                Bitmap originalBitmap = new Bitmap(Original);

               
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
            catch
            {
                throw new Exception("An error occured"); 
            }
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

        public Bitmap LoadImage()
        {
            Bitmap bitmap = loadSave.LoadImage();
            return bitmap;
        }

        public void SaveImageAppropriateFormat(Image filtered)
        {
            loadSave.SaveImageAppropriateFormat(filtered);
        }
    }
}

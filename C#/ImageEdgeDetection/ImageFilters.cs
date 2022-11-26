using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace ImageEdgeDetection
{
    public static class ImageFilters
    {
        //apply color filter at your own taste
        public static Bitmap ApplyFilter(Bitmap bmp, int alpha, int red, int blue, int green)
        {
            if (alpha < 0 || alpha > 255 || red < 0 || red > 255 || blue < 0 || blue > 255 ||  green < 0 || green > 255)
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

        public static Image XyFilter(string xfilter, string yfilter, Image Original, int value)
        {
            double[,] xFilterMatrix;
            double[,] yFilterMatrix;
            Matrix matrix = new Matrix();

            //We replace the switch case to choose the right matrix for the x,y filter
            xFilterMatrix = (double[,])matrix.GetType().GetProperty(xfilter).GetValue(matrix, null);
            yFilterMatrix = (double[,])matrix.GetType().GetProperty(yfilter).GetValue(matrix, null);

            if (xFilterMatrix == null || yFilterMatrix == null)
            {
                return null;
            }

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
                            else if (greenTotal < 0)
                            { greenTotal = 0; }


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
    }
}

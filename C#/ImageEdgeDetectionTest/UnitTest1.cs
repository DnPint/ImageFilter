using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System;
using System.Resources;
using ImageEdgeDetection;

namespace ImageEdgeDetectionTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestNightFilter()
        {

            Image original = Image.FromFile("./Images/chad.png");
            
            Image filtered = Image.FromFile("./Images/nightchad.png");

            //filter the original with the night filter
            original = ImageFilters.ApplyFilter(new Bitmap(original), 1, 1, 1, 25);

            compareImage(original, filtered);
                  
        }

        [TestMethod]
        public void TestMiamiFilter()
        {
            Image original = Image.FromFile("./Images/chad.png");

            Image filtered = Image.FromFile("./Images/miamichad.png");

            //filter the original with the night filter
            original = ImageFilters.ApplyFilter(new Bitmap(original), 1, 1, 10, 1);           

            compareImage(original, filtered);
        }

        private void compareImage(Image original, Image filtered)
        {
            Bitmap bmpFiltered = new Bitmap(filtered);
            Bitmap bmpOriginal = new Bitmap(original);

            //Compare bitmaps to see if they are the same
            for (int i = 0; i < bmpOriginal.Width; i++)
            {
                for (int j = 0; j < bmpOriginal.Height; j++)
                {
                    Color originalColor = bmpOriginal.GetPixel(i, j);
                    Color filteredColor = bmpFiltered.GetPixel(i, j);
                    Assert.AreEqual(originalColor.R, filteredColor.R);
                    Assert.AreEqual(originalColor.G, filteredColor.G);
                    Assert.AreEqual(originalColor.B, filteredColor.B);
                }
            }
        }
        

        [TestMethod]
        public void XKirsch3x3Vertical_YKirsch3x3Vertical()
        {
            Image original = Image.FromFile("./Images/chad.png");
            Image filtered = Image.FromFile("./Images/xKirsh3x3Vert_yKirsh3x3Vert_Chad.png");
            
            original = ImageFilters.XyFilter("Kirsch3x3Vertical", "Kirsch3x3Vertical", original, 100);
            
            compareImage(original, filtered);

        }

        [TestMethod]
        public void XKirsch3x3Vertical_YKirsch3x3Horizontal()
        {

        }

        [TestMethod]
        public void XKirsch3x3Vertical_YPrewitt3x3Vertical()
        {

        }

        [TestMethod]
        public void XKirsch3x3Horizontal_YKirsch3x3Vertical()
        {

        }

        [TestMethod]
        public void XKirsch3x3Horizontal_Ykirsch3x3Horizontal()
        {

        }

        [TestMethod]
        public void XKirsch3x3Horizontal_YPrewitt3x3Vertical()
        {

        }

        [TestMethod]
        public void XPrewitt3x3Vertical_YKirsch3x3Vertical()
        {

        }

        [TestMethod]
        public void XPrewitt3x3Vertical_YKirsch3x3Horizontal()
        {

        }

        [TestMethod]
        public void XPrewitt3x3Vertical_YPrewitt3x3Vertical()
        {

        }
    }
}

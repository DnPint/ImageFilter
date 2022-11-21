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

            compareImage(original, filtered, 1, 1, 1, 25);
                  
        }

        [TestMethod]
        public void TestMiamiFilter()
        {
            Image original = Image.FromFile("./Images/chad.png");

            Image filtered = Image.FromFile("./Images/miamichad.png");

            compareImage(original, filtered, 1, 1, 10, 1);
        }

        private void compareImage(Image original, Image filtered, int alpha,int red, int blue, int green)
        {
            Bitmap bmpFiltered = new Bitmap(filtered);

            //filter the original with the night filter
            original = ImageFilters.ApplyFilter(new Bitmap(original), alpha,red, blue, green);
            Bitmap bmpOriginal = new Bitmap(original);

            Assert.AreEqual(bmpOriginal.Width, bmpFiltered.Width);

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

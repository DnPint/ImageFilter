using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System;
using System.Resources;

namespace ImageEdgeDetectionTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestNightFilter()
        {
            //var original = ResourceSet.GetObject("mark");
            //var bmp = ImageEdgeDetectionTest.Properties.Resources.mark;
            Image original = Image.FromFile("./Images/mark.jpg");
            
        }

        [TestMethod]
        public void TestMiamiFilter()
        {

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

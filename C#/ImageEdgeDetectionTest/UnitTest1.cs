using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System;
using System.Resources;
using ImageEdgeDetection;
using NSubstitute;
using ImageEdgeDetection.BusinessLayer;
using System.IO;
using System.Reflection;
using ImageEdgeDetection.LoadSaveLayer;

namespace ImageEdgeDetectionTest
{
    [TestClass]
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class UnitTest1
    {
        private void compareImage(Bitmap bmpFiltered, Bitmap bmpOriginal)
        {
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
        public void TestNightFilter()
        {
            var filter = Substitute.For<IFilter>();
            IToolBox tb = new ToolBox();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");
            Bitmap filtered = (Bitmap)Image.FromFile("./Images/nightchad.png");

            //Ensure the user take the NightFilter to test the method
            filter.getFilterName().Returns("NightFilter");

            //filter the original with the night filter
            original = tb.ChooseFilter(filter, original);

            compareImage(original, filtered);   
        }

        [TestMethod]
        public void TestMiamiFilter()
        {
            var filter = Substitute.For<IFilter>();
            IToolBox tb = new ToolBox();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");
            Bitmap filtered = (Bitmap)Image.FromFile("./Images/miamichad.png");

            //Ensure the user take the NightFilter to test the method
            filter.getFilterName().Returns("MiamiFilter");

            //filter the original with the night filter
            original = tb.ApplyFilter(new Bitmap(original), 1, 1, 10, 1);           

            compareImage(original, filtered);
        }

        
        
        [TestMethod]
        public void XKirsch3x3Vertical_YKirsch3x3Vertical()
        {
            var filterX = Substitute.For<IFilter>();
            var filterY = Substitute.For<IFilter>();
            IToolBox tb = new ToolBox();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");
            Bitmap filtered = (Bitmap)Image.FromFile("./Images/xKirsh3x3Vert_yKirsh3x3Vert_Chad.png");

            filterX.getFilterName().Returns("Kirsch3x3Vertical");
            filterY.getFilterName().Returns("Kirsch3x3Vertical");

            original = tb.XyFilter(filterX, filterY, original, 100);
            
            compareImage(original, filtered);

        }

        [TestMethod]
        public void XKirsch3x3Vertical_YKirsch3x3Horizontal()
        {
            var filterX = Substitute.For<IFilter>();
            var filterY = Substitute.For<IFilter>();
            IToolBox tb = new ToolBox();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");
            Bitmap filtered = (Bitmap)Image.FromFile("./Images/XKirsch3x3Vertical_YKirsch3x3Horizontal_Chad.png");

            filterX.getFilterName().Returns("Kirsch3x3Vertical");
            filterY.getFilterName().Returns("Kirsch3x3Horizontal");

            original = tb.XyFilter(filterX, filterY, original, 100);

            compareImage(original, filtered);
        }

        [TestMethod]
        public void XKirsch3x3Vertical_YPrewitt3x3Vertical()
        {
            var filterX = Substitute.For<IFilter>();
            var filterY = Substitute.For<IFilter>();
            IToolBox tb = new ToolBox();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");
            Bitmap filtered = (Bitmap)Image.FromFile("./Images/XKirsch3x3Vertical_YPrewitt3x3Vertical_Chad.png");

            filterX.getFilterName().Returns("Kirsch3x3Vertical");
            filterY.getFilterName().Returns("Prewitt3x3Vertical");

            original = tb.XyFilter(filterX, filterY, original, 100);

            compareImage(original, filtered);
        }

        [TestMethod]
        public void XKirsch3x3Horizontal_YKirsch3x3Vertical()
        {
            var filterX = Substitute.For<IFilter>();
            var filterY = Substitute.For<IFilter>();
            IToolBox tb = new ToolBox();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");
            Bitmap filtered = (Bitmap)Image.FromFile("./Images/XKirsch3x3Horizontal_YKirsch3x3Vertical_Chad.png");

            filterX.getFilterName().Returns("Kirsch3x3Horizontal");
            filterY.getFilterName().Returns("Kirsch3x3Vertical");

            original = tb.XyFilter(filterX, filterY, original, 100);

            compareImage(original, filtered);
        }

        [TestMethod]
        public void XKirsch3x3Horizontal_Ykirsch3x3Horizontal()
        {
            var filterX = Substitute.For<IFilter>();
            var filterY = Substitute.For<IFilter>();
            IToolBox tb = new ToolBox();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");
            Bitmap filtered = (Bitmap)Image.FromFile("./Images/XKirsch3x3Horizontal_YKirsch3x3Horizontal_Chad.png");

            filterX.getFilterName().Returns("Kirsch3x3Horizontal");
            filterY.getFilterName().Returns("Kirsch3x3Horizontal");

            original = tb.XyFilter(filterX, filterY, original, 100);

            compareImage(original, filtered);
        }

        [TestMethod]
        public void XKirsch3x3Horizontal_YPrewitt3x3Vertical()
        {
            var filterX = Substitute.For<IFilter>();
            var filterY = Substitute.For<IFilter>();
            IToolBox tb = new ToolBox();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");
            Bitmap filtered = (Bitmap)Image.FromFile("./Images/XKirsch3x3Horizontal_YPrewitt3x3Vertical_Chad.png");

            filterX.getFilterName().Returns("Kirsch3x3Horizontal");
            filterY.getFilterName().Returns("Prewitt3x3Vertical");

            original = tb.XyFilter(filterX, filterY, original, 100);

            compareImage(original, filtered);
        }

        [TestMethod]
        public void XPrewitt3x3Vertical_YKirsch3x3Vertical()
        {
            var filterX = Substitute.For<IFilter>();
            var filterY = Substitute.For<IFilter>();
            IToolBox tb = new ToolBox();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");
            Bitmap filtered = (Bitmap)Image.FromFile("./Images/XPrewitt3x3Vertical_YKirsch3x3Vertical_Chad.png");

            filterX.getFilterName().Returns("Prewitt3x3Vertical");
            filterY.getFilterName().Returns("Kirsch3x3Vertical");

            original = tb.XyFilter(filterX, filterY, original, 100);

            compareImage(original, filtered);
        }

        [TestMethod]
        public void XPrewitt3x3Vertical_YKirsch3x3Horizontal()
        {
            var filterX = Substitute.For<IFilter>();
            var filterY = Substitute.For<IFilter>();
            IToolBox tb = new ToolBox();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");
            Bitmap filtered = (Bitmap)Image.FromFile("./Images/XPrewitt3x3Vertical_YKirsch3x3Horizontal_Chad.png");

            filterX.getFilterName().Returns("Prewitt3x3Vertical");
            filterY.getFilterName().Returns("Kirsch3x3Horizontal");

            original = tb.XyFilter(filterX, filterY, original, 100);

            compareImage(original, filtered);
        }

        [TestMethod]
        public void XPrewitt3x3Vertical_YPrewitt3x3Vertical()
        {
            var filterX = Substitute.For<IFilter>();
            var filterY = Substitute.For<IFilter>();
            IToolBox tb = new ToolBox();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");
            Bitmap filtered = (Bitmap)Image.FromFile("./Images/XPrewitt3x3Vertical_YPrewitt3x3Vertical_Chad.png");

            filterX.getFilterName().Returns("Prewitt3x3Vertical");
            filterY.getFilterName().Returns("Prewitt3x3Vertical");

            original = tb.XyFilter(filterX, filterY, original, 100);

            compareImage(original, filtered);
        }

        [TestMethod]
        public void ApplyFilterNullBitmap()
        {
            IToolBox tb = new ToolBox();

            //filter the original with the night filter
            Image original = tb.ApplyFilter(null, 1, 1, 1, 25);

            Assert.IsNull(original);
        }

        [TestMethod]
        public void XyfilterMatrixNotFound()
        {
            var filterX = Substitute.For<IFilter>();
            var filterY = Substitute.For<IFilter>();
            var tb = Substitute.For<IToolBox>();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");
            Bitmap filtered = (Bitmap)Image.FromFile("./Images/XKirsch3x3Horizontal_YKirsch3x3Horizontal_Chad.png");

            //Throw specific exception
            tb.When(x => x.XyFilter(filterX, filterY, original, 100)).Do(x => { throw new Exception("An error occured"); });

            Assert.ThrowsException<Exception>(() => tb.XyFilter(filterX, filterY, original, 100));
        }


        [TestMethod]
        public void ApplyFilterBelowZeroRGB()
        {
            IToolBox tb = new ToolBox();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");

            //filter the original with the night filter
            original = tb.ApplyFilter(original, -2, 1, 1, 25);

            Assert.IsNull(original);
        }

        [TestMethod]
        public void ApplyFilterAboveMaxRGB()
        {
            IToolBox tb = new ToolBox();
            Image original = (Bitmap)Image.FromFile("./Images/chad.png");

            //filter the original with the night filter
            original = tb.ApplyFilter(new Bitmap(original), 1, 1, 259, 25);

            Assert.IsNull(original);
        }

        [TestMethod]
        public void XyFilterNullBitmap()
        {
            //Bitmap bitmapResult = new Bitmap(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "chad.png"));
            var filterX = Substitute.For<IFilter>();
            var filterY = Substitute.For<IFilter>();
            IToolBox tb = new ToolBox();

            filterX.getFilterName().Returns("Kirsch3x3Vertical");
            filterY.getFilterName().Returns("Kirsch3x3Vertical");

            Assert.ThrowsException<Exception>(() => tb.XyFilter(filterX, filterY, null, 100)); 
        }

        [TestMethod]
        public void XyFilterException()
        {
            Filter X = new();
            Filter Y = new();
            X.setFilterName("Kirsch3x3Vertical");
            Y.setFilterName("Kirsch3x3Vertical");
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");

            var tb = Substitute.For<IToolBox>();

            tb.XyFilter(X, Y, original, 100).Returns(x => throw new Exception());
            tb.When(x => x.XyFilter(X, Y, original, 100)).Do(x => throw new Exception());

            Assert.ThrowsException<Exception>(() => tb.XyFilter(X, Y, original, 100));
        }

        [TestMethod]
        public void SaveMethodThrowIOException()
        {
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");
            var tb = Substitute.For<IToolBox>();

            tb.When(x => x.SaveImageAppropriateFormat(Arg.Any<Bitmap>())).
                Do(x => { throw new IOException("The write operation could not " +
              "be performed because the specified " +
              "part of the file is locked.");
                });

            Assert.ThrowsException<IOException>(() => tb.SaveImageAppropriateFormat(original));
        }

        [TestMethod]
        public void TestLoadImage()
        {
            var ls = Substitute.For<ILoadSave>();

            //Assume the user clicked on the right image
            ls.LoadImage().Returns(new Bitmap("./Images/chad.png"));
            ls.When(x => x.LoadImage()).Do(x => new Bitmap("./Images/chad.png"));

            //Verify it is loaded and of the right type 
            Assert.IsNotNull(ls.LoadImage());
            Assert.IsInstanceOfType(ls.LoadImage(), typeof(Bitmap));
        }

        [TestMethod]
        public void TestSaveImageAppropriateFormat()
        {
            var ls = Substitute.For<ILoadSave>();
            Image image = new Bitmap("./Images/chad.png");

            //assume the user gave a name to his image
            ls.SaveImageAppropriateFormat(image).Returns(true);

            Assert.IsTrue(ls.SaveImageAppropriateFormat(image));
        }
    }
}

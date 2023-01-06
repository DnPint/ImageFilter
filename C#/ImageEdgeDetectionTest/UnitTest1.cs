using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System;
using ImageEdgeDetection;
using NSubstitute;
using ImageEdgeDetection.BusinessLayer;
using System.IO;
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

            //Ensure the user take the Miami filter to test the method
            filter.getFilterName().Returns("MiamiFilter");

            //filter the original with the Miami filter
            original = tb.ChooseFilter(filter, original);           

            compareImage(original, filtered);
        }

        [TestMethod]
        public void TestMagicMosaicPortrait()
        {
            var filter = Substitute.For<IFilter>();
            IToolBox tb = new ToolBox();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");
            Bitmap filtered = (Bitmap)Image.FromFile("./Images/chadMagicMosaic.png");

            //Ensure the user take the Magic Mosaic to test the method
            filter.getFilterName().Returns("MagicMosaic");

            //filter the original with the Magic Mosaic
            original = tb.ChooseFilter(filter, original);

            compareImage(original, filtered);
        }

        [TestMethod]
        public void TestMagicMosaicLandscape()
        {
            IToolBox tb = new ToolBox();
            var filter = Substitute.For<IFilter>();
            Bitmap original = (Bitmap)Image.FromFile("./Images/honks.png");
            Bitmap filtered = (Bitmap)Image.FromFile("./Images/honksMagicMosaic.png");

            filter.getFilterName().Returns("MagicMosaic");

            original = tb.ChooseFilter(filter, original);

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

            // Filter the original with the night filter
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

            // Throw specific exception
            tb.When(x => x.XyFilter(filterX, filterY, original, 100)).Do(x => { throw new Exception("An error occured"); });

            Assert.ThrowsException<Exception>(() => tb.XyFilter(filterX, filterY, original, 100));
        }


        [TestMethod]
        public void ApplyFilterBelowZeroRGB()
        {
            IToolBox tb = new ToolBox();
            Bitmap original = (Bitmap)Image.FromFile("./Images/chad.png");

            // Filter the original with the night filter
            original = tb.ApplyFilter(original, -2, 1, 1, 25);

            Assert.IsNull(original);
        }

        [TestMethod]
        public void ApplyFilterAboveMaxRGB()
        {
            IToolBox tb = new ToolBox();
            Image original = (Bitmap)Image.FromFile("./Images/chad.png");

            // Filter the original with the night filter
            original = tb.ApplyFilter(new Bitmap(original), 1, 1, 259, 25);

            Assert.IsNull(original);
        }

        [TestMethod]
        public void XyFilterNullBitmap()
        {
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
        public void TestSaveMethodThrowIOException()
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

            // Assume the user clicked on the right image
            ls.LoadImage().Returns(new Bitmap("./Images/chad.png"));
            ls.When(x => x.LoadImage()).Do(x => new Bitmap("./Images/chad.png"));

            // Verify if it is loaded and of the right type 
            Assert.IsNotNull(ls.LoadImage());
            Assert.IsInstanceOfType(ls.LoadImage(), typeof(Bitmap));
        }

       
        [TestMethod]
        public void TestLoadImageFromToolBox()
        {
            var tb = Substitute.For<IToolBox>();
            var ls = Substitute.For<ILoadSave>();

            // Check loadImage method works
            tb.LoadImage().Returns(new Bitmap("./Images/chad.png"));
            tb.When(x => x.LoadImage()).Do(x => new Bitmap("./Images/chad.png"));

            Image image = tb.LoadImage();

            // Verify it is loaded and of the right type
            Assert.IsNotNull(image);
            Assert.IsInstanceOfType(tb.LoadImage(), typeof(Bitmap));
        }

        [TestMethod]
        public void TestSaveImageAppropriateFormat()
        {
            var ls = Substitute.For<ILoadSave>();
            Image image = new Bitmap("./Images/chad.png");

            // Assume the user gave a name to his image
            ls.SaveImageAppropriateFormat(image).Returns(true);

            Assert.IsTrue(ls.SaveImageAppropriateFormat(image));
        }

        [TestMethod]
        public void Get_Gaussian3x3()
        {
            var matrix = Substitute.For<Matrix>();
            var matrixGaussian3x3 = new double[,]
                { { 1, 2, 1, },
                  { 2, 4, 2, },
                  { 1, 2, 1, }, };

            var resultMatrix = matrix.Gaussian3x3;

            CompareMatrices(matrixGaussian3x3, resultMatrix);
        }

        [TestMethod]
        public void Get_Gaussian5x5Type1()
        {
            var matrix = Substitute.For<Matrix>();
            var matrixGaussian5x5Type1 = new double[,]
                { { 2, 04, 05, 04, 2 },
                  { 4, 09, 12, 09, 4 },
                  { 5, 12, 15, 12, 5 },
                  { 4, 09, 12, 09, 4 },
                  { 2, 04, 05, 04, 2 }, };

            var resultMatrix = matrix.Gaussian5x5Type1;

            CompareMatrices(matrixGaussian5x5Type1, resultMatrix);
        }

        [TestMethod]
        public void Get_Gaussian5x5Type2()
        {
            var matrix = Substitute.For<Matrix>();
            var matrixGaussian5x5Type2 = new double[,]
                { {  1,   4,  6,  4,  1 },
                  {  4,  16, 24, 16,  4 },
                  {  6,  24, 36, 24,  6 },
                  {  4,  16, 24, 16,  4 },
                  {  1,   4,  6,  4,  1 }, };

            var resultMatrix = matrix.Gaussian5x5Type2;

            CompareMatrices(matrixGaussian5x5Type2, resultMatrix);
        }

        [TestMethod]
        public void Get_Laplacian3x3()
        {
            var matrix = Substitute.For<Matrix>();
            var matrixLaplacian3x3 = new double[,]
                { { -1, -1, -1,  },
                  { -1,  8, -1,  },
                  { -1, -1, -1,  }, };

            var resultMatrix = matrix.Laplacian3x3;

            CompareMatrices(matrixLaplacian3x3, resultMatrix);
        }

        [TestMethod]
        public void Get_Laplacian5x5()
        {
            var matrix = Substitute.For<Matrix>();
            var matrixLaplacian5x5 = new double[,]
                { { -1, -1, -1, -1, -1, },
                  { -1, -1, -1, -1, -1, },
                  { -1, -1, 24, -1, -1, },
                  { -1, -1, -1, -1, -1, },
                  { -1, -1, -1, -1, -1  }, };

            var resultMatrix = matrix.Laplacian5x5;

            CompareMatrices(matrixLaplacian5x5, resultMatrix);
        }

        [TestMethod]
        public void Get_LaplacianOfGaussian()
        {
            var matrix = Substitute.For<Matrix>();
            var matrixLaplacianOfGaussian = new double[,]
                { {  0,   0, -1,  0,  0 },
                  {  0,  -1, -2, -1,  0 },
                  { -1,  -2, 16, -2, -1 },
                  {  0,  -1, -2, -1,  0 },
                  {  0,   0, -1,  0,  0 }, };

            var resultMatrix = matrix.LaplacianOfGaussian;

            CompareMatrices(matrixLaplacianOfGaussian, resultMatrix);
        }

        [TestMethod]
        public void Get_Prewitt3x3Horizontal()
        {
            var matrix = Substitute.For<Matrix>();
            var matrixPrewitt3x3Horizontal = new double[,]
                { { -1,  0,  1, },
                  { -1,  0,  1, },
                  { -1,  0,  1, }, };

            var resultMatrix = matrix.Prewitt3x3Horizontal;

            CompareMatrices(matrixPrewitt3x3Horizontal, resultMatrix);
        }

        [TestMethod]
        public void Get_Sobel3x3Horizontal()
        {
            var matrix = Substitute.For<Matrix>();
            var matrixSobel3x3Horizontal = new double[,]
                { { -1,  0,  1, },
                  { -2,  0,  2, },
                  { -1,  0,  1, }, };

            var resultMatrix = matrix.Sobel3x3Horizontal;

            CompareMatrices(matrixSobel3x3Horizontal, resultMatrix);
        }

        [TestMethod]
        public void Get_Sobel3x3Vertical()
        {
            var matrix = Substitute.For<Matrix>();
            var matrixSobel3x3Vertical = new double[,]
                { {  1,  2,  1, },
                  {  0,  0,  0, },
                  { -1, -2, -1, }, };

            var resultMatrix = matrix.Sobel3x3Vertical;

            CompareMatrices(matrixSobel3x3Vertical, resultMatrix);
        }

        [TestMethod]
        public void Get_FilterName()
        {
            Filter X = new();
            Filter Y = new();
            X.setFilterName("Kirsch3x3Vertical");
            Y.setFilterName("Kirsch3x3Horizontal");

            Assert.AreEqual("Kirsch3x3Vertical", X.getFilterName());
            Assert.AreEqual("Kirsch3x3Horizontal", Y.getFilterName());

        }


        private void CompareMatrices (double[,] matrixInit, double[,] resultMatrix)
        {
            for (int i = 0; i < matrixInit.GetLength(0); i++)
            {
                for (int j = 0; j < matrixInit.GetLength(1); j++)
                {
                    Assert.AreEqual(matrixInit[i, j], resultMatrix[i, j]);
                }
            }    
        }
    }
}

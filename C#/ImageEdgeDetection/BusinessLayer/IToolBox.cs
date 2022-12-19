using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageEdgeDetection
{
    public interface IToolBox
    {
        Bitmap ApplyFilter(Bitmap bmp, int alpha, int red, int blue, int green);
        Bitmap MagicMosaic(Bitmap bmp);
        Image XyFilter(string xfilter, string yfilter, Image Original, int value);
        Bitmap LoadImage(object sender, EventArgs e);
        void SaveImageAppropriateFormat(Image filtered, SaveFileDialog saveFileDialog);

    }
}
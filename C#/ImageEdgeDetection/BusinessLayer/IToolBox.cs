using ImageEdgeDetection.BusinessLayer;
using System.Drawing;

namespace ImageEdgeDetection
{
    public interface IToolBox
    {
        Bitmap ApplyFilter(Bitmap bmp, int alpha, int red, int blue, int green);
        Bitmap MagicMosaic(Bitmap bmp);
        Bitmap XyFilter(IFilter xfilter, IFilter yfilter, Image Original, int value);
        Bitmap LoadImage();
        void SaveImageAppropriateFormat(Image filtered);
        Bitmap ChooseFilter(IFilter filterName, Bitmap originalBitmap);
    }
}
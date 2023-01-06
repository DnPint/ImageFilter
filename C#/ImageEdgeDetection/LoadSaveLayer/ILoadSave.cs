using System.Drawing;

namespace ImageEdgeDetection.LoadSaveLayer
{
    public interface ILoadSave
    {
        Bitmap LoadImage();
        bool SaveImageAppropriateFormat(Image filtered);
    }
}
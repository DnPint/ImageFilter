using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageEdgeDetection.LoadSaveLayer
{
    public interface ILoadSave
    {
        Bitmap LoadImage(object sender, EventArgs e);
        void SaveImageAppropriateFormat(Image filtered);
    }
}
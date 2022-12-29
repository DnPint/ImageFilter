using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEdgeDetection.LoadSaveLayer
{
    public class LoadSave : ILoadSave
    {
        public Bitmap LoadImage(object sender, EventArgs e)
        {
            //Open the dialog box to get an image on the screen
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select an image file.";
            op.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            op.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            DialogResult dr = op.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string path = op.FileName;
                return (Bitmap)Bitmap.FromFile(path);
            }
            return null;
        }

        public void SaveImageAppropriateFormat(Image filtered, SaveFileDialog saveFileDialog)
        {
            System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
            switch (saveFileDialog.FilterIndex)
            {
                case 1:
                    filtered.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case 2:
                    filtered.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
            }
            fs.Close();
        }
    }
}

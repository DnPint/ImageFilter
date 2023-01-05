using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageEdgeDetection.LoadSaveLayer
{
    public class LoadSave : ILoadSave
    {
        public Bitmap LoadImage()
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

        public bool SaveImageAppropriateFormat(Image filtered)
        {
            SaveFileDialog saveFileDialog = InitializeSaveFileDialog();
            if (saveFileDialog.FileName != "")
            {
                try
                {
                    System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();

                    switch (saveFileDialog.FilterIndex)
                    {
                        case 1:
                            filtered.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                            return true;
                        case 2:
                            filtered.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                            return true;
                        default:
                            return false;
                    }

                }catch (IOException e)
                {
                    throw new IOException("The write operation could not " +
                    "be performed because the specified " +
                    "part of the file is locked.");
                }
            }
            else
            {
                return false;
            }          
        }

        private SaveFileDialog InitializeSaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.ShowDialog();
            return saveFileDialog;
        }
    }
}

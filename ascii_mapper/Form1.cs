using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ascii_mapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Upload_Image_Button_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog {
                Title = "Select Image",
                RestoreDirectory = true,
            }) {
                if (fileDialog.ShowDialog() == DialogResult.Cancel)
                    return;
                
                string filepath = fileDialog.FileName;

                pictureBox1.Image = Image.FromFile(filepath);
            }

            const int MAX_WIDTH = 1200;

            ImageHelper imageHelper = new ImageHelper((Bitmap)pictureBox1.Image);
            imageHelper.Resize(new Size(
                    MAX_WIDTH,
                    (int)Math.Ceiling(MAX_WIDTH / imageHelper.AspectRatio)
                ));


            pictureBox1.Height = imageHelper.Image.Height;
            pictureBox1.Image = imageHelper.Image;
        }
        public Bitmap MakeGrayscale3(Bitmap original)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                ColorMatrix colorMatrix = new ColorMatrix(
                   new float[][]
                   {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                   });

                using (ImageAttributes attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);

                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                       0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

                }
            }

            return newBitmap;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ImageHelper imageHelper = new ImageHelper((Bitmap)pictureBox1.Image);
            pictureBox1.Image = (Image)imageHelper.MakeGrayscale3();

        }

        private void WriteToDisk(string path) 
        {
            
        }


        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            
        }
    }
}

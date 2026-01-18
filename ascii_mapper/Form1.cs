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

        const int MAX_WIDTH = 1200;

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


            ImageHelper imageHelper = new ImageHelper((Bitmap)pictureBox1.Image);
            imageHelper.Resize(
                    MAX_WIDTH,
                    (int)Math.Ceiling(Form1.MAX_WIDTH / imageHelper.AspectRatio)
                );

            pictureBox1.Height = imageHelper.Image.Height;
            pictureBox1.Image = imageHelper.Image;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ImageHelper imageHelper = new ImageHelper((Bitmap)pictureBox1.Image);
            pictureBox1.Image = imageHelper.MakeGrayscale();

        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void DownscaleButton_Click(object sender, EventArgs e)
        {
            ImageHelper imageHelper = new ImageHelper((Bitmap)pictureBox1.Image);
            pictureBox1.Image = imageHelper.MakeDownscale();
        }
    }
}

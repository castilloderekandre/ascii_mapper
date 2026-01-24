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

            ImageWrapper imageWrapper = new ImageWrapper((Bitmap)pictureBox1.Image);
            Bitmap resizedImage = ImageHelper.MakeResize((Bitmap)pictureBox1.Image,
                    MAX_WIDTH,
                    (int)Math.Ceiling(Form1.MAX_WIDTH / imageWrapper.AspectRatio)
                );
            pictureBox1.Height = resizedImage.Height;
            pictureBox1.Image = resizedImage;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ImageWrapper imageWrapper = new ImageWrapper((Bitmap)pictureBox1.Image);
            imageWrapper.ApplyFilter(new Filters.GrayscaleFilter());
            pictureBox1.Image = imageWrapper.Image;

        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void DownscaleButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = ImageHelper.MakeDownscaleByWidth((Bitmap)pictureBox1.Image, 40);
        }
    }
}

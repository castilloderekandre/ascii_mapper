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
        ImageWrapper imageWrapper = null;

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

            imageWrapper = new ImageWrapper((Bitmap)pictureBox1.Image);
            imageWrapper.Resize(MAX_WIDTH,
                    (int)Math.Ceiling(Form1.MAX_WIDTH / imageWrapper.AspectRatio)
                );
            pictureBox1.Height = imageWrapper.Height;
            pictureBox1.Image = imageWrapper.Image;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            imageWrapper.ApplyFilter(new Filters.GrayscaleFilter());
            pictureBox1.Image = imageWrapper.Image;
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void DownscaleButton_Click(object sender, EventArgs e)
        {
            imageWrapper.DownscaleByWidth(40);
            pictureBox1.Image = imageWrapper.Image;
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            this.imageWrapper.Undo();
            pictureBox1.Image = imageWrapper.Image;
        }
    }
}

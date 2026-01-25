using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ascii_mapper
{
    internal class ImageWrapper
    {
        private readonly Stack<Bitmap> _history = new Stack<Bitmap>();
        private Bitmap _image;
        public Bitmap Image 
        {
            get => this._image;
            private set 
            {
                _image = value;
                Width = value.Width;
                Height = value.Height;
            }
        }
        public int Width { get; private set;  }
        public int Height { get; private set; }

        public double AspectRatio { get; private set; }

        public ImageWrapper(Bitmap image)
        {
            this.Image = new Bitmap(image);
            this.AspectRatio = (double)this.Image.Width / this.Image.Height;
            this.Width = this.Image.Width;
            this.Height = this.Image.Height;

            _history.Push(new Bitmap(this.Image));
        }

        public void ApplyFilter(IFilter filter)
        {
            this.Image = filter.Apply(this.Image);
            _history.Push(new Bitmap(this.Image));
        }

        public void Resize(int width, int height)
        {
            this.Image = ImageHelper.MakeResize(this.Image, width, height);
            _history.Push(new Bitmap(this.Image));
        }

        public void DownscaleByWidth(int width)
        {
            this.Image = ImageHelper.MakeDownscaleByWidth(this.Image, width);
            _history.Push(new Bitmap(this.Image));
        }

        public void Undo()
        {
            if (_history.Count > 1)
            {
                _history.Pop();
                this.Image = _history.Peek();
            }
        }

        public void Reset()
        {
            if (_history.Count > 0)
            {
                this.Image = _history.Last();
                _history.Clear();
            }
        }
    }
}

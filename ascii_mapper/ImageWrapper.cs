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
        public Bitmap Image { get; private set; }
        public int Width { get; private set;  }
        public int Height { get; private set; }

        public double AspectRatio { get; private set; }

        public ImageWrapper(Bitmap image)
        {
            this.Image = new Bitmap(image);
            this.AspectRatio = (double)this.Image.Width / this.Image.Height;
            this.Width = this.Image.Width;
            this.Height = this.Image.Height;

            _history.Push(this.Image);
        }

        public void ApplyFilter(IFilter filter)
        {
            this.Image = filter.Apply(this.Image);
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

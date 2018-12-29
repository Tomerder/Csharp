using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shape
{
    class Rectangle : Shape
    {
        
        public int Width { get; private set; }
        public int Height { get; private set; }
        public override float  Area { get { return Width * Height; } }
        public Rectangle() : base() { Width = 0; Height = 0; }
        public Rectangle(int x, int y, int width, int height)
            : base(x,y)
        {
            Width = width;
            Height = height;
        }
        public override void resize(int percent)
        {
            Width = Width * percent;
            Height = Height * percent ;
        }
        
        public override void assign(Shape other)
        {
            base.assign(other);
            Rectangle sOther = other as Rectangle;
            if (sOther != null)
            {
                Width = sOther.Width;
                Height = sOther.Height;
            }
        }
        
        private int min(int first, int second) {
            return (first < second ? first : second);
        }
        private int max(int first, int second)
        {
            return (first > second ? first : second);
        }
        public Rectangle getUnion(Rectangle other)
        {
            Rectangle result = new Rectangle();
            result.X = min(X, other.X);
            result.Y = min(Y, other.Y);
            result.Width = max(Width, other.Width);
            result.Height = max(Height, other.Height);
            return result;
        }
        

        public override string ToString()
        {
            return base.ToString() +  " widht is: " + Width + " height is: " + Height;
        } 
    }
}

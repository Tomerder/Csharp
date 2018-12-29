using System;

namespace RectangleExercise
{
    // There may be a reason to define a common struct Point
    //  that will hold the bottom left point of the rectangle.
    //
    public struct Point
    {
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X;
        public int Y;
    }

    public class Rectangle
    {
        private Point _bottomLeft;
        private int _width;
        private int _height;

        public Rectangle(Point bottomLeft, int width, int height)
        {
            _bottomLeft = bottomLeft;
            _width = width;
            _height = height;
        }

        // Note how this constructor calls the first constructor,
        //  providing the required parameters.
        //
        public Rectangle(Point bottomLeft, Point topRight)
            : this(bottomLeft,
                   topRight.X - bottomLeft.X, topRight.Y - bottomLeft.Y)
        {
        }

        // The following four methods should be replaced
        //  with properties, which will be introduced later.
        //

        public Point GetBottomLeftPoint() { return _bottomLeft; }
        //public Point BottomLeft { get { return _bottomLeft; } }

        public int GetWidth() { return _width; }
        //public int Width { get { return _width; } }

        public int GetHeight() { return _height; }
        //public int Height { get { return _height; } }

        public int GetArea() { return _width * _height; }
        //public int Area { get { return _width * _height; } }

        public void Resize(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public void Move(int x, int y)
        {
            _bottomLeft = new Point(x, y);
        }

        // Note that using the = operator on Rectangle instances
        //  will not perform a memberwise copy, because Rectangle
        //  is a reference type.
        //
        public void Assign(Rectangle other)
        {
            this._bottomLeft = other._bottomLeft;
            this._width = other._width;
            this._height = other._height;
        }

        public bool IsSizeEqual(Rectangle other)
        {
            return this.GetArea() == other.GetArea();
        }

        public Rectangle GetUnion(Rectangle other)
        {
            // Call a private implementation method.
            //
            return GetMinimumBoundingRectangle(other);
        }

        public Rectangle GetMinimum(Rectangle other)
        {
            // Call a private static implementation method.
            //
            return Rectangle.Minimum(this, other);
        }

        private static Rectangle Minimum(Rectangle first, Rectangle second)
        {
            if (first == null)
            {
                //throw new ArgumentNullException...
            }
            if (first.GetArea() < second.GetArea())
            {
                return first;
            }
            else
            {
                return second;
            }
        }

        // Overriding object.ToString allows us to pass our
        //  object directly to Console.WriteLine.
        //
        public override string ToString()
        {
            return string.Format(
                "Bottom left corner: [X={0}, Y={1}], width: {2}, height: {3}",
                _bottomLeft.X, _bottomLeft.Y, _width, _height);
        }

        public void Print()
        {
            // Console.WriteLine(this) would also work.
            //
            Console.WriteLine(this.ToString());
        }
        
        private Rectangle GetMinimumBoundingRectangle(Rectangle other)
        {
            Point newBottomLeft =
                new Point(Math.Min(this._bottomLeft.X, other._bottomLeft.X),
                          Math.Min(this._bottomLeft.Y, other._bottomLeft.Y));

            Point newTopRight =
                new Point(Math.Max(this._bottomLeft.X + this._width,
                                   other._bottomLeft.X + other._width),
                          Math.Max(this._bottomLeft.Y + this._height,
                                   other._bottomLeft.Y + other._height));

            return new Rectangle(newBottomLeft, newTopRight);
        }
    }
}

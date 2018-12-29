using System;

namespace ShapesExercise
{
    public class Rectangle : Shape
    {
        private int _bottomLeftX;
        private int _bottomLeftY;
        private int _topRightX;
        private int _topRightY;

        public Rectangle(int bottomLeftX, int bottomLeftY,
                         int topRightX, int topRightY)
        {
            _bottomLeftX = bottomLeftX;
            _bottomLeftY = bottomLeftY;
            _topRightX = topRightX;
            _topRightY = topRightY;
        }

        public int Width
        {
            get { return _topRightX - _bottomLeftX; }
        }

        public int Height
        {
            get { return _topRightY - _bottomLeftY; }
        }

        #region Shape implementation

        public override void Resize(int percentage)
        {
            double heightIncrement = this.Height * (((double)percentage) / 100);
            double widthIncrement = this.Width * (((double)percentage) / 100);
            _topRightY += (int)heightIncrement;
            _topRightX += (int)widthIncrement;
        }

        public override void Move(int x, int y)
        {
            int heightIncrement = x - _bottomLeftX;
            int widthIncrement = y - _bottomLeftY;
            
            _bottomLeftX = x;
            _bottomLeftY = y;
            _topRightX += widthIncrement;
            _topRightY += heightIncrement;
        }

        public override void Print()
        {
            Console.WriteLine("Rectangle: bottom left=[{0},{1}], top right: [{2},{3}], " +
                              "width={4}, height={5}", _bottomLeftX, _bottomLeftY,
                              _topRightX, _topRightY, this.Width, this.Height);
        }

        #endregion
    }
}

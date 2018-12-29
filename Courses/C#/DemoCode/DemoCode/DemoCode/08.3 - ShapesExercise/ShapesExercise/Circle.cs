using System;

namespace ShapesExercise
{
    public class Circle : Shape
    {
        private int _x;
        private int _y;
        private int _radius;

        public Circle(int x, int y, int radius)
        {
            _x = x;
            _y = y;
            _radius = radius;
        }

        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        #region Shape implementation

        public override void Resize(int percentage)
        {
            double increment = _radius * (((double)percentage) / 100);
            _radius += (int)increment;
        }

        public override void Move(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override void Print()
        {
            Console.WriteLine("Circle: center=[{0},{1}], radius={2}",
                              _x, _y, _radius);
        }

        #endregion
    }
}

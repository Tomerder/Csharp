using System;

namespace RectangleExercise
{
    class MyClass
    {
        public void Eat() { }
        public void Sleep() { }
        public int GetAge() { return 5; }
    }

    class RectangleDriver
    {
        private static Rectangle _hardCodedRectangle =
            new Rectangle(new Point(1, 1), 4, 5);

        static void Foo(int i, int j, int o)
        {

        }

        static void Main(string[] args)
        {
            Point bottomLeft;
            Console.Write("Rectangle bottom left point, X: ");
            bottomLeft.X = int.Parse(Console.ReadLine());
            Console.Write("Rectangle bottom left point, Y: ");
            bottomLeft.Y = int.Parse(Console.ReadLine());

            int height, width;
            Console.Write("Rectangle height: ");
            height = int.Parse(Console.ReadLine());
            Console.WriteLine("Rectangle width: ");
            width = int.Parse(Console.ReadLine());

            //TODO: add support for multiprocessor machines

            Rectangle userSuppliedRectangle = new Rectangle(bottomLeft, width, height);

            Console.WriteLine("Are equal? " +
                userSuppliedRectangle.IsSizeEqual(_hardCodedRectangle));
            Console.WriteLine("Yours is smaller? " +
                (userSuppliedRectangle.GetMinimum(_hardCodedRectangle) == userSuppliedRectangle));
            Console.WriteLine("Union: " +
                userSuppliedRectangle.GetUnion(_hardCodedRectangle));

            _hardCodedRectangle.Assign(userSuppliedRectangle);
            Console.WriteLine("Are they equal after Rectangle.Assign? " +
                (userSuppliedRectangle == _hardCodedRectangle));

            _hardCodedRectangle = userSuppliedRectangle;
            Console.WriteLine("Are they equal after using operator = ? " +
                (userSuppliedRectangle == _hardCodedRectangle));
        }
    }
}

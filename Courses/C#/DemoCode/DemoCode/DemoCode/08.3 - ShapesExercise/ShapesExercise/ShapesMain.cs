using System;

namespace ShapesExercise
{
    class ShapesMain
    {
        static void Main(string[] args)
        {
            CompoundShape compoundShape = new CompoundShape();
            compoundShape.Add(new Rectangle(1, 1, 3, 3));
            compoundShape.Add(new Circle(5, 5, 2));
            compoundShape.Print();

            compoundShape.Move(4, 4);
            compoundShape.Print();

            compoundShape.Resize(50);
            compoundShape.Print();
        }
    }
}

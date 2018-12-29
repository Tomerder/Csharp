using System;
using System.Collections.Generic;

namespace ShapesExercise
{
    // The CompoundShape class implements the Composite design pattern.
    //  It provides a Shape interface (by means of implementing the
    //  Shape abstract class), but may contain a collection of Shape
    //  objects, applying each operation to each of the objects.
    //
    public class CompoundShape : Shape
    {
        // This can be solved by using an array, which we already know.
        //  A List<T> is a dynamic generic collection, similar to the STL
        //  vector<T> class.
        //
        private List<Shape> _shapes = new List<Shape>();

        public CompoundShape()
        {
        }

        public void Add(Shape shape)
        {
            _shapes.Add(shape);
        }

        #region Shape implementation

        public override void Resize(int percentage)
        {
            foreach (Shape shape in _shapes)
            {
                shape.Resize(percentage);
            }
        }

        public override void Move(int x, int y)
        {
            foreach (Shape shape in _shapes)
            {
                shape.Move(x, y);
            }
        }

        public override void Print()
        {
            Console.WriteLine("Compound shape: number of shapes={0}",
                              _shapes.Count);
            Console.WriteLine("Shapes listing:");
            foreach (Shape shape in _shapes)
            {
                shape.Print();
            }
        }

        #endregion
    }
}

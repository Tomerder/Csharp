using System;
using System.Collections.Generic;
using System.Text;

namespace Struct
{
    // This is the definition of an enum.  We will learn
    //  more about it in this lesson.
    //
    enum Color
    {
        None,
        Red,
        Green,
        Blue
    }

    // This is the definition of a struct.  Note that
    //  a struct has accessibility levels as well, so this
    //  one is implicitly 'internal'.
    //
    struct Point
    {
        // These are the struct's fields.  Note that for
        //  C# struct fields the default accessibility level
        //  is still private.  This is in contrast with
        //  C++ struct fields, which are public by default.
        //
        public int _x;
        public int _y;
        public Color _color;

        // A struct may have a constructor that accepts parameters.
        //  It may not have a parameterless constructor -- it will
        //  not compile.
        //
        public Point(int x, int y, Color color)
        {
            _x = x;
            _y = y;
            _color = color;
        }

        // A struct may have more than one constructor, and it
        //  it allowed for it to use other constructors, just as
        //  in the case with classes.  However, every single
        //  field of the struct must be assigned before control
        //  leaves the constructor method.  Failing to do this
        //  generates a compilation error.
        //
        public Point(int x, int y)
            : this(x, y, Color.None)
        {
        }
    }

    class StructDemo
    {
        static void Main(string[] args)
        {
            // The following line initializes a stack-based Point
            //  object.  The fact that we're using 'new' here
            //  does not mean that the struct is allocated on the
            //  heap.  The r-value is a temporarily allocated
            //  struct that is copied into the stack-based 'point'
            //  variable.
            //
            Point point = new Point(5, 3);

            // Another alternative we have is:
            //
            Point point2;
            point2._color = Color.Green;
            point2._x = point2._y = 5;

            // Note that we cannot use the struct until we have
            //  manually initialized all its fields.  Attempting
            //  to do so will cause a compilation error.
        }
    }
}

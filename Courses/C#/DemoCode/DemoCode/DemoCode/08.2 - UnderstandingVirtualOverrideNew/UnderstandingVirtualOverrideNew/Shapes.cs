using System;

namespace UnderstandingVirtualOverrideNew
{
    public class Shape
    {
        protected int _size;

        public Shape(int size) { _size = size; }
        public int GetSize() { return _size; }

        // This method is declared virtual, implying that it is
        //  intended to be overriden later in the class hierarchy.
        //
        public virtual void Draw() { }

        // Uncomment the following line to see the compiler warning.
        //  See Circle.Print for the discussion.
        //
        //public virtual void Print() { }
    }

    public class Circle : Shape
    {
        // This constructor calls the base class constructor with
        //  the provided radius, but uses the local _size field
        //  to store the circle area.
        //
        public Circle(int radius) : base(radius)
        { this._size = (int)(radius * radius * Math.PI); }

        // This method overrides Shape.Draw which was declared virtual.
        //
        public override void Draw() { base.Draw(); }

        // This method hides Shape.GetSize, and therefore we should use
        //  the 'new' keyword to prevent a compiler warning.
        //
        public new int GetSize() { return _size; }

        // This method is a regular method that is not declared in the
        //  base class.  However, the base class author might decide, in
        //  the next version, that he wants this method to be available
        //  to Shape clients.  If he adds a virtual (or non-virtual)
        //  Shape.Print method, we will get a compiler warning, which is
        //  perfectly well and intended, because at the time of writing
        //  the Circle.Print method, we were NOT overriding a base class
        //  method.
        //
        public void Print() { }

        // This field hides Shape._size, and therefore we should use
        //  the 'new' keyword to prevent a compiler warning.
        //
        protected new int _size;
    }
}

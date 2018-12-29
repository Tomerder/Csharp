using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shape 

{
    //abstract class Shape : IComparable
    abstract class Shape 
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public Shape() { X = 0; Y = 0; }
        public Shape(int x, int y)
        {
            X = x;
            Y = y;
        }
        public abstract float Area { get; }
        

        public abstract void resize(int percent);
       
        public virtual void move(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }
        public virtual void assign(Shape other)
        {
            X = other.X;
            Y = other.Y;
        }
        public bool isSizeEqual(Shape other)
        {
            return (Area == other.Area);
        }

        public Shape minimum(Shape other)
        {
            if (Area < other.Area)
                return this;
            return other;
        }
        public override string ToString()
        {
            return "x is: " + X + " y is: " + Y ;
        }


        #region IComparable Members

        //public int CompareTo(object obj)
        //{
        //    Shape other = (Shape)obj;
        //    return this.Area.CompareTo(other.Area);
        //}

        #endregion
    }
}

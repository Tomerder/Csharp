using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shape
{
    class Circle : Shape
    {
        public float Radius {get; private set; }
        public Circle() : base() { Radius = 0; }
        public Circle(int x, int y, float radius)
            : base(x, y)
        {
            Radius = radius;
        }
       
        public override float Area { get { return (3.14f * Radius * Radius); } }
        public override void resize(int percent)
        {
            Radius = Radius * percent;
            
        }
        public override void assign(Shape other)
        {
            base.assign(other);
            Circle sOther = other as Circle;
            if (sOther != null)
            {
                Radius = sOther.Radius;
               
            }
        }

        public override string ToString()
        {
            return base.ToString() + " Radius is: " + Radius;
        } 
    }
}

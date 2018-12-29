using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dollar
{
     class Shekl
    {
        public float Value
        {   get;
            private set;
        }
        public Shekl(float value)
        {
            Value = value;
        }
        public static bool operator >(Shekl d1, Shekl d2)
        {
            return d1.Value > d2.Value;
        }
        public static bool operator <(Shekl d1, Shekl d2)
        {
            return d2 > d1;
        }
        public override bool Equals(object obj)
        {
            Shekl other;
            other = obj as Shekl;

            return other.Value == this.Value;
        }

        public static bool operator ==(Shekl d1, Shekl d2)
        {
            return d1.Equals(d2);
        }
        public static bool operator !=(Shekl d1, Shekl d2)
        {
            return (!(d1 == d2));
        }
        public static Shekl operator ++(Shekl d1)
        {
            d1.Value++;
            return d1;
        }
        public static Shekl operator --(Shekl d1)
        {
            d1.Value++;
            return d1;
        }
        public override int GetHashCode()
        {
            return (int)((Value*100))/100;
        }
        

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dollar
{
    class Dollar
    {
        public float Value
        {   get;
            private set;
        }
        public Dollar(float value)
        {
            Value = value;
        }
        public static bool operator >(Dollar d1, Dollar d2)
        {
            return d1.Value > d2.Value;
        }
        public static bool operator <(Dollar d1, Dollar d2)
        {
            return d2 > d1;
        }
        public override bool Equals(object obj)
        {
            Dollar other;
            other = obj as Dollar;

            return other.Value == this.Value;
        }

        public static bool operator ==(Dollar d1, Dollar d2)
        {
            return d1.Equals(d2);
        }
        public static bool operator !=(Dollar d1, Dollar d2)
        {
            return (!(d1 == d2));
        }
        public static Dollar operator ++(Dollar d1)
        {

            Dollar d2 = new Dollar(d1.Value);
            d2.Value++;
            return d2;

        }
        public static Dollar operator --(Dollar d1)
        {
            Dollar d2 = new Dollar(d1.Value);
            d2.Value--;
            return d2;
        }
        public override int GetHashCode()
        {
            return (int)((Value*100))/100;
        }
        public static explicit operator Shekl (Dollar d) {
            return new Shekl(d.Value * 4);
        }

        public static explicit operator Dollar(Shekl s)
        {
            return new Dollar(s.Value / 4);
        }

        public static explicit operator float(Dollar d)
        {
            return d.Value;
        }

    }
}

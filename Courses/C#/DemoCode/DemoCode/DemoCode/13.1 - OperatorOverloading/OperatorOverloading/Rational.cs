using System;

namespace OperatorOverloading
{
    // The following is a class that represents rational numbers.
    //  It has two fields: a numerator and a denominator (reminder:
    //  in the number 3/5, 3 is the numerator and 5 is the denominator).
    // Note: this is not a fully functional class.
    //
    class Rational
    {
        private int _numerator;
        private int _denominator;

        public int Numerator
        {
            get { return _numerator; }
            set { _numerator = value; }
        }

        public int Denominator
        {
            get { return _denominator; }
            set { _denominator = value; }
        }

        public Rational(int numerator, int denominator)
        {
            _numerator = numerator;
            _denominator = denominator;
        }

        public Rational(int numerator)
            : this(numerator, 1)
        {
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is Rational)
            {
                return this == (obj as Rational);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _numerator ^ _denominator;
        }

        // This is our first overloaded operator.  It allows us
        //  to compare instances of the Rational class.  Note that
        //  overriding operator== produces a compiler warning
        //  if we have not overriden object.Equals and object.GetHashCode,
        //  so their implementations are provided above.
        //
        public static bool operator ==(Rational r1, Rational r2)
        {
            return r1.Numerator == r2.Numerator &&
                   r1.Denominator == r2.Denominator;
        }

        // Overriding operator== requires us to override operator!=.
        //  Obviously, they can use each other.
        //
        public static bool operator !=(Rational r1, Rational r2)
        {
            return !(r1 == r2);
        }

        // Our Rational class can be cast to double, implicitly.
        //
        public static implicit operator double(Rational r)
        {
            return ((double)r.Numerator) / r.Denominator;
        }

        // Our Rational class can be cast to int, explicitly.
        //
        public static explicit operator int(Rational r)
        {
            return r.Numerator / r.Denominator;
        }

        public static Rational operator +(Rational r, int value)
        {
            r.Numerator += r.Denominator * value;
            return r;
        }

        // When overriding operator++, we can use the definion
        //  of operator+.  Note that we don't provide two versions
        //  of ++ (one for postfix and one for prefix).  They are
        //  automatically generated by the compiler.
        //
        public static Rational operator ++(Rational r)
        {
            return r + 1;
        }

        // Override object.ToString so that we can display this object.
        //
        public override string ToString()
        {
            return String.Format("{0} / {1}", this.Numerator, this.Denominator);
        }
    }
}
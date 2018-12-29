using System;
using System.Text;

namespace Generics
{
    // This is the generic class definition.  Note the use of the
    //  type parameter T throughout the class.
    class Vector<T> where T : IComparable
    {
        private T[] _data;

        public Vector(int size)
        {
            _data = new T[size];
        }

        public void Sort()
        {
            _data[0].CompareTo(_data[1]);
        }

        public int Size
        {
            get { return _data.Length; }
        }

        public T this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        public void Fill(T value)
        {
            for (int i = 0; i < this.Size; ++i)
                this[i] = value;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < this.Size; ++i)
            {
                result.AppendFormat(this[i] + " ");
            }
            return result.ToString();
        }
    }
}

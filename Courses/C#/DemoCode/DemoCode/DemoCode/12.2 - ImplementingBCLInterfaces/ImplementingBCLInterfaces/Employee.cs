using System;

namespace ImplementingBCLInterfaces
{
    // The Employee class implements the IComparable interface, so that
    //  objects of this type can be compared to each other.  For example,
    //  it enables us to sort an array of objects of this type using the
    //  Array.Sort static method.
    // It also implements the IFormattable method which allows us to
    //  specify special formatting for Employee objects.
    //
    class Employee : IComparable, IFormattable
    {
        private string _name;
        private int _seniority;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Seniority
        {
            get { return _seniority; }
            set { _seniority = value; }
        }

        public Employee(string name, int seniority)
        {
            _name = name;
            _seniority = seniority;
        }

        public override string ToString()
        {
            return string.Format("[{0}] --> {1}", _name, _seniority);
        }

        #region IComparable Members

        // Note that we implement the IComparable interface explicitly,
        //  so that it can be called only through an IComparable reference
        //  to our object.  This prevents polluting the class with public
        //  members that we don't intend others to call.
        //
        int IComparable.CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            // Ensure that we have an Employee in our possession.
            //  Otherwise, we have no means of comparing to it.
            Employee employee = obj as Employee;
            if (employee == null)
            {
                throw new ArgumentException("Argument was not an Employee object", "obj");
            }
            // Compare employees by name.
            return String.Compare(this._name, employee._name);
        }

        #endregion

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "F":
                    return string.Format("Employee details:\nName: {0}\nSeniority: {1}",
                                         _name, _seniority);
                case "":    // Can't use String.Empty here because it's not a const.
                case null:
                default:
                    return this.ToString();
            }
        }

        #endregion
    }
}

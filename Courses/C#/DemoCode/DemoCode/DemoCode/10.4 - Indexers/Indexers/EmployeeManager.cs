using System;
using System.Collections.Generic;

namespace Indexers
{
    class EmployeeManager
    {
        private List<Employee> _employees = new List<Employee>();

        // Note that this property is not backed by a field.
        //  It is calculated every time it is accessed.
        //
        public int Count
        {
            get { return _employees.Count; }
        }

        public void Add(Employee employee)
        {
            _employees.Add(employee);
        }

        // This is an indexer for the EmployeeManager class.  It is
        //  quite similar to the C++ operator[] syntax.  In general,
        //  it's nothing but a regular property.  See the IndexersDemo
        //  class for usage details.
        //
        public Employee this[int index]
        {
            get
            {
                RangeCheck(index);
                return _employees[index];
            }
            set
            {
                RangeCheck(index);
                _employees[index] = value;
            }
        }

        // Note that since the C# compiler converts the indexer
        //  into a couple of simple methods (get_Item, set_Item),
        //  the following code will not compile because the set_Item
        //  method is already defined.
        //
        //public void set_Item(int index, Employee value) { }

        private void RangeCheck(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
        }

        // Other methods omitted for brevity.
    }
}

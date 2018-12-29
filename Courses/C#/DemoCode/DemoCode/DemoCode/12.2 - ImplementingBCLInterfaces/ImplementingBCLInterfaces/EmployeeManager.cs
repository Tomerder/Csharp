using System;
using System.Collections;

namespace ImplementingBCLInterfaces
{
    // The EmployeeManager class implements the IEnumerable interface,
    //  meaning that its contents may be iterated using an enumerator.
    //  This enables us to use the 'foreach' statement on objects of
    //  this type.
    //
    class EmployeeManager : IEnumerable
    {
        private Employee[] _employees;

        public EmployeeManager(int numEmployees)
        {
            _employees = new Employee[numEmployees];
        }

        public Employee this[int index]
        {
            get { return _employees[index]; }
            set { _employees[index] = value; }
        }

        #region IEnumerable Members
        
        // The only method in the IEnumerable interface is the
        //  GetEnumerator() method.  It is required to return
        //  another object that implements the IEnumerator interface.
        //  We provide such an object by using an inner class.
        // Another alternative is using the C# 2.0 'yield return'
        //  statement, which generates this inner class automatically
        //  in compile-time.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Employee_Enumerator(_employees);

            // Alternative:  (C# 2.0 only)
            //
            //  foreach (Employee employee in _employees)
            //  {
            //      yield return employee;
            //  }
        }

        #endregion

        public class Employee_Enumerator : IEnumerator
        {
            private Employee[] _employees;
            // Note that the index starts at -1, because MoveNext
            //  must be called before iterating the collection.
            private int _currentIndex = -1;

            // This class accepts the array of employees
            //  and keeps track of current the index.
            //
            public Employee_Enumerator(Employee[] employees)
            {
                _employees = employees;
            }

            #region IEnumerator Members

            public object Current
            {
                get { return _employees[_currentIndex]; }
            }

            public bool MoveNext()
            {
                return ++_currentIndex < _employees.Length;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }

            #endregion
        }
    }
}

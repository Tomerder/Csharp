using System;

namespace ThrowingExceptions
{
    class Employee
    {
        // Most of the code from the previous exercise omitted
        //  for brevity.

        public int Salary;
    }

    class EmployeeManager
    {
        // Most of the code from the previous exercise omitted
        //  for brevity.

        private int _numEmployees;
        
        public EmployeeManager(int numEmployees)
        {
            // If the number of employees provided is less than or equal
            //  to 0, we throw an ArgumentOutOfRangeException exception,
            //  and specify a custom error message that can be displayed.
            //
            if (numEmployees <= 0)
            {
                throw new ArgumentException("Illegal numEmployees number " +
                    "provided: " + numEmployees + "\nnumEmployees should be > 0");
            }

            _numEmployees = numEmployees;
        }

        public Employee this[int index]
        {
            get
            {
                // In the case that the index specified is outside the legal
                //  bounds, we can throw a built-in IndexOutOfRangeException exception.
                //
                if (index < 0 || index >= _numEmployees)
                {
                    throw new IndexOutOfRangeException();
                }
                // The actual code omitted for brevity.
                return new Employee();
            }
            // The set accessor omitted for brevity.
        }
    }
}

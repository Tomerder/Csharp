using System;

namespace EmployeeClass
{
    public class Employee
    {
        public Employee(string name, float salary)
        {
            // The use of 'this' to access private members
            //  is not required.  Sometimes, however, it
            //  makes the code more readable.
            //
            this._name = name;
            this._salary = salary;
        }

        public Employee(string name)
            // The following line actually invokes another
            //  constructor (which we have seen previously).
            //
            : this(name, _minimumSalary)
        {
        }

        // The following two methods could have been implemented
        //  using properties.  This will be introduced in further
        //  lessons.
        //
        public string GetName() { return _name; }
        public float GetSalary() { return _salary; }

        public void Work()
        {
            Console.WriteLine("Employee {0} is working...", _name);
        }

        public void RaiseSalary(int percentage)
        {
            _salary += _salary * percentage / 100;
        }

        public static void RaiseMinimumSalary(int percentage)
        {
            _minimumSalary += _minimumSalary * percentage / 100;
        }

        public float CalculateSalary()
        {
            if (_salary < _minimumSalary)
            {
                _salary = _minimumSalary;
            }
            return _salary;
        }

        private string _name;
        private float _salary;
        
        // The following field is static, meaning that it is
        //  shared across all instances of the Employee class.
        //
        static private float _minimumSalary = 6000.0f;
        
    }

    class EmployeeApp
    {
        static EmployeeApp()
        {
            // uses the logger for the first time.
        }

        static void Main(string[] args)
        {
            // There is no way to create an instance of the Employee
            //  class without using 'new', because it is a reference
            //  type.
            //
            Employee jack = new Employee("Jack Smith", 6000.0f);
            Employee mary = new Employee("Mary White", 7000.0f);
            
            jack.Work();
            mary.Work();

            // Mary worked very hard (completed the release on time!)...
            mary.RaiseSalary(5);

            // Time to calculate salaries...
            Console.WriteLine("Jack's salary: " + jack.CalculateSalary());
            Console.WriteLine("Mary's salary: " + mary.CalculateSalary());

            // Time to raise the minimum salary...
            //  Note that static methods are invoked using dot (.)
            //  notation, without requiring an instance of the type.
            //
            Employee.RaiseMinimumSalary(5);

            // More employee-management code omitted for brevity.
        }
    }
}

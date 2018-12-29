using System;

namespace DelegatesExercise_Solution
{
    class Employee
    {
        private string _name;
        private int _salary;

        public Employee(string name, int salary)
        {
            _name = name;
            _salary = salary;
        }

        public string Name { get { return _name; } }
        public int Salary { get { return _salary; } }
    }
}

using System;

namespace Indexers
{
    class Employee
    {
        private string _name;
        private int _salary;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Salary
        {
            get { return _salary; }
            set { _salary = value; }
        }

        public Employee(string name, int salary)
        {
            _name = name;
            _salary = salary;
        }
    }
}

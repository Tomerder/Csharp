using System;

namespace Attributes
{
    [LastModified(DeveloperName = "Masha", DateModified = "1/1/06")]
    public class Employee
    {
        [LastModified(DateModified = "20/11/06", DeveloperName = "Sasha")]
        private int _salary;
        private string _name;

        [LastModified("Dasha", "1/5/06")]
        public Employee(string name, int salary)
        {
            _name = name;
            _salary = salary;
        }

        public int Salary
        {
            [LastModified("Pasha", "1/7/06")]
            get { return _salary; }
            set { _salary = value; }
        }
    }
}

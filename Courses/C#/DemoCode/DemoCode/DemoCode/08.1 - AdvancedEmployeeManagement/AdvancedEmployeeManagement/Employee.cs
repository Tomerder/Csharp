using System;

namespace AdvancedEmployeeManagement
{
    public class Employee
    {
        private string _name;
        // This is not the best OOP-practice in the C# world.
        //  When properties are introduced, we will see that
        //  it's best to have *all* fields private, and declare
        //  a protected property whenever it's necessary to
        //  expose a field to derived classes.
        protected int _salary;
        private static int _minimumSalary = 6000;

        public Employee(string name, int salary)
        {
            this._name = name;
            this._salary = salary;
        }

        // By declaring this method virtual, we allow it to be
        //  overriden by deriving classes.  Note that a virtual
        //  method cannot be private (it makes no sense).
        //
        public virtual void CalculateSalary()
        {
            if (_salary < _minimumSalary)
            {
                _salary = _minimumSalary;
            }
        }

        // Note the override keyword, which tells the compiler
        //  that the ToString method we are defining overrides
        //  the virtual implementation that we implicitly
        //  inherited from System.Object.
        //
        public override string ToString()
        {
            return "Employee name: " + _name;
        }

        // Again, the property syntax is commented out.
        //
        public int GetSalary() { return _salary; }
        //public int Salary
        //{
        //  get { return _salary; }
        //  protected set { _salary = value; }
        //}
    }
}

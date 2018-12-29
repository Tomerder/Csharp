using System;

namespace AdvancedEmployeeManagement
{
    public class Payroll
    {
        // The Payroll class handles all employees in a polymorphic way.
        //  We have here an array of Employee objects, but as we have
        //  seen in the EmployeeMain.cs file, the actual objects added
        //  to this array are objects that derive from Employee, i.e.
        //  ProjectManager and Programmer objects.
        //
        private Employee[] _employees;
        private int _numEmployees;

        public Payroll(int maxEmployees)
        {
            _employees = new Employee[maxEmployees];
        }

        public bool AddEmployee(Employee employee)
        {
            if (_numEmployees < _employees.Length)
            {
                _employees[_numEmployees++] = employee;
                return true;
            }
            return false;
        }

        public void CalculateSalaries()
        {
            for (int i = 0; i < _numEmployees; ++i)
            {
                // The Employee.CalculateSalary method is virtual,
                //  and therefore this invocation will call the
                //  correct method in any deriving class that overrides
                //  it (ProjectManager.CalculateSalary or
                //  Programmer.CalculateSalary).
                //
                _employees[i].CalculateSalary();
            }
        }

        public void PrintReport()
        {
            Console.WriteLine("Payroll report:");
            for (int i = 0; i < _numEmployees; ++i)
            {
                Employee emp = _employees[i];
                Console.WriteLine(emp + " salary: " + emp.GetSalary());
            }
        }
    }
}

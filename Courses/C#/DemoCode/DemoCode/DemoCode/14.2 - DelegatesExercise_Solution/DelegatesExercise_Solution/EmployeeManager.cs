using System;
using System.Collections.Generic;

namespace DelegatesExercise_Solution
{
    delegate int SortDelegate(Employee e1, Employee e2);

    class EmployeeManager
    {
        private List<Employee> _employees = new List<Employee>();

        public void Add(Employee emp)
        {
            _employees.Add(emp);
        }

        public void Sort(SortDelegate sorter)
        {
            // Sort implementation omitted ;-)

            // Sample of using the delegate instance:
            int result = sorter(_employees[0], _employees[1]);
        }
    }
}

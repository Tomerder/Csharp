using System;
using System.Collections.Generic;

namespace Nullables
{
    class EmployeeManager
    {
        // A List<> is a generic collection that auto-expands
        //  when elements are added to it.
        //
        private List<Employee> _employees = new List<Employee>();

        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
        }

        // However, now we are approached with a different problem.
        //  What are we to do if the FindEmployee method cannot find
        //  the requested employee?  We could say 'return null', but
        //  Employee is a struct, so it's a value type, and it has
        //  no null value.  Therefore, we abandon this method and
        //  move on to the next approach.
        //
        public Employee FindEmployee_Naive(string employeeName)
        {
            throw new NotImplementedException();
        }

        // The following code sample appears to solve the problem,
        //  but it isn't very convenient to use.  It requires passing
        //  an INITIALIZED Employee structure, and can return false
        //  if no record was found.  We can't use an 'out' parameter
        //  here because if we can't find a record, we have no value
        //  to assign to the 'result' parameter.  So this is still
        //  quite unsatisfactory.
        //
        public bool FindEmployee_HardToUse(string employeeName, ref Employee result)
        {
            throw new NotImplementedException();
        }

        // The following method, surprisingly, returns something quite
        //  different from 'Employee'.  We haven't drilled down the
        //  Generics section yet, but for now it's enough to say that
        //  Nullable<Employee> is a type, similar to a C++ template,
        //  that acts as a wrapper for the Employee value type.  Unlike
        //  the Employee type itself, Nullable<Employee> can hold two
        //  distinct types of values: a null value, or a valid Employee
        //  struct.  It means that this method can return a null value,
        //  or a valid Employee struct, which is exactly what we were
        //  trying to achieve.
        //
        public Nullable<Employee> FindEmployee_Final(string employeeName)
        {
            foreach (Employee employee in _employees)
            {
                if (employee.Name == employeeName)
                {
                    // Nullable<Employee> can be constructed with
                    //  an Employee object.
                    //
                    return employee;
                }
            }
            // Nullable<Employee> can also be constructed with
            //  a null 'object', even though it is a value type.
            //  Have a look at the calling code to see how we
            //  can check what the return value was.
            return null;
        }
    }
}

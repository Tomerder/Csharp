using System;
using System.Collections;

namespace ImplementingBCLInterfaces
{
    class EmployeeComparer_BySeniority : IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentNullException();
            }
            Employee employee1 = x as Employee;
            Employee employee2 = y as Employee;
            if (employee1 == null || employee2 == null)
            {
                throw new ArgumentException("Argument was not an Employee object");
            }
            return employee2.Seniority - employee1.Seniority;
        }

        #endregion
    }
}

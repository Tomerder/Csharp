using System;
using System.Collections;

namespace ImplementingBCLInterfaces
{
    // The following class implements the IComparer interface.  This means
    //  it's a type that knows how to compare two objects.  This one compares
    //  two Employee objects by their name.
    //
    class EmployeeComparer_ByName : IComparer
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
            return String.Compare(employee1.Name, employee2.Name);
        }

        #endregion
    }
}

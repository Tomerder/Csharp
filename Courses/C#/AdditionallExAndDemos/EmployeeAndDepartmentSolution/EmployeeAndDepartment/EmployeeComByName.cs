using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EmployeeAndDepartment
{
    class EmployeeComByName: IComparer

    {
        public int Compare(object x, object y)
        {
            Employee e1 = x as Employee;
            Employee e2 = y as Employee;
            if (e1 == null || e2 == null)
                throw new InvalidCastException();
            return e1.Name.CompareTo(e2.Name);
        }
    }
}

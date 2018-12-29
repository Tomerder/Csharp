using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EmployeeAndDepartment
{
    
    
    class Department 
    {
            Employee[] _emps;
            int _counter = 0;

            public Department(int size)
            {
                _emps = new Employee[size];
            }

            public void addEmployee(Employee emp)
            {
                if (_counter < _emps.Length)
                {
                    _emps[_counter++] = emp;
                }

            }
            public void sort()
            {
                
                Array.Sort(_emps, 0, _counter);
            }
            public void sort(IComparer comp)
            {
                Array.Sort(_emps,comp);
            }

            public override string ToString()
            {
                StringBuilder sr = new StringBuilder();
                for (int i = 0; i < _counter; i++)
                {
                    sr.AppendLine(_emps[i].ToString());
                }

                return sr.ToString();
            }

        
        




        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EmployeeAndDepartment
{
    public delegate int EmpComp(Employee e1, Employee e2);
    
    public class EmpDepException : Exception
    {
        public int Index { get; private set; }
        public EmpDepException() { }
        public EmpDepException(string message) : base(message) { }
        public EmpDepException(string message, Exception inner) : base(message, inner) { }
        public EmpDepException(int ind) { Index = ind; }
    }
    class Department :IEnumerable
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
                throw new IndexOutOfRangeException();

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

        public Employee this[int ind] 
        {
            get
            {
                if (ind < 0 || ind > _counter)
                    throw new EmpDepException(ind);
                return _emps[ind];
            }
        }
        public Employee this[String name]
        {
            get
            {
                for (int i = 0; i < _counter; i++)
                    if (_emps[i].Name == name)
                        return _emps[i];

                return null;
            }
        }




        public IEnumerator GetEnumerator()
        {
            //foreach (Employee emp in _emps)
            for (int i = 0; i < _counter; i++)
                //if (_emps[i].Seniority == Seniority.EntryLevel)
                    yield return _emps[i];
        }

        public void Sort(EmpComp comp)
        {

            for (int i =0; i < _counter; i++)
                for (int j = i; j < _counter; j++)
                {
                    if (comp(_emps[j], _emps[j + 1]) > 0)
                    {
                        Employee temp = _emps[j];
                        _emps[j] = _emps[j + 1];
                        _emps[j + 1] = temp;
                    }
                }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullableEx
{
    class Program
    {

        public struct Employee
        {
            public string Name { get; set; }
        }
        public class Department
        {
            Employee[] _emps = new Employee[2];
            int _counter = 0;

            public void addEmployee(Employee emp)
            {
                if (_counter < _emps.Length)
                {
                    _emps[_counter++] = emp;
                }

            }
            public Employee? findEmployeeWithName(string name)
           //  bool  findEmployeeWithName(string name, out Employee emp)
            {
                //Problem: Employee is not a reference type!

                for (int i = 0; i < _counter; i++)
                {
                    if (_emps[i].Name == name)
                    {

                        return _emps[i];
                        
                    }
                }
                return null;
            }

        }

        static void Main(string[] args)
        {

            Employee e1 = new Employee();
            e1.Name = "lll";
            Employee e111 = new Employee();
            e111.Name = "yossi";
            Department dep = new Department();
            dep.addEmployee(e1);
            dep.addEmployee(e111);
            // how can we implement findEmployeeWithName to test cases of no employee found?
            //-----------------------------------------------------------------------------------
            Employee? e2 = dep.findEmployeeWithName("yossi");
            Employee e3;
            if (e2 != null)
                 e3 = e2.Value;
            
            if (dep.findEmployeeWithName("yossi") == null)
                Console.WriteLine("Could not find Employee");

            //Employee e1 = new Employee();
            //e1.Name = "Dany";
            //dep.addEmployee(e1);
            //e1.Name = "Yossi";
            //dep.addEmployee(e1);

            int? ni=null;
            if (ni == null)
                ni = 5;
           



           
        }
    }
}

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
            {
                

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


            Department dep = new Department();
            if (dep.findEmployeeWithName("yossi") == null)
                Console.WriteLine("Could not find Employee");

            Employee e1 = new Employee();
            e1.Name = "Dany";
            dep.addEmployee(e1);

            Employee? val = dep.findEmployeeWithName("Dany");
            if (val != null)
            {
                Console.WriteLine("found  : ");
                Console.WriteLine(val.Value.Name);
            }



            int? ni =null;
            if (ni == null)
                Console.WriteLine("variable is null");
            else
                Console.WriteLine("variable is not null");

            Console.ReadLine();
        }
    }
}

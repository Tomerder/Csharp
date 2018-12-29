using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeAndDepartment
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Seniority s1 = Seniority.EntryLevel;
           
            Console.WriteLine(s1);
            Employee e1 = new Employee("Ariel", 20, Seniority.Experienced);
            Employee e2 = new Employee("Zork", 15, Seniority.EntryLevel);
            Employee e3 = new Employee("Dan", 55, Seniority.Experienced);
            Console.WriteLine(e1);
            Console.WriteLine("Employee: {0:N}", e1);
            Console.WriteLine("Employee: {0:A}", e1);
            Console.WriteLine("Employee: {0:S}", e1);
           
            
            Department dep = new Department(3);
            dep.addEmployee(e1);
            dep.addEmployee(e2);
            dep.addEmployee(e3);

            dep.sort();

            //foreach (Employee emp in dep)
            //{
            //    Console.WriteLine(emp);
            //}
            //dep.sort(new EmployeeComByName());
            //Console.WriteLine(dep);

            Console.WriteLine(dep);
            //try
            //{
            //    Console.WriteLine(dep[5]);
            //}
            //catch (EmpDepException e)
            //{

            //    Console.WriteLine("Error! wrong index is: " + e.Index);
            //}
            //finally
            //{

            //    Console.ReadLine();
            //}
        }
    }
}

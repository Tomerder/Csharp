using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeAndDepartment
{
    //class SystemArry
    //{
    //    public static void sort(object[] arr)
    //    {
    //        int length = arr.Length;
    //        for (int i = 0; i < length; i++)
    //        {
    //            for (int j = 0; j < length; j++)
    //            {
    //                if (!(arr[i] is IComparable))
    //                    throw new InvalidCastException();
    //                if (!(arr[j] is IComparable))
    //                    throw new InvalidCastException();
    //                if (((IComparable) (arr[i])).CompareTo(arr[j]) > 0)
    //            }
    //        }
    //    }
    //}
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

            foreach (Employee emp in dep)
            {
                Console.WriteLine(emp);
            }

            // Sort using IComparer
            dep.sort(new EmployeeComByName());

            // Isn't this easier? Using delegates
            dep.Sort((e11,e12) => e11.Age - e12.Age));
       
            Console.WriteLine(dep);

            Console.WriteLine(dep);
            try
            {
                Console.WriteLine(dep[5]);
            }
            catch (EmpDepException e)
            {
                
                Console.WriteLine("Error! wrong index is: " + e.Index);
            }
            finally
            {

                Console.ReadLine();
            }
        }
    }
}

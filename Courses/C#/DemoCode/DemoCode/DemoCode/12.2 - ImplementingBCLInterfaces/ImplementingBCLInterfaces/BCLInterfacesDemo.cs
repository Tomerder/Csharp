using System;
using System.Collections;

namespace ImplementingBCLInterfaces
{
    class BCLInterfacesDemo
    {
        static void Main(string[] args)
        {
            Employee rick = new Employee("Rick", 3);
            Employee jack = new Employee("Jack", 1);
            Employee mary = new Employee("Mary", 2);

            Employee[] employees = { rick, jack, mary };
            PrintEmployees(employees);

            Array.Sort(employees);  // We can do this thanks to IComparable.
            PrintEmployees(employees);

            // We can do this thanks to IComparer.
            Array.Sort(employees, new EmployeeComparer_BySeniority());
            PrintEmployees(employees);

            EmployeeManager manager = new EmployeeManager(3);
            manager[0] = rick;
            manager[1] = jack;
            manager[2] = mary;

            PrintEmployees(manager);  // We can do this thanks to IEnumerable.
        }

        // The PrintEmployees method takes an IEnumerable parameter,
        //  meaning that we can pass to it arrays, built-in collections,
        //  or any type of ours that implements IEnumerable.
        //
        private static void PrintEmployees(IEnumerable employees)
        {
            Console.WriteLine();
            foreach (Employee employee in employees)
            {
                // Note the use of the :F format specifier that
                //  is passed to Employee.IFormattable.ToString.
                Console.WriteLine("{0:F}", employee);
            }
            Console.WriteLine();
        }
    }
}

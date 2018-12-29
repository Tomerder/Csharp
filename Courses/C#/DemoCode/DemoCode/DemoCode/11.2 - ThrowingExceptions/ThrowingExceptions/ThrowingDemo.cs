using System;

namespace ThrowingExceptions
{
    class ThrowingDemo
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Accessing an index outside the bounds of the array...\n");
                EmployeeManager manager = new EmployeeManager(10);
                Employee emp = manager[11];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.ReadLine();

            try
            {
                Console.WriteLine("Creating an EmployeeManager with an invalid argument...\n");
                EmployeeManager manager = new EmployeeManager(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.ReadLine();

            try
            {
                Console.WriteLine("Attempting to initialize the EmployeeManager...\n");
                EmployeeManager manager;
                EmployeeOperations.InitEmployeeManager(out manager);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Note that e.InnerException contains the exception wrapped
                //  by this Exception object.
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.ReadLine();

            try
            {
                Console.WriteLine("Attempting to raise the salaries...\n");

                EmployeeManager manager = new EmployeeManager(10);
                EmployeeOperations.RaiseSalaries(manager, 11);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Note that e.InnerException contains the exception wrapped
                //  by this Exception object.
            }
        }
    }
}

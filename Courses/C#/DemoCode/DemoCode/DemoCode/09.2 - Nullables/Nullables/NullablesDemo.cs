using System;

namespace Nullables
{
    static class Util
    {
        // The following method is supposed to return the smallest
        //  factor of a given number.  For example, if we pass 21,
        //  we should expect to receive 3 as an answer.  However,
        //  what are we to do if we pass a prime number to this
        //  method?  We have to either adopt some kind of error
        //  code strategy, or simply return an integer value that
        //  is part of our contracts with the users, that indicates
        //  that the number is prime.  In this case, if we find
        //  no factor, we return -1.
        //
        static public int ReturnSmallestFactor(int number)
        {
            int root = (int)Math.Ceiling(Math.Sqrt(number));
            for (int i = 2; i < root; ++i)
            {
                if (number % i == 0)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    class NullablesDemo
    {
        static void Main(string[] args)
        {
            // Create the EmployeeManager object and add some
            //  employees to it.
            //
            EmployeeManager employeeManager = new EmployeeManager();
            employeeManager.AddEmployee(new Employee("John", 1));
            employeeManager.AddEmployee(new Employee("Mary", 2));

            Console.Write("Enter employee name to find: ");
            string employeeName = Console.ReadLine();
            // The EmployeeManager.FindEmployee_Final method returns
            //  a Nullable<Employee> object.  However, C# provides
            //  a special '?' notation to make working with nullables
            //  a bit easier:
            Employee? employee = employeeManager.FindEmployee_Final(employeeName);

            // Nullable<Employee>.HasValue is a boolean property
            //  that allows us to determine whether the value returned
            //  by the method was null or a real Employee structure.
            //
            if (employee.HasValue)
            {
                // If there was a value, we can access it using the
                //  Nullable<Employee>.Value property.
                //
                Console.WriteLine("Found an employee, his/her id is: " + employee.Value.Id);
            }
            else
            {
                Console.WriteLine("Couldn't find an employee with that name");
            }
        }
    }
}

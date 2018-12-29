using System;

namespace ThrowingExceptions
{
    static class EmployeeOperations
    {
        static public void InitEmployeeManager(out EmployeeManager manager)
        {
            try
            {
                manager = new EmployeeManager(-1);
            }
            catch (Exception innerException)
            {
                // We catch the exception thrown by the EmployeeManager,
                //  and wrap with a new exception that provides more
                //  contextual information.
                //
                throw new Exception("Error while initializing employee manager",
                                    innerException);
            }
        }

        static public void RaiseSalaries(EmployeeManager manager, int numEmployees)
        {
            try
            {
                for (int i = 0; i < numEmployees; ++i)
                {
                    manager[i].Salary += 100;
                }
            }
            catch (Exception innerException)
            {
                // We catch the exception thrown by the EmployeeManager,
                //  and wrap with a new exception that provides more
                //  contextual information.
                //
                throw new Exception("Error while raising salaries",
                                    innerException);
            }
        }
    }
}

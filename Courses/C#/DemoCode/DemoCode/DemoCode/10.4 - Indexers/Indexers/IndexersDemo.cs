using System;

namespace Indexers
{
    class IndexersDemo
    {
        static void Main(string[] args)
        {
            EmployeeManager manager = new EmployeeManager();
            manager.Add(new Employee("Jack", 10000));
            manager.Add(new Employee("Dave", 8000));

            // Now we can use the indexer to implement an
            //  operation that the author of the EmployeeManager
            //  class did not consider useful.
            //
            for (int i = 0; i < manager.Count; ++i)
            {
                manager[i].Salary += 500;
            }
        }
    }
}

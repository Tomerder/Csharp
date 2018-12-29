using System;

namespace DelegatesExercise_Solution
{
    class DelegatesExerciseMain
    {
        static void Main(string[] args)
        {
            EmployeeManager manager = new EmployeeManager();
            manager.Add(new Employee("Jack", 10000));
            manager.Add(new Employee("Rick", 20000));
            manager.Add(new Employee("Rita", 15000));

            // In .NET 2.0, we can use:
            manager.Sort(ByNameSorter);

            // In .NET 1.1, this would be:
            manager.Sort(new SortDelegate(ByNameSorter));
        }

        static int ByNameSorter(Employee e1, Employee e2)
        {
            return String.Compare(e1.Name, e2.Name);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegateEmployees
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeManager manager = new EmployeeManager();
            manager.Add(new Employee() { Name = "Shlomo", Salary = 7000 });
            manager.Add(new Employee() { Name = "Noam", Salary = 17000 });
            manager.Add(new Employee() { Name = "Yossi", Salary = 5000 });
            manager.Add(new Employee() { Name = "Tomer", Salary = 2000 });

            manager.Print();

            EmployeeManager.isGreater greaterAction = Employee.isGreaterByName;

            manager.generalSort(greaterAction);

            manager.Print();

            EmployeeManager.isGreater greaterAction2 = Employee.isGreaterBySalary;

            manager.generalSort(greaterAction2);

            manager.Print();
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public float Salary { get; set; }

        public override string ToString()
        {
            return string.Format("Name {0}, Salary {1}", Name, Salary);
        }
        public static bool isGreaterByName(Employee e1, Employee e2) {
            return (string.Compare(e1.Name,e2.Name) > 0);
        }
        public static bool isGreaterBySalary(Employee e1, Employee e2)
        {
            return (e1.Salary > e2.Salary);
        }
    }

    public class EmployeeManager
    {
        public delegate bool isGreater (Employee e1, Employee e2);
        private List<Employee> _employees = new List<Employee>();

        public void Add(Employee emp)
        {
            _employees.Add(emp);
        }

        public void Print()
        {
            foreach (var item in _employees)
            {
                Console.WriteLine(item);
            }
        }
#if test
        public void SortByName()
        {
            for (int i = 0; i < _employees.Count; i++)
            {
                for (int j = 0; j < _employees.Count - 1; j++)
                {
                    if (_employees[j].Name.CompareTo(_employees[j + 1].Name) == 1)
                    {
                        var tmp = _employees[j];
                        _employees[j] = _employees[j + 1];
                        _employees[j + 1] = tmp;
                    }
                }
            }
        }
#endif

        public  void generalSort(isGreater greaterAction)
        {
            for (int i = 0; i < _employees.Count; i++)
            {
                for (int j = 0; j < _employees.Count - 1; j++)
                {
                    if (greaterAction(_employees[j],_employees[j + 1]) )
                    {
                        var tmp = _employees[j];
                        _employees[j] = _employees[j + 1];
                        _employees[j + 1] = tmp;
                    }
                }
            }
        }

    }
}

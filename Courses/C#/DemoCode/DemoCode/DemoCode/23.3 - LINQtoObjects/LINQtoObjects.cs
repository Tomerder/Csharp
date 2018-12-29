using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Ch4_LINQtoObjects
{
    class Employee
    {
        public Employee()
        {
            City = "London";
        }

        public string Name { get; set; }
        public int Salary { get; set; }
        public string City { get; set; }
    }

    static class PayrollExtensions
    {
        public static IEnumerable<T> Select<T>(this PayrollSystem payroll, Func<Employee,T> selector)
        {
            return payroll.employees.Select(selector);
        }
    }

    class PayrollSystem
    {
        internal List<Employee> employees = new List<Employee>();

        public void Add(Employee e)
        {
            employees.Add(e);
        }

        public int GetTotalMonthlySalary()
        {
            return employees.Sum(e => e.Salary);
        }

        public IEnumerable<Employee> Where(Func<Employee, bool> filter)
        {
            return employees.Where(e => e.City=="London" && filter(e));
        }

        //public IEnumerable<T> Select<T>(Func<Employee, T> selector)
        //{
        //    return employees.Select(selector);
        //}
    }

    class LINQtoObjects
    {
        static void Main(string[] args)
        {
            PayrollSystem payroll = new PayrollSystem();
            payroll.Add(new Employee { Name = "John", Salary = 200 });
            payroll.Add(new Employee { Name = "Mary", Salary = 250, City = "Paris" });
            payroll.Add(new Employee { Name = "Kate", Salary = 230 });
            payroll.Add(new Employee { Name = "Mike", Salary = 280 });

            var query = from employee in payroll
                        where employee.Salary > 220
                        select employee.Name;
            query.ToList().ForEach(Console.WriteLine);

            //LINQ to Strings
            string s = Guid.NewGuid().ToString();
            string digitsOnly =
                new string((from c in s
                            where Char.IsDigit(c)
                            select c).ToArray());
            Console.WriteLine(s + Environment.NewLine + digitsOnly);

            //LINQ to Reflection
            var queryableTypes =
                from asm in AppDomain.CurrentDomain.GetAssemblies()
                from t in asm.GetExportedTypes()
                where t.GetInterfaces().Any(itf =>
                    itf.IsGenericType &&
                        ((itf.IsGenericTypeDefinition && itf == typeof(ICollection<>)) ||
                         (!itf.IsGenericTypeDefinition && itf.GetGenericTypeDefinition() == typeof(ICollection<>)))
                         ) ||
                      typeof(IEnumerable).IsAssignableFrom(t) && t.GetMethods().Any(m => m.Name == "Add")
                select t;
            queryableTypes.ToList().ForEach(t => Console.WriteLine(t.FullName));

            //LINQ to File System
            var largeDllFiles =
                from file in Directory.GetFiles(
                    Environment.GetFolderPath(Environment.SpecialFolder.System))
                let info = new FileInfo(file)
                where info.Extension == ".dll" && info.Length > 5000000
                select info.FullName;
            largeDllFiles.ToList().ForEach(Console.WriteLine);

            //LINQ to WCF contracts
            var operations =
                from operation in ContractDescription.GetContract(typeof(IService)).Operations
                where !operation.IsOneWay
                where operation.Faults.Any(f => f.DetailType == typeof(string))
                select new { operation.DeclaringContract.Namespace, operation.SyncMethod.Name };
            operations.ToList().ForEach(Console.WriteLine);
        }

        [ServiceContract]
        interface IService
        {
            [OperationContract]
            [FaultContract(typeof(Exception))]
            string Echo(string s);
            [OperationContract]
            [FaultContract(typeof(string))]
            void Bar(int n);
            [OperationContract]
            [FaultContract(typeof(Exception)), FaultContract(typeof(string))]
            void Baz(double d);
            [OperationContract(IsOneWay = true)]
            void Foo(string str);
        }
    }
}

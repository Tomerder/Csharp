using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CovarianceAndContravariance
{
    class Program
    {
        static void Main(string[] args)
        {
            // Demo 1
            var arr = new Employee[] { new Employee(10), new Employee(51), new Employee(17) };
            Array.Sort<Employee>(arr, new Comparison<Person>((x, y) => x.Id.CompareTo(y.Id)));

            //Demo 2
            Action<object> printObject = PrintObject;
            Action<string> printString = printObject; // Dose not compile in previous version of C#
        }

        static void PrintObject(object o)
        {
            Console.WriteLine(o);
        }

    }

    class Person
    {
        public int Id { get; set; }

        public Person(int id)
        {
            Id = id;
        }
    }

    class Employee : Person
    {
        public Employee(int id) : base(id)
        {

        }
    }
}
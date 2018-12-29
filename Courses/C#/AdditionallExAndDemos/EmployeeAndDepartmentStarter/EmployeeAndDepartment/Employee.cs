using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeAndDepartment
{
    enum Seniority { EntryLevel, Experienced, Senior }
    class Employee : IFormattable
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Seniority Seniority { get; set; }
        public Employee(string name, int age, Seniority sen)
        {
            Name = name;
            Age = age;
            Seniority = sen;
        }
        public Employee(string name) : this(name, 20, Seniority.EntryLevel) { }

        public override string ToString()
        {
            return String.Format("Name: {0} Age: {1} Seniority: {2}", Name, Age, Seniority);
        }


        public string ToString(string format, IFormatProvider formatProvider)
        {
           if (format== null || format== default)
               return ToString();

            switch (format)
            {

            case "S":
            return string.Format ("name is {0}", Name);

            }

    }
    }
}
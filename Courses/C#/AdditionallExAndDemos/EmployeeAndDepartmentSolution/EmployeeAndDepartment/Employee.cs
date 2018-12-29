using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeAndDepartment
{
    enum Seniority { EntryLevel, Experienced, Senior }
    class Employee 
        : IComparable, IFormattable
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



        //public int CompareTo(object obj)
        //{
        //    //throw new NotImplementedException();
        //    Employee other = obj as Employee;
        //    if (obj == null)
        //        throw new ArgumentException();
        //    return (Age - other.Age);
        //}

        public virtual int CompareTo(object obj)
        {
            //throw new NotImplementedException();
            if (obj != null)
            {
                Employee other = obj as Employee;
                if (other == null)
                    throw new InvalidCastException();
                //return (Age - other.Age);
                return Age.CompareTo(other.Age);

            }
            throw new InvalidCastException();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null || format == String.Empty)
                return ToString();
            switch (format)
            {
                case "N":
                    return String.Format("Name: {0} ", Name);
   
                    
                case "A":
                    return String.Format("Name: {0} Age: {1}", Name,Age);

                    
                case "S":
                    return String.Format("Name: {0} Seniority: {1}", Name, Seniority);
   
                    
                default:
                    return ToString();
            }
        }
    }
}

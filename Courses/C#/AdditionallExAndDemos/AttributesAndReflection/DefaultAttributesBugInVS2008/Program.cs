using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AttributeBugDemo
{
    public class Person
    {
        public  string Name { get; internal set;} 
        public int Age {get; internal set;}
        public Person ([DefaultValue ("Michal"), optional]string name, int age)
        {
            Name = name;
            age = age;
        }

        
    }
    class Program
    {
        static void Main(string[] args)
        {

            Person p1 = new Person("SelectedName", 25);

            // this should not compile
            //Person p2 = new Person(); 

            // this should compile, but not in VS2009
            Person p3 = new Person(25);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AttributeBugDemo
{
    public class Person
    {
        public  string Name { get; internal set;} 
        public int Age {get; internal set;}

        public Person(int age,[DefaultValue("Michal"), Optional]string name)
        //public Person( int age,string name = "Michal")
        //public Person( int age,string name)
        {
            //            Note  from Lib documentation
            //A DefaultValueAttribute will not cause a member to be automatically initialized with the attribute's value. 
            //You must set the initial value in your code.


            if (name != null)
                Name = name;
            else Name = "Michal";
            Age = age;
        }

        
    }
    class Program
    {
        static void Main(string[] args)
        {

             Person p1 = new Person( 25,"SelectedName");

            // this should not compile
            //Person p2 = new Person(); 

            // this should compile, but not in VS2008
           Person p3 = new Person(25);
           
            
        }
    }
}

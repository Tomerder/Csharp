using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace ReflectionAdditionalExample
{
    public class Person
    {
	public string Name {set; get;}
        private int _age;
        public Person (string name, int age) 
        {
           Name = name; 
           _age = age;
        }
        public void doSomething(int i)
        {
            Console.WriteLine("In doSomething, method of Person");
            _age = _age + i;
        }
        public override string ToString() 
        {
 		return "Name : " + Name + " Age: "  + _age;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person("John",20);

            Type t = p.GetType();
            //Type t = typeof(Person);
            MethodInfo[] miarr = t.GetMethods();

            MethodInfo mi = t.GetMethod("doSomething");
        
            object [] paramArr = new object[1];
            paramArr[0] = 6;
            //mi.Invoke(p, new object[0]);
	        mi.Invoke(p, paramArr);

            Console.WriteLine(p);

            // note _age is a private field!!

            FieldInfo fi = t.GetField("_age", BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(p, 50);

            Console.WriteLine(p);
            ShowMetadata(p);
        }

        private static void ShowMetadata(object o)
        {
            Type t = o.GetType();
            Console.WriteLine("Type {0}", t.FullName);
            foreach (MethodInfo m in t.GetMethods())
            {
                Console.WriteLine("\tMethod {0}", m.Name);
                foreach (ParameterInfo p in m.GetParameters())
                {
                    Console.WriteLine("\t\tParam {0}", p.Name);
                }
                Console.WriteLine("\t\tRet: {0}", m.ReturnType.Name);
            }
            foreach (FieldInfo f in t.GetFields())
            {
                Console.WriteLine("Field {0}", f.Name);
            }
        }


    }

}

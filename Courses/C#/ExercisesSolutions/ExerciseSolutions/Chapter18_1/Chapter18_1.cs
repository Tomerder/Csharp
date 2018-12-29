using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chapter18_1
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class SingletonAttribute : Attribute
    {
        public string Name { get; set; }

        public SingletonAttribute(string name)
        {
            this.Name = name;
        }
    }

    [Singleton("Printer")]
    public class Printer
    {
        public void Print()
        {
            Console.WriteLine("Hello World from Printer");
        }
    }

    public static class Repository
    {
        private static Dictionary<string, object> _singletons = new Dictionary<string, object>();

        static Repository()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                foreach (Type type in assembly.GetExportedTypes())
                {
                    SingletonAttribute[] attributes = (SingletonAttribute[]) type.GetCustomAttributes(typeof (SingletonAttribute), false);
                    if (attributes.Length != 0)
                    {
                        object singleton = Activator.CreateInstance(type);
                        _singletons.Add(attributes[0].Name, singleton);
                    }
                }
        }

        public static object GetInstance(string name)
        {
            return _singletons[name];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Printer printer = (Printer)Repository.GetInstance("Printer");
            printer.Print();
        }
    }
}
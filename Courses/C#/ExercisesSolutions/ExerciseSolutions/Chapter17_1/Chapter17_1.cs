using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chapter17_1
{
    public interface ISingleton
    {
        string Name { get; }
    }

    public class Printer : ISingleton
    {
        public string Name
        {
            get { return "Printer"; }
        }

        public void Print()
        {
            Console.WriteLine("Hello World from Printer");
        }
    }

    public static class Repository
    {
        private static Dictionary<string, ISingleton> _singletons = new Dictionary<string, ISingleton>();

        static Repository()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                foreach (Type type in assembly.GetExportedTypes())
                {
                    if (type.GetInterface(typeof(ISingleton).FullName) != null)
                    {
                        ISingleton singleton = (ISingleton) Activator.CreateInstance(type);
                        _singletons.Add(singleton.Name, singleton);
                    }
                }
        }

        public static ISingleton GetInstance(string name)
        {
            return _singletons[name];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Printer printer = (Printer) Repository.GetInstance("Printer");
            printer.Print();
        }
    }
}

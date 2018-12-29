using System;
using System.Reflection;

namespace Reflection
{
    // This is the sample class we will be reflecting on.
    public class DemoClass
    {
        public int Add(int i, int j) { return i + j; }
        public void Print() { }
    }

    class ReflectionDemo
    {
        static void Main(string[] args)
        {
            // The following line gives us the currently executing assembly.
            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            // We dynamically obtain the System.Type object for the
            //  Reflection.DemoClass class (above).
            Type demoType = executingAssembly.GetType("Reflection.DemoClass");
            // We dynamically create an instance of the Reflection.DemoClass
            //  class (above).
            object demoObj = executingAssembly.CreateInstance("Reflection.DemoClass");

            ReflectionInfoDisplayer displayer =
                new ReflectionInfoDisplayer(demoType);

            displayer.DisplayGeneralInfo();
            displayer.DisplayMethods();

            // We dynamically invoke the "Add" method.
            Console.WriteLine(displayer.InvokeMethod(demoObj, "Add", 5, 3));
        }
    }
}

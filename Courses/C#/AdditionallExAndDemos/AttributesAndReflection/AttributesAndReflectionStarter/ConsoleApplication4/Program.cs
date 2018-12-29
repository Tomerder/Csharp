using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace QueryApplicaion
{
    class Program
    {
        static void Main(string[] args)
        {
            // Student: ask the user to enter a dll name
            string assemblyNAme = Console.ReadLine();

            Assembly assm = Assembly.LoadFrom(assemblyNAme);
            var types = assm.GetTypes().Where(t => t.IsClass).ToArray();
            foreach (var type in types)
            {
                // Student: 
                //Check if the class has TestClass attribute
                // Then:
                object o = Activator.CreateInstance(type);
                
                MethodInfo[] methods = type.GetMethods();
                foreach (var method in methods)
                {
                    // Student: 
                    //Check if the method has TestMethod attribute
                    // Then:
                    method.Invoke(o, new object[0]);
                }
            }
        }
    }
}

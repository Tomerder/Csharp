using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using TestAttributesClasses;

namespace QueryingAttributesExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string assemblyNAme = Console.ReadLine();

            Assembly assm = Assembly.LoadFrom(assemblyNAme);
            var types = assm.GetTypes().Where(t => t.IsClass).ToArray();
            foreach (var type in types)
            {
                //Check if TestClass exist
                var atts = type.GetCustomAttributes(typeof(TestClassAttributes), false);
                
               
                foreach (var classWithAtt in atts)
                    // we will get in only if we are a type that got the attribute
                {

                    object o = Activator.CreateInstance(type);

                    MethodInfo[] methods = type.GetMethods();
                    foreach (var method in methods)
                    {
                        //Check if TestMethod exist
                        var attsMethods = method.GetCustomAttributes(typeof(TestMethodAttributes), false);
                        foreach (var methodWithAttributes in attsMethods)
                        {

                            method.Invoke(o, new object[0]);
                        }
                    }
                }
            }
        }
    }
}

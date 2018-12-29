using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Q1A
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly a = Assembly.LoadFrom("Q1B.exe");
            Type t = a.GetType("A");
            object aInstance = Activator.CreateInstance(t);
            t.InvokeMember("F", BindingFlags.InvokeMethod, null, aInstance, null);
        }
    }
}

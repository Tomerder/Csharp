using Plugin;
using System;
using System.Reflection;

namespace MainHost
{
    /// <summary>
    /// This demo highlights the problem of assembly loading context.  The rules
    /// of assembly binding in the CLR are very complicated, and it's possible
    /// for the same assembly to be loaded into the default load context and into
    /// the load-from context.  Types from the two assemblies are considered
    /// incompatible even they "are the same type".
    /// 
    /// This phenomenon is explained in greater detail in the slides and in the following
    /// blog post:
    ///     http://blogs.microsoft.co.il/blogs/sasha/archive/2007/03/06/Assembly-Load-Contexts-Subtleties.aspx
    /// </summary>
    class Program
    {
        static void PrintLoadedAssemblies()
        {
            Console.WriteLine("------------------");
            Array.ForEach(AppDomain.CurrentDomain.GetAssemblies(),
                delegate(Assembly a) { Console.WriteLine(a.Location); });
        }

        static void Main(string[] args)
        {
            MyPlugin m = new MyPlugin();
            PrintLoadedAssemblies();

            Assembly plugin = Assembly.LoadFrom(@"..\..\..\Plugin\bin\Debug\Plugin.dll");

            PrintLoadedAssemblies();

            Method(plugin);

            PrintLoadedAssemblies();
        }

        static void Method(Assembly plugin)
        {
            //This cast fails because the two MyPlugin types are incompatible.
            //The MyPlugin type which appears in the code is resolved by the JIT
            //to the load context, but the Plugin.MyPlugin type which is created
            //using reflection (Assembly.CreateInstance) is resolved to the
            //load-from context.  The exception message (as of .NET 3.5) discloses
            //the problem right away.
            MyPlugin myPlugin2 = (MyPlugin)plugin.CreateInstance("Plugin.MyPlugin");
        }
    }
}

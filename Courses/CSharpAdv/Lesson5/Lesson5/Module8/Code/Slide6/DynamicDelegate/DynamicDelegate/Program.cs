using System;
using System.Reflection;

namespace DynamicDelegate
{
    public delegate string LogMethodDelegate();

    class Program
    {
        static void Main(string[] args)
        {
            Box box = new Box { Name = "Chips", Volume = 2.0m };
            LogMethodDelegate logGenerator = DynamicLog(box, "Log");
            string log = logGenerator();
            Console.WriteLine(log);
        }

        private static LogMethodDelegate DynamicLog(object @object, string name)
        {
            Type type = @object.GetType();
            MethodInfo logMethod = type.GetMethod(name);
            LogMethodDelegate logGenerator = (LogMethodDelegate)Delegate.CreateDelegate(typeof(LogMethodDelegate), @object, logMethod);
            return logGenerator;
        }
    }
}

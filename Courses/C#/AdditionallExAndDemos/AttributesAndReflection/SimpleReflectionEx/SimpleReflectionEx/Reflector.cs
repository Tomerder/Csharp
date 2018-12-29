using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SimpleReflectionEx
{
    class Reflector
    {
        private string _className;
        private Type _type;
        private object _obj;
        public Reflector(object obj)
        {
            if (obj != null)
            {
                _type = obj.GetType();
                _className = _type.Name;
                _obj = obj;
            }
        }
        public void showAllMethods()
        {
            Console.WriteLine("All class Public Methods:");
            MethodInfo[] mi = _type.GetMethods();
            foreach (MethodInfo method in mi)
                Console.WriteLine(method.Name);
        }
        public void showAllFields()
        {
            Console.WriteLine("All class Fields:");
            FieldInfo[] mi = _type.GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance);
            foreach (FieldInfo field in mi)
                Console.WriteLine(field.Name);
        }
        public void activateTestMethod()
        {
            MethodInfo mi = _type.GetMethod("test");
            if (mi == null)
                Console.WriteLine("method test does not exist in class");
            else
            {
                //object o = Activator.CreateInstance(_type);
                object[] parameters = new object[0]; // no need for arguments
                mi.Invoke(_obj, parameters);
            }
        }
    }

}

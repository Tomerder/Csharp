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
            // Student??
        }
        public void showAllFields()
        {
            Console.WriteLine("All class Fields:");
            // Student??
        }
        public void activateTestMethod()
        {
            MethodInfo mi = _type.GetMethod("test");
            // student??
        }
    }
}

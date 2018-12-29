using System;
using System.Reflection;

namespace Reflection
{
    class ReflectionInfoDisplayer
    {
        private Type _theType;

        public ReflectionInfoDisplayer(string typeName)
            : this(Type.GetType(typeName))
        {
        }

        public ReflectionInfoDisplayer(object obj)
            : this(obj.GetType())
        {
        }

        public ReflectionInfoDisplayer(Type type)
        {
            _theType = type;
        }

        public void DisplayGeneralInfo()
        {
            Console.WriteLine("Type name: " + _theType.FullName);
            Console.WriteLine("Is class? " + _theType.IsClass);
            Console.WriteLine("Is enum? " + _theType.IsEnum);
            // More info can be added, of course.
        }

        public void DisplayMethods()
        {
            // MethodInfo is a base class for all kinds of methods -
            //  regular methods, properties, indexers, operators,
            //  constructors, etc.
            foreach (MethodInfo methodInfo in _theType.GetMethods())
            {
                Console.Write("{0} {1}( ", methodInfo.ReturnType.Name, methodInfo.Name);
                // ParameterInfo describes the parameters to the method.
                foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
                {
                    Console.Write(parameterInfo.ParameterType.Name + " " + parameterInfo.Name + ", ");
                }
                Console.WriteLine(")"); Console.WriteLine();
            }
        }

        public object InvokeMethod(object @this, string methodName, params object[] parameters)
        {
            MethodInfo theMethod = _theType.GetMethod(methodName);
            return theMethod.Invoke(@this, parameters);
        }
    }
}

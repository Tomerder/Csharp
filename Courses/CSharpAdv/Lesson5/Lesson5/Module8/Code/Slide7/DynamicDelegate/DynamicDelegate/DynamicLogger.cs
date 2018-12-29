using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DynamicDelegate
{
    class DynamicLogger
    {
        private static Dictionary<Type, LogMethodDelegate> _loggers = new Dictionary<Type, LogMethodDelegate>();

        public static void Log(TextWriter writer, object @object)
        {
            if (@object == null)
            {
                writer.WriteLine("(null)");
                return;
            }

            LogMethodDelegate logGenerator = GetLoggerForObject(@object);
            if (logGenerator == null)
            {
                writer.WriteLine(@object.ToString());
            }
            else
            {
                writer.WriteLine(logGenerator());
            }
        }

        /// <summary>
        /// Retrieves the logging delegate for the specified object by creating it at runtime.
        /// </summary>
        private static LogMethodDelegate GetLoggerForObject(object @object)
        {
            Type type = @object.GetType();
            LogMethodDelegate logGenerator;
            lock (_loggers)
            {
                //If we already have a logger delegate for this type, return the cached value.
                if (_loggers.TryGetValue(type, out logGenerator))
                {
                    return logGenerator;
                }

                //Obtain the MethodInfo for the log method
                MethodInfo logMethod = (from method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                        where method.IsDefined(typeof(LogMethodAttribute), false)
                                        select method).SingleOrDefault();
                if (logMethod == null)
                {
                    logGenerator = null;
                }
                else
                {
                    //Use the MethodInfo to create a delegate at runtime, which is now bound to the
                    //logGenerator delegate and can be used to invoke the method in a strongly-typed fashion.
                    logGenerator = (LogMethodDelegate)Delegate.CreateDelegate(typeof(LogMethodDelegate), @object, logMethod);
                }

                //Cache the result for future invocations.
                _loggers.Add(type, logGenerator);
                return logGenerator;
            }
        }
    }
}

using System;
using System.Reflection;
using System.IO;

namespace ReflectionExercise
{
    static public class Logger
    {
        private static StreamWriter _file;

        public static void Initialize(string fileName)
        {
            _file = new StreamWriter(fileName);
        }

        public static void Uninitialize()
        {
            _file.Close();
        }

        public static void LogObject(string message, object theObject)
        {
            Type objectType = theObject.GetType();

            _file.WriteLine("{0} {1} object: {2}",
                            DateTime.Now,
                            objectType.Name,
                            message);

            FieldInfo[] fields =
                objectType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                // Determine whether this field should be logged.
                if (!FieldShouldBeLogged(field))
                    continue;

                _file.WriteLine("Field: {0} {1} = {2}",
                                field.FieldType.Name, field.Name,
                                field.GetValue(theObject));
            }
            _file.WriteLine("----------------------");
        }

        private static bool FieldShouldBeLogged(FieldInfo field)
        {
            object[] attributes = field.GetCustomAttributes(false);
            foreach (object attr in attributes)
            {
                if ((attr as LogFieldAttribute) != null)
                    return true;
            }
            return false;


            //LogFieldAttribute[] attributes =
            //    (LogFieldAttribute[])field.GetCustomAttributes(typeof(LogFieldAttribute), true);

            //return attributes.Length != 0;
        }
    }
}

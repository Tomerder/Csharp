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
            //TODO 1: Open the file.
        }

        public static void Uninitialize()
        {
            //TODO 1: Close the file.
        }

        public static void LogObject(string message, object theObject)
        {
            //TODO 1: Initialize objectType with the actual type of theObject.
            Type objectType = null;

            _file.WriteLine("{0} {1} object: {2}",
                            DateTime.Now,
                            objectType.Name,
                            message);

            //TODO 1: Initialize fields with the actual fields of this type.
            FieldInfo[] fields = null;

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
            //TODO 2: Determine if the field should be logged to the disk.  (You should check for the [LogField] attribute.)

            throw new NotImplementedException();
        }
    }
}

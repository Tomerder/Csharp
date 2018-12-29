using System;

namespace ReflectionExercise
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class LogFieldAttribute : Attribute
    {
    }
}

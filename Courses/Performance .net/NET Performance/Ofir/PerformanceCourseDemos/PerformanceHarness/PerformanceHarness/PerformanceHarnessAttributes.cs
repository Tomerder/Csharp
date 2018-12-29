using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceHarness
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SetupMethodAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TeardownMethodAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MeasurableMethodAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MeasurableClassAttribute : Attribute
    {
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PerformanceHarness
{
    public static class Util
    {
        public static bool IsAttributeDefined<AttributeType>(ICustomAttributeProvider provider)
            where AttributeType : Attribute
        {
            return provider.GetCustomAttributes(typeof (AttributeType), false).Length != 0;
        }

        public static AttributeType GetCustomAttribute<AttributeType>(ICustomAttributeProvider provider)
            where AttributeType : Attribute
        {
            return GetCustomAttributes<AttributeType>(provider)[0];
        }

        public static AttributeType[] GetCustomAttributes<AttributeType>(ICustomAttributeProvider provider)
            where AttributeType : Attribute
        {
            return provider.GetCustomAttributes(typeof (AttributeType), false) as AttributeType[];
        }
    }
}

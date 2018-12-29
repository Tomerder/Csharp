using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit;

namespace Todo.App
{
    public class ViewResolver : IViewResolver
    {
        public Type GetViewType(Type viewModelType)
        {
            var name = viewModelType.FullName;

            if (name.EndsWith("Vm"))
            {
                var prefix = name.Substring(0, name.Length - 2);
                var fullName = $"{prefix}View";
                return Type.GetType(fullName);
            }

            return null;
        }
    }
}

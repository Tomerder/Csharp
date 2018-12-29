using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorInterfaces
{
    public sealed class CalculatorPluginAttribute : Attribute { }

    public interface ICalculatorPlugin
    {
        float Calculate(float op1, float op2);
    }
}

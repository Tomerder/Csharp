using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorInterfaces;

namespace BasicMath
{
    [CalculatorPlugin]
    public class Subtract : ICalculatorPlugin
    {
        public float Calculate(float op1, float op2)
        {
            return op1 - op2;
        }
    }
}

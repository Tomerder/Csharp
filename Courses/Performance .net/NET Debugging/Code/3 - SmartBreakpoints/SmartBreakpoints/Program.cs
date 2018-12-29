using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBreakpoints
{
    class Program
    {
        static Random _rand = new Random();

        #region Here Be Dragons

        static void BuggyFunction()
        {
            if (_rand.Next() % 97 == 0)
            {
                _rand = null;
            }
        }

        #endregion

        static void NiceFunction()
        {
            int n = _rand.Next();
            //Conditional breakpoint on _rand == null
            Console.WriteLine(_rand.Next());
            BuggyFunction();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                NiceFunction();
            }
        }
    }
}

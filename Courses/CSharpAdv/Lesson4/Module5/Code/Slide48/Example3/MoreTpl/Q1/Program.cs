using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(F);
            Action a = new Action(F);
            Task.Factory.StartNew(a);
            Task.Factory.StartNew(new Action(F));
        }

        static void F() { }
    }
}

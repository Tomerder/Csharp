using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNet_performance_Course
{
    static class ClassEx
    {
        //--------------------------------------------
        static public void WeekRefExample()
        {
            object s = new object();    //will be cleaned
            string s1 = string.Format("Tomer");  //will be cleaned
            string s2 = "Tomer";  //wont be cleaned - string has an optimization which saves literals on Intern Table (cache for strings which is implemented on SW - CLR)

            WeakReference wr = new WeakReference(s1);
            s1 = null; //no more strong ref for s

            GC.Collect(); //clean wr

            string strongS = wr.Target as string;
            if (strongS != null)
            {
                //use strongS safely
                Console.WriteLine("weak ref was not cleaned", strongS);
            }
            else
            {
                Console.WriteLine("weak ref was cleaned !!!");
            }
        }

        //--------------------------------------------
    }
}

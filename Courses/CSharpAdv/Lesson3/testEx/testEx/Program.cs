using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace testEx
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ex1();
            //Ex2();
            //Ex3();
            //Ex4();
            //Ex5();
            Ex6();

        }

        //--------------------------------------------------------------------------------
        //Thread (start)
        static void Ex6()
        {
            string s = "abcdefgh";

            Thread[] threadArr = new Thread[s.Length];
            int i = 0;

            foreach (char c in s.ToCharArray())
            {
                //thread pool
                ParameterizedThreadStart parThread = new ParameterizedThreadStart(PrintChar);
                threadArr[i] = new Thread(parThread);
                threadArr[i].Start(c);
                i++;
            }

            foreach(Thread thread in threadArr)
            {
                thread.Join();
            }

            Console.WriteLine("After threads end");

            //Console.Read();  //not neccessary since thread is foreground by default 
        }

        //--------------------------------------------------------------------------------
        //Thread (start)
        static void Ex5()
        {
            string s = "abcdefgh";

            //Thread[] threadArr = new Thread[s.Length];

            foreach (char c in s.ToCharArray())
            {
                //thread pool
                ParameterizedThreadStart parThread = new ParameterizedThreadStart(PrintChar);
                Thread thread = new Thread(parThread);
                thread.Start(c);
            }           

            //Console.Read();  //not neccessary since thread is foreground by default 
        }

        //--------------------------------------------------------------------------------
        //Thread Pool
        static void Ex4()
        {
            string s = "abcdefgh";

            foreach (char c in s.ToCharArray())
            {
                //thread pool
                WaitCallback callBack = new WaitCallback(PrintChar);
                ThreadPool.QueueUserWorkItem(callBack, c);
            }

            Console.Read();
        }

        private static void PrintChar(object c)
        {
            Console.WriteLine((char)c);
        }

        //--------------------------------------------------------------------------------
        //APM (use Action)

        //delegate void MyDel(char c);

        static void Ex3()
        {
            string s = "abcdefgh";

            foreach (char c in s.ToCharArray())
            {
                //MyDel del = PrintChar;
                Action<char> del = PrintChar;
                del.BeginInvoke(c, null, null);
            }

            Console.Read();
        }


        //--------------------------------------------------------------------------------
        //APM

        //delegate void MyDel(char c);

        static void Ex2()
        { 
            string s = "abcdefgh";

            //MyDel del = PrintChar;
            Action<char> del = PrintChar;

            foreach (char c in s.ToCharArray())
            {
                //PrintChar(c);
                del.BeginInvoke(c, null, null);
            }

            Console.Read();
        }

        private static void PrintChar(char c)
        {
            Console.WriteLine(c);
        }

        //--------------------------------------------------------------------------------
        //FUNC 

        static void Ex1()
        {
            Func<string, int> F = Func1;
            Console.WriteLine(F("abc"));
            Console.ReadLine();
        }

        static int Func1(string str)
        {
            return 5;
        }

        //--------------------------------------------------------------------------------

    }
}

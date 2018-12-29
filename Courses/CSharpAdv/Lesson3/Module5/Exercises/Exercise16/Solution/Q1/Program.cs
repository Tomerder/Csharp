﻿using System;

namespace ConsoleApplication1
{
    delegate void MyDel(char c);

    class Program
    {
        static void Main(string[] args)
        {
            string s = "abcdefgh";

            MyDel myDel = new MyDel(PrintChar);

            foreach (char c in s.ToCharArray())
            {
                myDel.BeginInvoke(c, null, null);
            }

            Console.Read();
        }

        private static void PrintChar(char c)
        {
            Console.WriteLine(c);
        }
    }
}

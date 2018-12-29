using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using System.Diagnostics;
using System.Runtime;

namespace ServerGCVsWorkstationGC
{
    class Program
    {
        static void AllocateALot(int iterations)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);

            for (int i = 0; i < iterations; ++i)
            {
                XmlElement child = doc.CreateElement("child" + i);
                root.AppendChild(child);
                string s;
                if (i % 47 == 0) s = root.OuterXml;
            }
        }

        static void Main(string[] args)
        {
            Console.ReadLine();
            Console.WriteLine(GCSettings.IsServerGC);

            const int ITERATIONS = 40000;

            ThreadPool.QueueUserWorkItem(dummy => AllocateALot(ITERATIONS));
            ThreadPool.QueueUserWorkItem(dummy => AllocateALot(ITERATIONS));
            ThreadPool.QueueUserWorkItem(dummy => AllocateALot(ITERATIONS));

            Stopwatch st = Stopwatch.StartNew();
            AllocateALot(ITERATIONS);
            Console.WriteLine("Time elapsed: " + st.ElapsedMilliseconds + " ms");
        }
    }
}

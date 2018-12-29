using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using Microsoft.CSharp;

namespace OOM4
{
    class OOM4Program
    {
        [MethodImpl(MethodImplOptions.NoOptimization)]
        static void Main(string[] args)
        {
            List<byte[]> datums = new List<byte[]>();
            for (long i = 0; ; ++i)
            {
                string source = "";
                for (int j = 0; j <= i; ++j)
                {
                    source += "public class C" + i.ToString() + "_" + j.ToString() + " {";
                    for (int k = 0; k <= j; ++k)
                    {
                        source += "public string P" + k.ToString() + " {get;set;}";
                    }
                    source += "}";
                    source += Environment.NewLine;
                }

                CSharpCodeProvider prov = new CSharpCodeProvider();
                CompilerParameters options = new CompilerParameters();
                CompilerResults results = prov.CompileAssemblyFromSource(options, source);

                for (int j = 0; j <= i; ++j)
                {
                    Type generatedType =
                        results.CompiledAssembly.GetType("C" + i.ToString() + "_" + j.ToString());
                    XmlSerializer ser = new XmlSerializer(generatedType);
                    ser.Serialize(
                        Console.Out,
                        Activator.CreateInstance(generatedType));
                }
                
                byte[] data = new byte[80 * 1048576];
                datums.Add(data);
                DoSomething(i, data);
                Console.WriteLine(data.Length);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void DoSomething(long i, byte[] data)
        {
            for (int j = 0; j <= i; ++j)
            {
                data[j] = 3;
            }
        }
    }
}

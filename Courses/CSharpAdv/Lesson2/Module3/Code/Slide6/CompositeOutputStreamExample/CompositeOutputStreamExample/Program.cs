using System;
using System.Text;

namespace CompositeOutputStreamExample
{
    /// <summary>
    /// Demonstrates the use of the CompositeOutputStream 
    /// by directing output to the standard output stream and the standard error stream
    /// simultaneously.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CompositeOutputStream output = new CompositeOutputStream(Console.OpenStandardOutput(), Console.OpenStandardError());

            byte[] buf = Encoding.ASCII.GetBytes("Hello World" + Environment.NewLine);
            output.Write(buf, 0, buf.Length);

            output.Close();
        }
    }
}

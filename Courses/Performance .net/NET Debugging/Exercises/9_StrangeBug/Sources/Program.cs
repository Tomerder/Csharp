using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FileAccessEmulation
{
    class MyHandle
    {
        private bool _isClosed;

        public void Use()
        {
            if (_isClosed)
            {
                throw new ObjectDisposedException(
                    "handle",
                    "Attempted to use handle after it has already been closed.");
            }
        }

        public void Close()
        {
            _isClosed = true;
        }
    }

    static class Utilities
    {
        public static byte[] Read(MyHandle handle, int count)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Thread.Sleep(2000);
            handle.Use();
            return new byte[count];
        }
    }

    class MyFile
    {
        private MyHandle _handle;

        public MyFile(string filename)
        {
            _handle = new MyHandle();
        }

        public byte[] Read(int count)
        {
            return Utilities.Read(_handle, count);
        }

        ~MyFile()
        {
            _handle.Close();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyFile file = new MyFile("MyFile");
            byte[] data = file.Read(100);
            Console.WriteLine(data.Length);
        }
    }
}

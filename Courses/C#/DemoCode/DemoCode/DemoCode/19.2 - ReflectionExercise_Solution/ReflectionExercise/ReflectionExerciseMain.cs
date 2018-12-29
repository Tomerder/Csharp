using System;
using System.IO;

namespace ReflectionExercise
{
    public class MyClass
    {
        private FileStream _stream;
        [LogField]
        private string _fileName;

        public MyClass(string fileName)
        {
            _fileName = fileName;
        }

        public void Work()
        {
            _stream = new FileStream(_fileName, FileMode.Open);
            // Do some work with the file.
        }
    }

    class ReflectionExerciseMain
    {
        static void Main(string[] args)
        {
            Logger.Initialize(@"C:\Log.txt");

            // Let's do some work, and in the case of an exception,
            //  log the object that we're interested in.
            //
            MyClass myClass = new MyClass("fdfdf");
            try
            {
                myClass.Work();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred: {0}", ex.Message);
                Console.WriteLine("FileStream details are in the log file");

                Logger.LogObject(ex.Message, myClass);
            }

            Logger.Uninitialize();
        }
    }
}

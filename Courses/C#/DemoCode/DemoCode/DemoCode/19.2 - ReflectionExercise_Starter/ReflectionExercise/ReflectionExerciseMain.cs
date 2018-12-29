using System;
using System.IO;

namespace ReflectionExercise
{
    public class MyClass
    {
        private FileStream _stream;
        //TODO 2: Specify that the _fileName field should be logged.  (Use the [LogField] attribute.)
        private string _fileName;

        public MyClass(string fileName)
        {
            _fileName = fileName;
        }

        public void Work()
        {
            _stream = new FileStream(_fileName, FileMode.Open);
            // Working code omitted for brevity.
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
                Console.WriteLine("Details are in the log file");

                //TODO 1: Use the Logger.LogObject method with the exception message and the myClass instance.
            }

            Logger.Uninitialize();
        }
    }
}

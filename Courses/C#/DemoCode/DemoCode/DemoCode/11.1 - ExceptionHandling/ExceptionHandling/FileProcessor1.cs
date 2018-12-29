using System;
using System.IO;

namespace ExceptionHandling
{
    class FileProcessor1
    {
        private string _fileName;

        public FileProcessor1(string fileName)
        {
            _fileName = fileName;
        }

        // The following method completely ignores the possible condition
        //  that the file specified doesn't exist.  If that is indeed the
        //  case, an exception will be thrown during runtime.  The source
        //  of the exception is the File.Open static method, and its type
        //  is System.IO.FileNotFoundException.
        //
        public void Process()
        {
            FileStream stream = File.Open(_fileName, FileMode.Open);
            Console.WriteLine("Processing file {0}", stream.Name.ToUpper());
            // Actual processing code omitted for brevity.
            Console.WriteLine("The file {0} was successfully processed", stream.Name);
        }
    }
}

using System;

namespace NameRetrieval
{
    // This is our user-defined exception class.  Note that it derives
    //  from System.Exception (Microsoft no longer recommends deriving
    //  user-defined exception classes from System.ApplicationException).
    //
    class BadFileFormatException : Exception
    {
        private string _fileName;

        // Our exception defines an additional property called 'FileName'
        //  that holds the name of the problematic file.  It can be
        //  retrieved by the user of our exception when it's caught.
        //
        public string FileName
        {
            get { return _fileName; }
        }

        // We provide three constructors that are similar in essence
        //  to the System.Exception constructors.
        //
        public BadFileFormatException() : base()
        {
        }

        public BadFileFormatException(string fileName, string problem)
            : this(fileName, problem, null)
        {
        }

        public BadFileFormatException(string fileName, string problem,
                                      Exception innerException)
            : base("The file " + fileName + " has a format error: " + problem,
                   innerException)
        {
            _fileName = fileName;
        }
    }
}

using System;
using System.IO;

namespace PropertiesExercise
{
    public class FileStatistics
    {
        private int _wordCount;

        public int WordCount
        {
            get { return _wordCount; }
            protected set { _wordCount = value; }
        }
        private int _lineCount;

        public int LineCount
        {
            get { return _lineCount; }
            protected set { _lineCount = value; }
        }
        private int _charCount;

        public int CharCount
        {
            get { return _charCount; }
            protected set { _charCount = value; }
        }
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            protected set { _fileName = value; }
        }

        //TODO: Provide property access to the four private fields above.
        //TODO: Make them writable to derived classes without exposing them to the user.

        public FileStatistics(string fileName)
        {
            _fileName = fileName;
        }

        public void Go()
        {
            using (StreamReader reader = new StreamReader(_fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    ++_lineCount;
                    if (line.Length > 0)
                    {
                        _wordCount += line.Split(_separators,
                                                 StringSplitOptions.RemoveEmptyEntries)
                                                .Length;
                        _charCount += line.Length;
                    }
                }
            }
        }

        private static readonly char[] _separators = new char[] { ' ', '\t' };
    }
}

using System;
using System.IO;

namespace PropertiesExercise
{
    public class FileStatistics
    {
        private int _wordCount;
        private int _lineCount;
        private int _charCount;
        private string _fileName;

        #region Solution

        public int WordCount
        {
            get { return _wordCount; }
            protected set { _wordCount = value; }
        }

        public int LineCount
        {
            get { return _lineCount; }
            protected set { _lineCount = value; }
        }

        public int CharCount
        {
            get { return _charCount; }
            protected set { _charCount = value; }
        }

        public string FileName
        {
            get { return _fileName; }
            protected set { _fileName = value; }
        }

        #endregion

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

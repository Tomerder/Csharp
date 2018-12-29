using System;
using System.IO;

namespace NameRetrieval
{
    // This is the revisited version of the NameSorter class, that throws
    //  an appropriate user-defined exception when an error condition
    //  is encountered.
    //
    public class NameSorter
    {
        private string _fileName;
        private string[] _names;

        public NameSorter(string fileName)
        {
            _fileName = fileName;
        }

        public void Read()
        {
            using (StreamReader reader = new StreamReader(_fileName))
            {
                // First, read the number of lines in the file
                //  and allocate an array big enough to contain them.
                //
                int numberOfLines;
                try
                {
                    numberOfLines = int.Parse(reader.ReadLine());
                    _names = new string[numberOfLines];
                }
                catch (FormatException parseError)
                {
                    throw new BadFileFormatException(_fileName,
                        "First line should contain the number of lines",
                        parseError);
                }

                // Now, read each line into an entry in the array.
                //
                try
                {
                    for (int i = 0; i < numberOfLines; ++i)
                    {
                        string line = reader.ReadLine();
                        if (string.IsNullOrEmpty(line))
                        {
                            throw new ArgumentException("Empty line read");
                        }
                        _names[i] = line;
                    }
                }
                catch (Exception readException)
                {
                    throw new BadFileFormatException(_fileName,
                                                     "Error reading line from file",
                                                     readException);
                }
            }
        }

        // Sorts the internal array of names.
        //
        public void Sort()
        {
            Array.Sort(_names);
        }

        // Writes the names to the file in the specified format.
        //
        public void WriteTo(string outFileName)
        {
            using (StreamWriter writer = new StreamWriter(outFileName))
            {
                // First, write the number of items.
                //
                writer.WriteLine(_names.Length);

                // Then, write every name on a separate line.
                //
                foreach (string name in _names)
                {
                    writer.WriteLine(name);
                }
            }
        }
    }
}

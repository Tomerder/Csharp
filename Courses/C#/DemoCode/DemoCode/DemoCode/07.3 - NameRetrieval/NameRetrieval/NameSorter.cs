using System;
// Note that we need a using statement for the System.IO
//  namespace, because that's where StreamReader and StreamWriter
//  come from.
using System.IO;

namespace NameRetrieval
{
    public class NameSorter
    {
        private string _fileName;
        private string[] _names;

        public NameSorter(string fileName)
        {
            _fileName = fileName;
        }

        // Reads names from the file in the specified format.
        //  Assumes the input is perfect (performs no validation).
        //
        public void Read()
        {
            //instead of using we could use : try->finally (and call dispose in finaly}  
            //try{
            using (StreamReader reader = new StreamReader(_fileName))
            {
                // First, read the number of lines in the file
                //  and allocate an array big enough to contain them.
                //
                int numberOfLines = int.Parse(reader.ReadLine());
                _names = new string[numberOfLines];

                // Now, read each line into an entry in the array.
                //
                for (int i = 0; i < numberOfLines; ++i)
                {
                    _names[i] = reader.ReadLine();
                }
            }
        }
        //finaly { reader.Dispose(); }

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
                // can use var instead 
                foreach (string /*var*/ name in _names)
                {
                    writer.WriteLine(name);
                }
            }
        }
    }
}

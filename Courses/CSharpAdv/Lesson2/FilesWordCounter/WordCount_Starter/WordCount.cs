using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordCount_Starter
{
    /// <summary>
    /// Parses a command line string looking for positional arguments
    /// at the beginning of the command line string, followed by named
    /// argument which are formatted as follows:
    ///     -argname:argvalue
    /// </summary>
    class CommandLineParser
    {
        private readonly string[] _args;
        private readonly int _positional;
        private Dictionary<string, string> _named = new Dictionary<string, string>();

        public CommandLineParser(string[] args)
        {
            _args = args;
            foreach (string arg in args)
            {
                if (!arg.StartsWith("-"))
                    _positional++;
                else
                {
                    int colon = arg.IndexOf(':');
                    _named.Add(arg.Substring(1, colon - 1), arg.Substring(colon + 1, arg.Length - colon - 1));
                }
            }
        }

        /// <summary>
        /// Count of positional arguments.
        /// </summary>
        public int PositionalArguments
        {
            get
            {
                return _positional;
            }
        }

        public string GetPositionalArgument(int index)
        {
            return _args[index];
        }

        /// <summary>
        /// Retrieves a named argument by its key name.
        /// </summary>
        /// <param name="key">The key name.</param>
        /// <returns>The value of the named argument or an empty string
        /// if there is no such named argument.</returns>
        public string GetNamedArgument(string key)
        {
            string val = String.Empty;
            _named.TryGetValue(key, out val);
            return val;
        }
    }

    class WordCount
    {
        private string[] _pathesToRead;
        private string _textOutput;
        private string _binOutput;

        static void Main(string[] args)
        {
            WordCount wc = new WordCount(args);
            wc.Run();
        }

        /// <summary>
        /// Prepares the word count program for execution.
        /// </summary>
        /// <param name="args">Command line arguments, in the following format:
        /// WordCount.exe file1 [file2 [file3...]] [-t:summary.txt] [-b:summary.bin]
        /// The default output text stream is Console.Out, and there is no default
        /// output binary stream.</param>
        private WordCount(string[] args)
        {
            CommandLineParser parser = new CommandLineParser(args);

            //TODO: Initialize a text output stream and a binary output stream
            //according to the command line parameters parsed.

            _pathesToRead = new string[parser.PositionalArguments];

            for (int i = 0; i < parser.PositionalArguments; i++)
            {
                string arg = parser.GetPositionalArgument(i);
                _pathesToRead[i] = arg;
            }

            _textOutput = parser.GetNamedArgument("t");
            _binOutput = parser.GetNamedArgument("b");
        }

        private void Run()
        {
            //TODO: For each file, calculate the number of words that appear in the file
            //and write a line to the text output stream in the following format:
            //  <filename> <word_count>
            //Next, write the word count (without the file name and without any separators)
            //to the binary output stream.

            //TODO: Don't forget to close the output streams

            //go over each input file and wite word count to text and bin files
            foreach(string inputFile in _pathesToRead)
            {
                string[] lines = File.ReadAllLines(inputFile);
                int wordsCount = 0;
                foreach (string line in lines)
                {
                    string[] words = line.Split(' ');
                    wordsCount += words.Length;
                }

                //write to text file
                FileStream stream = new FileStream(_textOutput, FileMode.Append);
                WriteOutput(inputFile, wordsCount, stream);

                //write to bin file
                FileStream stream2 = new FileStream(_binOutput, FileMode.Append);
                WriteOutput(inputFile, wordsCount, stream2);
            }
        }

        private void WriteOutput(string inputFile, int wordsCount, Stream _outputStream)
        {
            using (StreamWriter writer = new StreamWriter(_outputStream))
            {
                String str = inputFile + " : " + wordsCount;
                writer.Write(str + Environment.NewLine);
            }
        }
    }
}

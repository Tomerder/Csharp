using System;

namespace PropertiesExercise
{
    class PropertiesExerciseMain
    {
        static void Main(string[] args)
        {
            #region Solution

            string fileName;
            if (args.Length == 0)
            {
                Console.Write("Please specify a filename: ");
                fileName = Console.ReadLine();
            }
            else
            {
                fileName = args[0];
            }

            FileStatistics statistics = new FileStatistics(fileName);
            statistics.Go();

            Console.WriteLine("Statistics for file {0}:", statistics.FileName);
            Console.WriteLine("Characters: {0}", statistics.CharCount);
            Console.WriteLine("Words: {0}", statistics.WordCount);
            Console.WriteLine("Lines: {0}", statistics.LineCount);

            #endregion
        }
    }
}

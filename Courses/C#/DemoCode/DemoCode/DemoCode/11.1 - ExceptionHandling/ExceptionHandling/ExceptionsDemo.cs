using System;
using System.IO;

namespace ExceptionHandling
{
    class ExceptionsDemo
    {
        static void Main(string[] args)
        {
            //NoExceptionHandling();
            //LittleExceptionHandling();
            ExtendedExceptionHandling();
        }

        private static string GetFileNameFromUser()
        {
            Console.Write("Please enter the file name: ");
            return Console.ReadLine();
        }

        private static void NoExceptionHandling()
        {
            // There's nothing here to handle the possible exception,
            //  and therefore if the file does not exist, the application
            //  will crash, displaying the exception details.
            //
            FileProcessor1 processor = new FileProcessor1(GetFileNameFromUser());
            processor.Process();
        }

        private static void LittleExceptionHandling()
        {
            while (true)
            {
                try
                {
                    FileProcessor1 processor = new FileProcessor1(GetFileNameFromUser());
                    processor.Process();
                    // Don't forget this line so that the loop doesn't
                    //  become infinite even when nothing exceptional
                    //  happens.
                    break;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File could not be found.  Try again.");
                    // Execution will now continue at the end of the
                    //  while block, effectively restarting the operation.
                }
            }
        }

        private static void ExtendedExceptionHandling()
        {
            try
            {
                FileProcessor1 processor = new FileProcessor1(GetFileNameFromUser());
                processor.Process();
            }
            catch (FileNotFoundException exception)
            {
                // This time, we have in our hands a FileNotFoundException
                //  object that we can investigate to see what went wrong:
                //
                Console.WriteLine("An exception was raised.  Details:");
                Console.WriteLine("Exception message: " + exception.Message);
                Console.WriteLine("--- FOR THE PROGRAMMER ONLY ---");
                Console.WriteLine("Exception source: " + exception.Source);
                Console.WriteLine("Exception stack trace:\n" + exception.StackTrace);
            }
            catch (Exception otherException)
            {
                // We can define more than one 'catch' block, and indeed
                //  can simulate a condition where an exception other than
                //  FileNotFoundException will be thrown.
                //
                Console.WriteLine("Something unexpected happened.  Details:");
                Console.WriteLine(otherException.Message);
            }
        }
    }
}

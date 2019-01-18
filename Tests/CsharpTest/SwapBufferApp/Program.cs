using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SwapBufferApp
{
    class Program
    {
        //----------------------------------------------------------------------

        const string FILE_NAME = @"C:\Matrix2.txt";
        const string FILE_NAME_OUTPUT = @"C:\Matrix2_OUTPUT.txt";
        const int CHUNK_SIZE_TO_WRITE = 6000;
        const int BUFFER_SIZE = 10000;
        const int READ_EVERY_MSEC = 3;
        const int WRITE_EVERY_MSEC = 1;
        const int READ_MAX_FAILURES = 50;

        //----------------------------------------------------------------------

        static int readFailedCounter = 0;

        //----------------------------------------------------------------------
        static void Main(string[] args)
        {
            //Swap buffer - youtube - client1 send file to server - server sends the file to client2 while receiving  

            SwapBuffer swapBuffer = new SwapBuffer(BUFFER_SIZE);

            //start read thread
            Action<SwapBuffer, int> readAct = ReadThread;
            readAct.BeginInvoke(swapBuffer, READ_EVERY_MSEC, null, null);

            //start write thread
            Action<SwapBuffer, int> writeAct = WriteThread;
            writeAct.BeginInvoke(swapBuffer, WRITE_EVERY_MSEC, null, null);
       
            //prevent program to finish
            Console.ReadLine();

            //dispose swapBuffer resources
            swapBuffer.Dispose();
        }

        //----------------------------------------------------------------------
        private static void ReadThread(SwapBuffer _swapBuffer, int _readEveryMsec)
        {
            byte[] buffer;
            int bytesRead;
            
            File.Delete(FILE_NAME_OUTPUT);

            while (true)
            {
                //read from swapBuffer
                bool isReadSuccess = _swapBuffer.Read(out buffer, out bytesRead);
                if (isReadSuccess && bytesRead > 0)
                {
                    readFailedCounter = 0;
                    //Do something with read data (e.g : write to file and send to client2)
                    string str = Encoding.Default.GetString(buffer);
                    //Console.WriteLine(str);
                    File.AppendAllText(FILE_NAME_OUTPUT, str);
                }
                else
                {
                    readFailedCounter++;
                }

                //wait before next read
                Thread.Sleep(_readEveryMsec);

                //in case last READ_MAX_FAILURES failed => read last time and finish
                if (readFailedCounter == READ_MAX_FAILURES)
                {
                    _swapBuffer.ForceSwapBufferAndRead(out buffer, out bytesRead);
                    string str = Encoding.Default.GetString(buffer);
                    str = str.Substring(0, bytesRead);
                    File.AppendAllText(FILE_NAME_OUTPUT, str);
                    break;
                }
            }
        }

        //----------------------------------------------------------------------
        private static void WriteThread(SwapBuffer _swapBuffer, int _writeEveryMsec)
        {
            byte[] fileData = File.ReadAllBytes(FILE_NAME);
            int fileIndexToWrite = 0;

            while (true)
            {
                //Chunk size to write
                int chunkSizeToWrite = CHUNK_SIZE_TO_WRITE;
                if(fileData.Length < fileIndexToWrite + chunkSizeToWrite)
                {
                    chunkSizeToWrite = fileData.Length - fileIndexToWrite;
                }

                //prepare buffer to write (e.g : receive from soket) 
                byte[] bufferToWrite = new byte[chunkSizeToWrite];
                Array.Copy(fileData, fileIndexToWrite, bufferToWrite, 0, chunkSizeToWrite);
                fileIndexToWrite += chunkSizeToWrite;

                //write to swapBuffer
                _swapBuffer.Write(bufferToWrite, chunkSizeToWrite);

                //wait before next write
                Thread.Sleep(_writeEveryMsec);

                //stop upon end of file
                if(fileData.Length == fileIndexToWrite)
                {
                    break;
                }
            }
        }

        //----------------------------------------------------------------------
    }
}

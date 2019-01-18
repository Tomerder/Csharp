#define INNER_MEASURE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MatrixSumBenchmark
{
    class Program
    {
        const string FILE_NAME = @"C:\Matrix.txt";
        const int ROWS = 5000;
        const int COLS = 5000;

        //---------------------------------------------------------------------
        static void Main(string[] args)
        {
            //Create file once
            if (!File.Exists(FILE_NAME))
            {
                CreateFile();
            }

            //messure - single thread : read from file to matrix -> calc matrix sum 
            Func<long> func1 = TestSingleThreadFillMatrixAndCalcSum;
            UtilsLib.Utils.Measure(func1);

            //messure - multi thread : read from file to matrix -> single thread : calc matrix sum 
            Func<long> func2 = TestMultiThreadFillMatrixSingleThreadCalcSum;
            UtilsLib.Utils.Measure(func2);

            //messure - single thread : read from file to matrix -> multi thread : calc matrix sum 
            Func<long> func3 = TestSingleThreadFillMatrixMultiThreadCalcSum;
            UtilsLib.Utils.Measure(func3);

            //messure - single thread : read from file to matrix -> multi thread : calc matrix sum 
            Func<long> func4 = TestMultiThreadFillMatrixAndCalcSum;
            UtilsLib.Utils.Measure(func4);

            Console.ReadLine();
        }

        //---------------------------------------------------------------------

        private static void CreateFile()
        {
            StringBuilder toWrite = new StringBuilder();

            for (int i=0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    toWrite.Append(j + ",");
                }
                toWrite.Append("\n");
            }

            File.WriteAllText(FILE_NAME, toWrite.ToString());
        }

        //---------------------------------------------------------------------

        static long TestSingleThreadFillMatrixAndCalcSum()
        {
            bool isInnerMeasure = false;
#if (INNER_MEASURE)
            Stopwatch sw = Stopwatch.StartNew();
            isInnerMeasure = true;
#endif

            MatrixSum matrixSum = new MatrixSum(FILE_NAME, ROWS, COLS, MatrixSum.FillMatrixPolicy.SINGLE_THREAD, isInnerMeasure);

#if (INNER_MEASURE)
            Console.WriteLine("Fill Matrix from file : " + sw.ElapsedMilliseconds);
            sw = Stopwatch.StartNew();
#endif

            long sum = matrixSum.CalcSum(0, ROWS);

#if (INNER_MEASURE)
            Console.WriteLine("Calc Sum : " + sw.ElapsedMilliseconds);
#endif

            return sum;
        }

        //---------------------------------------------------------------------

        static long TestMultiThreadFillMatrixSingleThreadCalcSum()
        {
            bool isInnerMeasure = false;
#if (INNER_MEASURE)
            Stopwatch sw = Stopwatch.StartNew();
            isInnerMeasure = true;
#endif

            MatrixSum matrixSum = new MatrixSum(FILE_NAME, ROWS, COLS, MatrixSum.FillMatrixPolicy.MULTI_THREAD, isInnerMeasure);

#if (INNER_MEASURE)
            Console.WriteLine("Fill Matrix from file : " + sw.ElapsedMilliseconds);
            sw = Stopwatch.StartNew();
#endif

            long sum = matrixSum.CalcSum(0, ROWS);

#if (INNER_MEASURE)
            Console.WriteLine("Calc Sum : " + sw.ElapsedMilliseconds);
#endif

            return sum;
        }

        //---------------------------------------------------------------------

        static long TestSingleThreadFillMatrixMultiThreadCalcSum()
        {
            bool isInnerMeasure = false;
#if (INNER_MEASURE)
            Stopwatch sw = Stopwatch.StartNew();
            isInnerMeasure = true;
#endif

            MatrixSum matrixSum = new MatrixSum(FILE_NAME, ROWS, COLS, MatrixSum.FillMatrixPolicy.SINGLE_THREAD, isInnerMeasure);

#if (INNER_MEASURE)
            Console.WriteLine("Fill Matrix from file : " + sw.ElapsedMilliseconds);
            sw = Stopwatch.StartNew();
#endif

            long sum = matrixSum.CalcSumMultiThread();

#if (INNER_MEASURE)
            Console.WriteLine("Calc Sum : " + sw.ElapsedMilliseconds);
#endif

            return sum;
        }

        //---------------------------------------------------------------------

        static long TestMultiThreadFillMatrixAndCalcSum()
        {
            bool isInnerMeasure = false;
#if (INNER_MEASURE)
            Stopwatch sw = Stopwatch.StartNew();
            isInnerMeasure = true;
#endif

            MatrixSum matrixSum = new MatrixSum(FILE_NAME, ROWS, COLS, MatrixSum.FillMatrixPolicy.MULTI_THREAD, isInnerMeasure);

#if (INNER_MEASURE)
            Console.WriteLine("Fill Matrix from file : " + sw.ElapsedMilliseconds);
            sw = Stopwatch.StartNew();
#endif

            long sum = matrixSum.CalcSumMultiThread();

#if (INNER_MEASURE)
            Console.WriteLine("Calc Sum : " + sw.ElapsedMilliseconds);
#endif

            return sum;
        }

        //---------------------------------------------------------------------
    }
}

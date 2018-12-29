﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelMatrix
{
    class Program
    {
        private const int Rows = 4000, Cols = 1000;
        private const int Iterations = 100;

        private static volatile int x;

        static void Measure(Func<int> act, string what)
        {
            x = act();//Warm up - for JIT translation + Virtual memory (paging) + cache  
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < Iterations; ++i)
            {
                x = act();
            }
            Console.WriteLine(what + " " + sw.ElapsedMilliseconds);
        }

        static void Main()
        {
            int[,] matrix = BuildMatrix();
            Measure(() => TraverseMatrixSequential(matrix), "Sequential");

            //matrix = BuildMatrix();
            Measure(() => TraverseMatrixParallel(matrix, Environment.ProcessorCount), "Parallel");

            //matrix = BuildMatrix();
            Measure(() => TraverseMatrixParallel(matrix, 1), "Parallel/OneCore");

        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int TraverseMatrixParallel(int[,] matrix, int P)
        {
            int sum = 0;
            int[] result = new int[P];
            Parallel.For(0, P,
                         p =>
                             {
                                 int localResult = 0;  //fix performance
                                 result[p] = 0;
                                 int chunkSize = Rows / P + 1;
                                 int myStart = p * chunkSize;
                                 int myEnd = Math.Min(myStart + chunkSize, Rows);
                                 for (int i = myStart; i < myEnd; ++i)
                                 {
                                     for (int j = 0; j < Cols; ++j)
                                     {
                                         //every procersor core will change this cache line and will have to get it from ram
                                         //cache invalidation!!! - because result[4] will be on same cache line
                                         //result[p] += matrix[i, j];   //cache invalidation!!!
                                         localResult += matrix[i, j];   //fix performance
                                     }
                                 }
                                 result[p] = localResult;  //fix performance
                             });
            for (int p = 0; p < P; ++p)
                sum += result[p];
            return sum;
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int TraverseMatrixSequential(int[,] matrix)
        {
            int sum = 0;
            //for (int j = 0; j < Cols; ++j)   //go over each col - XXX
            for (int j = 0; j < Rows; ++j) 
            {
                //for (int i = 0; i < Rows; ++i)   //go over each row - XXX
                for (int i = 0; i < Cols; ++i)
                {
                    //sum += matrix[i, j];    //XXX
                    sum += matrix[j, i];
                }
            }
            return sum;
        }

        private static int[,] BuildMatrix()
        {
            int[,] result = new int[Rows,Cols];
            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Cols; ++j)
                    result[i, j] = i + j;
            return result;
        }
    }
}

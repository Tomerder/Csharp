using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MatrixBlockingMultiplication
{
    class Program
    {
        static int N = 2048;

        static int[,] BuildMatrix()
        {
            int[,] m = new int[N, N];
            Random r = new Random(Environment.TickCount);
            for (int i = 0; i < N; ++i)
                for (int j = 0; j < N; ++j)
                    m[i, j] = r.Next();
            return m;
        }
        static int[,] MultiplyNaive(int[,] A, int[,] B)
        {
            int[,] C = new int[N, N];
            for (int i = 0; i < N; ++i)
                for (int j = 0; j < N; ++j)
                    for (int k = 0; k < N; ++k)
                        C[i, j] += A[i, k] * B[k, j];
            return C;
        }
        static int[,] MultiplyBlocked(int[,] A, int[,] B, int bs)
        {
            int[,] C = new int[N,N];
            for (int ii = 0; ii < N; ii += bs)
                for (int jj = 0; jj < N; jj += bs)
                    for (int kk = 0; kk < N; kk += bs)
                    {
                        for (int i = ii; i < ii + bs; ++i)
                        {
                            for (int j = jj; j < jj + bs; ++j)
                            {
                                for (int k = kk; k < kk + bs; ++k)
                                    C[i, j] += A[i, k] * B[k, j];
                            }
                        }
                    }
            return C;
        }

        static void Main(string[] args)
        {
            int[,] A;
            int[,] B;

            Stopwatch sw;
            int[,] C;

            A = BuildMatrix();
            B = BuildMatrix();
            sw = Stopwatch.StartNew();
            C = MultiplyNaive(A, B);
            Console.WriteLine("Naive: " + sw.ElapsedMilliseconds);

            for (int bs = 4; bs <= N; bs *= 2)
            {
                A = BuildMatrix();
                B = BuildMatrix();
                sw = Stopwatch.StartNew();
                C = MultiplyBlocked(A, B, bs);
                Console.WriteLine("Blocked (bs=" + bs + "): " + sw.ElapsedMilliseconds);
            }
        }
    }
}


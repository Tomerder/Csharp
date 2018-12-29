using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;

namespace Microsoft.MSDNEvent.ParallelismVS2010
{
    //Adapted from Code Gallery sample
    public class NQueensSolver
    {
        public static int Sequential(int n)
        {
            return new NQueensState(n).CountSolutions();
        }

        public static int Parallel(int n)
        {
            NQueensState[] statesAfterOneMove = new NQueensState[n];
            for (int row = 0; row < n; row++)
            {
                statesAfterOneMove[row] = new NQueensState(n);
                statesAfterOneMove[row].PlaceQueen(row);
            };

            return (from q in statesAfterOneMove.AsParallel()
                    select q.CountSolutions()).Sum();
        }

        class NQueensState
        {
            private bool[] m_rows;        // m_rows[i] = does row i contain a queen?
            private bool[] m_fwDiagonals; // m_fwDiagonals[i] = does forward diagonal i contain a queen?
            private bool[] m_bwDiagonals; // m_bwDiagonals[i] = does backward diagonal i contain a queen?
            int m_size;  // Size of the chessboard
            int m_col;   // Column with the smallest index that does not contain a queen, 0 <= m_col < m_size

            public NQueensState(int size)
            {
                m_size = size;
                m_rows = new bool[size];
                m_fwDiagonals = new bool[2 * size - 1];
                m_bwDiagonals = new bool[2 * size - 1];
                m_col = 0;
            }

            public int CountSolutions()
            {
                if (m_col == m_size) return 1;

                int answer = 0;
                for (int row = 0; row < m_size; row++)
                {
                    if (PlaceQueen(row))
                    {
                        answer += CountSolutions();
                        RemoveQueen(row);
                    }
                }
                return answer;
            }

            public bool PlaceQueen(int row)
            {
                if (!m_rows[row] && !m_fwDiagonals[row + m_col] && !m_bwDiagonals[row - m_col + m_size - 1])
                {
                    m_rows[row] = m_fwDiagonals[row + m_col] = m_bwDiagonals[row - m_col + m_size - 1] = true;
                    m_col++;

                    return true;
                }
                return false;
            }

            private void RemoveQueen(int row)
            {
                m_col--;
                m_rows[row] = m_fwDiagonals[row + m_col] = m_bwDiagonals[row - m_col + m_size - 1] = false;
            }
        }
    }

    class Bank
    {
        public void Deposit(object acctHolder, int amount)
        {
            lock (acctHolder)
            {
                Thread.Sleep(amount);
            }
        }
        public void Withdraw(object acctHolder, int amount)
        {
            lock (acctHolder)
            {
                Thread.Sleep(amount);
            }
        }
    }

    class Profiling
    {
        static bool IsPrime(int number)
        {
            for (int i = 2; i < number; ++i)
                if (number % i == 0) return false;
            return true;
        }

        static void Primes()
        {
            ConcurrentLinkedList<int> list = new ConcurrentLinkedList<int>();
            (from n in Enumerable.Range(0, 100000).AsParallel().AsOrdered()
             where IsPrime(n)
             select n)
             .ForAll(n => list.AddLast(n));
        }
        static void Queens()
        {
            Console.WriteLine(
                "Solutions for {0} queens ({0}x{0} board): {1}",
                12, NQueensSolver.Parallel(12));
        }
        static void Bank()
        {
            Bank bank = new Bank();
            Parallel.Invoke(
                () => Parallel.For(0, 50, n => bank.Deposit(bank, n)),
                () => Parallel.For(0, 50, n => bank.Deposit(bank, 50 - n)));
        }

        static void Main(string[] args)
        {
            //Primes();
            //Queens();
            Bank();
        }
    }
}

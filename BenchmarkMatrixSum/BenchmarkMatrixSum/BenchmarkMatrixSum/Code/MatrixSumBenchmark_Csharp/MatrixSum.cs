using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixSumBenchmark
{
    class MatrixSum
    {
        //---------------------------------------------------------------------

            public enum FillMatrixPolicy { SINGLE_THREAD, MULTI_THREAD };

        //---------------------------------------------------------------------

        int[,] m_matrix;
        int m_rows;
        int m_cols;

        //---------------------------------------------------------------------

        public MatrixSum(string _fileName, int _rows, int _cols, FillMatrixPolicy _fillPolicy = FillMatrixPolicy.SINGLE_THREAD, bool _isInnerMeasure = false)
        {
            //create matrix
            m_rows = _rows;
            m_cols = _cols;
            m_matrix = new int[_rows,_cols];

            //read from file
            Stopwatch sw = Stopwatch.StartNew();

            string input = File.ReadAllText(_fileName);

            if (_isInnerMeasure)
            {
                Console.WriteLine("Read from file : " + sw.ElapsedMilliseconds);
            }

            string[] stringArr = input.Split('\n');

            //fill matrix
            switch (_fillPolicy)
            {
                case FillMatrixPolicy.SINGLE_THREAD:
                    FillMatrixFromStringArr(stringArr, 0, stringArr.Length);
                    break;
                case FillMatrixPolicy.MULTI_THREAD:
                    FillMatrixFromStringArrMultiThread(stringArr);
                    break;
                default:
                    break;
            }
        }

        //---------------------------------------------------------------------

        private void FillMatrixFromStringArr(string[] _lines, int _fromLine, int _numOfLines)
        {
            for (int i=_fromLine; i< _fromLine + _numOfLines; i++)
            {
                string row = _lines[i];
                int j = 0;
                foreach (string col in row.Trim().Split(','))
                {
                    if(!String.IsNullOrEmpty(col))
                    { 
                        m_matrix[i, j] = int.Parse(col.Trim());
                    }
                    j++;
                }
            }
        }

        //---------------------------------------------------------------------

        private void FillMatrixFromStringArrMultiThread(string[] _lines)
        {
            //fill matrix According to proccessors 
            int processorNum = Environment.ProcessorCount;
            int numOfLines = _lines.Length;

            //async fill of matrix with chunks according to num of processors
            Action<string[], int, int> action = FillMatrixFromStringArr;
            int chunkSize = numOfLines / processorNum;
            IAsyncResult[] sync = new IAsyncResult[processorNum];
            for (int i=0; i< processorNum;i++)
            {
                sync[i] = action.BeginInvoke(_lines, i * chunkSize, chunkSize, null, null);
            }

            //wait for matrix to fill (All async tasks to finish)
            for (int i = 0; i < processorNum; i++)
            {
                action.EndInvoke(sync[i]); 
            }           
        }

        //---------------------------------------------------------------------

        public long CalcSum(int _fromRow, int _rowsToCalc)
        {
            long sum = 0;

            for (int i = _fromRow; i < _fromRow + _rowsToCalc; ++i)
            {
                for (int j = 0; j < m_cols; ++j)
                {
                    sum += m_matrix[j, i];
                }
            }
            return sum;
        }

        //---------------------------------------------------------------------

        public long CalcSumMultiThread()
        {
            //Calc sum According to proccessors 
            int processorNum = Environment.ProcessorCount;

            //async calc sum of chunks according to num of processors
            Func<int, int, long> func = CalcSum;
            int chankSize = m_rows / processorNum;
            IAsyncResult[] sync = new IAsyncResult[processorNum];
            for (int i = 0; i < processorNum; i++)
            {
                sync[i] = func.BeginInvoke(i * chankSize, chankSize, null, null);
            }

            //wait for matrix to fill (All async tasks to finish) -> And get calculated result of chunk
            long MatrixSum = 0;
            for (int i = 0; i < processorNum; i++)
            {
                long chunkSum = func.EndInvoke(sync[i]);
                MatrixSum += chunkSum;
            }

            return MatrixSum;
        }
        //---------------------------------------------------------------------
    }
}

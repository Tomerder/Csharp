// MatrixSumBenchmarkCpp.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "MatrixSum.h"

#define FILE_NAME (string)"C:\\Matrix.txt"
#define ROWS 5000
#define COLS 5000

static void FillMatrixAndCalcSum()
{
	MatrixSum matrixSum(FILE_NAME, ROWS, COLS);
}

int main()
{
	FillMatrixAndCalcSum();

    return 0;
}




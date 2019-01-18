// MatrixBenchMark.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Matrix.h"
#include <iostream>
#include <fstream>

using namespace std;


int _tmain(int argc, _TCHAR* argv[])
{
	SYSTEMTIME st1, st2;
	GetSystemTime(&st1);
	Matrix matrix("Matrix.txt", 1);
	GetSystemTime(&st2);
	cout << "file reading:" << matrix.fileReadingTime << endl;
	cout << "parsing:" << matrix.parsingTime << endl;
	GetSystemTime(&st1);
	matrix.SumMatrix(1);
	GetSystemTime(&st2);
	cout << "calculation:" << CalculateDeltaTime(st1, st2) << endl;
	return 0;
}


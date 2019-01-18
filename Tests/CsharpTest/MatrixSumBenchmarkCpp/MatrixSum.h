#pragma once
#ifndef MATRIX_SUM_X
#define MATRIX_SUM_X

using namespace std;
#include <string>
#include <fstream>
#include <sstream>
#include <vector>
#include <cstdlib>

class MatrixSum
{
private:
	int* m_Matrix;

public:
	MatrixSum(string _fileName, int _rows, int _cols);
	~MatrixSum();
};

#endif

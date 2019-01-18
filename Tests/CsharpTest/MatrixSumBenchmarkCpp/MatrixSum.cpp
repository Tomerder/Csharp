#include "stdafx.h"
#include "MatrixSum.h"

#define GET_MATRIX_POS(Y,X,COLS) (Y*COLS + X)

void Split(const string &text, char sep, vector<string>& _output)
{
	size_t start = 0, end = 0;
	while ((end = text.find(sep, start)) != string::npos) {
		_output.push_back(text.substr(start, end - start));
		start = end + 1;
	}
	_output.push_back(text.substr(start));
}

MatrixSum::MatrixSum(string _fileName, int _rows, int _cols)
{
	//create matrix
	m_Matrix = new int[_rows*_cols];

	//get file content into string
	ifstream fileToParse(_fileName);
	stringstream buffer;
	buffer << fileToParse.rdbuf();
	string str = buffer.str();

	//fill matrix from string
	vector<string> lines;
	Split(str, '\n', lines);
	for (int i = 0; i < lines.size(); i++)
	{
		vector<string> cells;
		Split(lines[i], ',', cells);
		for (int j = 0; j < lines.size() - 1; j++)
		{
			if (cells[j] != "")
			{
				m_Matrix[GET_MATRIX_POS(i, j, _cols)] = stoi(cells[j]);
			}
		}

	}

}


MatrixSum::~MatrixSum()
{
	delete m_Matrix;
}



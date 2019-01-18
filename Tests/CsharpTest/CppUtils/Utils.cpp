#include "pch.h"
#include "Utils.h"
#include <fstream>
#include <sstream>

Utils::Utils()
{
}

bool Utils::GetFileIntoString(string _fileName, string & _output)
{
	ifstream fileToParse(_fileName);
	stringstream buffer;
	buffer << fileToParse.rdbuf();
	string strFromFile = buffer.str();

	return true;
}

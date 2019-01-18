#pragma once

#include <string>
using namespace std;

static class Utils sealed
{
public:
	Utils();
	bool GetFileIntoString(string _fileName, string& _output);
};


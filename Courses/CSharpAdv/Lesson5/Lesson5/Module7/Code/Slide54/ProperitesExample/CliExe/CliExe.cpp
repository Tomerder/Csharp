// CliExe.cpp : main project file.

#include "stdafx.h"

using namespace System;
using namespace CliDllNamespace;

int main(array<System::String ^> ^args)
{
	Complex complex(2.0, 3.0);
	Console::WriteLine("{0} + {1}i",
		complex.Real, complex.Imaginary);
	Console::WriteLine("= {0:F2}*(cos({1:F2}) + i*sin({1:F2}))",
		complex.R, complex.Theta);
	return 0;
}

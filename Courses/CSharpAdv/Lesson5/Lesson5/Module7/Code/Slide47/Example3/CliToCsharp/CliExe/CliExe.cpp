// CliExe.cpp : main project file.

#include "stdafx.h"

using namespace System;
using namespace CsharpDll;
#include <stdio.h>

int main(array<System::String ^> ^args)
{
    Console::WriteLine(L"Hello World");
	printf("Amir");
	Class1^ c1 = gcnew Class1();
	c1->F();
    return 0;
}

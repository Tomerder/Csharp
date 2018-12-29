// CliExe.cpp : main project file.

#include "stdafx.h"

using namespace System;
using namespace CsharpDll;

int main(array<System::String ^> ^args)
{
	Person^ p = gcnew Person("XXX", "YYY");

	String^ firstName = p->FirstName;
	Console::WriteLine(firstName);

	String^ fullName = p->GetFullName();
	Console::WriteLine(fullName);

    return 0;
}

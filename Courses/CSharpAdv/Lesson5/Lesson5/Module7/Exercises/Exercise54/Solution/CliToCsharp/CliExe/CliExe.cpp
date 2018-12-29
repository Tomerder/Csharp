// ExeCli.cpp : main project file.

#include "stdafx.h"

using namespace System;
using namespace CsharpDll;

int main(array<System::String ^> ^args)
{
	Person^ person = gcnew Person("Amir", "Adler");
	String^ firstName = person->FirstName;
	Console::WriteLine(firstName);
	String^ fullName = person->GetFullName();
	Console::WriteLine(fullName);
	return 0;
}

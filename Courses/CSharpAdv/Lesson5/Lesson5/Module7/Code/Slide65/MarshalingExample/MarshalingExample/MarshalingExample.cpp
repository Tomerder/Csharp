// MarshalingExample.cpp : main project file.

#include "stdafx.h"
#include <vcclr.h>

using namespace System;
using namespace System::Runtime::InteropServices;

//Demonstrates manual marshaling of strings from native to managed,
//using ANSI and Unicode formats.
int main(array<System::String ^> ^args)
{
	System::String^ s = "Hello World!";
	pin_ptr<const wchar_t> p = PtrToStringChars(s);

	const wchar_t* unicode = p;
	char* ansi = (char*)(void*) Marshal::StringToHGlobalAnsi(s);

	System::String^ s2 = gcnew System::String(ansi);
	System::String^ s3 = gcnew System::String(unicode);
	return 0;
}

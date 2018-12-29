// ExeCli.cpp : main project file.

#include "stdafx.h"
#include "SomeClass.h"

using namespace System;


int main(array<System::String ^> ^args)
{
	SomeClass^ someClass = gcnew SomeClass();
	
	int i = 5;
	someClass->F1(i);
	Console::WriteLine(i); // 5
	someClass->F2(&i);
	Console::WriteLine(i); // 0

	Int32 j = 6;
	someClass->F3(j);
	Console::WriteLine(j); // 6
	someClass->F4(j);
	Console::WriteLine(j); // 0

	String^ s = "Amir";
	someClass->F5(s);
	Console::WriteLine(s); // Amir
	someClass->F6(s);
	Console::WriteLine(s); // Adler


	return 0;
}


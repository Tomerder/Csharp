// PermutationExample.cpp : main project file.

#include "stdafx.h"
#include "Permutations.h"

using namespace System;

//Demonstrates the difference between a destructor (Dispose) and a finalizer
//in C++/CLI, and shows that the destructor automatically calls GC::SuppressFinalize
//so that the finalizer is invoked only once.
int main(array<System::String ^> ^args)
{
	array<System::String^>^ EmptyArray = gcnew array<System::String^>(0);

	Permutations^ p1 = gcnew Permutations(EmptyArray);
	delete p1;	//Calls the "destructor"
	Console::WriteLine(1);

	Permutations^ p2 = gcnew Permutations(EmptyArray);
	//No explicit delete, so finalizer will be called later

	{
		Permutations p3(EmptyArray);
		p3.HasNextPermutation;	//Direct access, no ->
	}	//"destructor" called at this line

	Console::WriteLine(2);

	return 0;
}

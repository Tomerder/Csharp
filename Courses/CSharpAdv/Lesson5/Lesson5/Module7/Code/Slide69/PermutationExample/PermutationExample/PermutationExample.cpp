// PermutationExample.cpp : main project file.

#include "stdafx.h"
#include "Permutations.h"

using namespace System;

//The Permutations class is a 
//managed type which uses the STL next_permutation algorithm and the STL vector<T>
//container to generate string permutations.
int main(array<System::String ^> ^args)
{
	array<System::String^>^ strings = { "A", "B", "C" };
	Permutations^ perm = gcnew Permutations(strings);
	do
	{
		System::Array::ForEach<System::String^>(perm->Next(),
			gcnew Action<System::String^>(System::Console::Write));
		System::Console::WriteLine();
	} while (perm->HasNextPermutation);
	
	return 0;
}

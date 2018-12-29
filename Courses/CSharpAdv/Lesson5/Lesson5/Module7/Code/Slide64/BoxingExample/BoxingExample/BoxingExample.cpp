// BoxingExample.cpp : main project file.

#include "stdafx.h"

using namespace System;

generic <typename T>
void Swap(T% first, T% second)
{
	T temp = first;
	first = second;
	second = temp;
}

//Demonstrates that boxing is an inherent language feature and can be
//performed in a type-safe way, allowing direct access into the box.
void BoxingAndUnboxing()
{
	int value = 42;
	int^ boxed = value;
	System::Object^ obj = boxed;

	int copy = *boxed;	//Strongly-typed, no cast
	int% refToTheBox = *boxed;
	refToTheBox = 41;
	int newValue = 43;
	Swap(*boxed, newValue);
}

int main(array<System::String ^> ^args)
{
	BoxingAndUnboxing();
    return 0;
}

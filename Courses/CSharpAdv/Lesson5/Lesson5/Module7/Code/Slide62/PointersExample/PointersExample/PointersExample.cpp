// PointersExample.cpp : main project file.

#include "stdafx.h"
#include "Box.h"

using namespace System;

//Demonstrates managed and native pointers and handles (references).
int main(array<System::String ^> ^args)
{
	//Pointer to the native heap:
	NativeBox* nativeBox = new NativeBox;
	nativeBox->Boxify();
	(*nativeBox).Boxify();

	//Pointer to the managed (GC) heap:
	ManagedBox^ managedBox = gcnew ManagedBox;
	managedBox->Boxify();
	(*managedBox).Boxify();

	//error C2440: 'initializing' :
	//cannot convert from 'cli::interior_ptr<Type>' to 'int *'
	//
	//int* pToTheBox = &managedBox->InTheBox;

	//Declare a pinning pointer, and then reach
	//for the actual address:
	pin_ptr<int> pToTheBox = &managedBox->InTheBox;
	int* p = pToTheBox;
	
	return 0;
}

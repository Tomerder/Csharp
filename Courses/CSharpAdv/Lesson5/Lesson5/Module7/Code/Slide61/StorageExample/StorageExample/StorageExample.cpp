// StorageExample.cpp : main project file.

#include "stdafx.h"
#include <stdio.h>

using namespace System;

class T1 
{
public:
	int i;
	T1() { printf("T1\n"); }
	~T1() { printf("~T1 %d\n", i); }
};

ref class T2 
{
public:
	int i;
	T2() { Console::WriteLine("T2"); }
	~T2() { Console::WriteLine("~T2 {0}", i); }
};

int main(array<System::String ^> ^args)
{
	T1* t1 = new T1;
	t1->i = 1;
	T2^ t2 = gcnew T2;
	t2->i = 2;
	T1 t3;
	t3.i = 3;
	T2 t4;
	t4.i = 4;
	return 0;
}

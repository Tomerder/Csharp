#include "stdafx.h"
#include "SomeClass.h"


SomeClass::SomeClass()
{
}

void SomeClass::F1(int i)
{
	i = 0;
}

void SomeClass::F2(int* p)
{
	*p = 0;
}

void SomeClass::F3(Int32 i)
{
	i = 0;
}

void SomeClass::F4(Int32% p)
{
	p = 0;
}

void SomeClass::F5(String^ p)
{
	p = "Adler";
}

void SomeClass::F6(String^% p)
{
	p = "Adler";
}

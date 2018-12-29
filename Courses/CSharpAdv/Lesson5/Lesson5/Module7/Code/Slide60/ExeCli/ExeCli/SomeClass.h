#pragma once

using namespace System;

ref class SomeClass
{
public:
	SomeClass();
	void F1(int i);
	void F2(int* p);
	void F3(Int32 i);
	void F4(Int32% p);
	void F5(String^ p);
	void F6(String^% p);
};


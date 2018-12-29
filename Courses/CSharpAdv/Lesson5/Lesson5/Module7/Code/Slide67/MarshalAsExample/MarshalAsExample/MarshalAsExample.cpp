// MarshalAsExample.cpp : main project file.

#include "stdafx.h"
#include "XmlInitializable.h"

using namespace System;

//Demonstrates the interoperability options of complex managed and native
//types.  The XmlInitializable class is a native type which uses an XmlDocument
//and an XmlTextReader for its initialization. 
int main(array<System::String ^> ^args)
{
	System::IO::File::WriteAllText("sample.xml", "<doc />");

	XmlInitializable initializable;
	initializable.Load("sample.xml");
	return 0;
}

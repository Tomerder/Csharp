// CllDll.h

#pragma once

using namespace System;
#include <stdio.h>

namespace CllDll {

	public ref class Class1
	{
		// TODO: Add your methods for this class here.
	public :
		void F()
		{
			//C++ class - Console
			Console::WriteLine("Hello");
			//C function - printf
			printf("World\n");
		}
	};
}

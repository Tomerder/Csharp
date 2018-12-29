// CliDll.h

#pragma once

using namespace System;
#include <stdio.h>

namespace CliDll {

	public ref class Class1
	{
		// TODO: Add your methods for this class here.
	public:
		void F()
		{
			Console::Write("Hello ");
			printf("World");
		}
	};
}

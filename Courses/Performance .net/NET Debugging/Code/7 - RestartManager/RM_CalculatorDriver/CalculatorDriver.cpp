// CalculatorDriver.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <windows.h>
#include <werapi.h>

#include "..\\RM_CalculatorDll\\CalculatorDll.h"
#ifdef _DEBUG
#pragma comment (lib, "..\\Debug\\CalculatorDll.lib")
#else
#pragma comment (lib, "..\\Release\\CalculatorDll.lib")
#endif

#include <iostream>
#include <string>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	if (argc > 1)
		printf("Restarted successfully, please proceed\n");

    if (!SUCCEEDED(RegisterApplicationRestart (TEXT("/restarted"), 0)))
		printf("Unable to register for application restart\n");

	for (;;)
	{
		int first, second;
		cout << "Enter first number: ";
		cin >> first;
		cout << "Enter second number: ";
		cin >> second;

		cout << "Result: " << AddTwoNumbers(first, second) << endl;
	}

	return 0;
}


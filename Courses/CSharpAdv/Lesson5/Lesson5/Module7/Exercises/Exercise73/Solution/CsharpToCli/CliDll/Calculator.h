#pragma once

namespace CalculatorNamespace
{
	public delegate void PlusEventHandler(int res);

	public ref class Calculator
	{
	public:
		Calculator(void);
		void Plus(int num1, int num2);
		event PlusEventHandler^ PlusResult;
	};
}



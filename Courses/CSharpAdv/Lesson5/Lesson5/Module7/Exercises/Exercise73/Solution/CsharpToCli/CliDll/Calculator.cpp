#include "stdafx.h"
#include "Calculator.h"

namespace CalculatorNamespace
{
	Calculator::Calculator(void)
	{
	}

	void Calculator::Plus(int num1, int num2)
	{
		int res = num1 + num2;
		PlusResult(res);
	}
}


#include <cstdlib>
#include <cstdio>
#include <windows.h>

int* g_ptr;

void buggy_function()
{
	if (rand() % 43 == 0)
	{
		g_ptr = NULL;
	}
}

int wmain(int argc, wchar_t* argv[])
{
	int i = 5;
	g_ptr = &i;

	while (true)
	{
		_wprintf_p(L"%d\n", *g_ptr);
		buggy_function();
	}

	return 0;
}
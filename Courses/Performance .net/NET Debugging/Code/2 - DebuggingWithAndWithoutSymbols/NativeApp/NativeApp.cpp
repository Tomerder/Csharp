
#include <stdio.h>

void baz()
{
	*((int*)0) = 5;
}

void bar()
{
	baz();
}

void foo()
{
	bar();
}

int wmain(int argc, wchar_t* argv[])
{
	getchar();
	foo();
	return 0;
}
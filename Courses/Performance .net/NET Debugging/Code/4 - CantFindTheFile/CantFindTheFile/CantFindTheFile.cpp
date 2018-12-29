
#include <stdio.h>
#include <windows.h>

void buggy_function(wchar_t* path)
{
	path[5] = 'K';
}

int wmain(int argc, wchar_t* argv[])
{
	wchar_t path[MAX_PATH];
	_putws(L"Enter file name to create: ");
	_getws_s(path);
	buggy_function(path);

	HANDLE hf = ::CreateFile(path, GENERIC_READ | GENERIC_WRITE, 0,
		NULL, CREATE_ALWAYS, 0, NULL);
	if (hf == INVALID_HANDLE_VALUE)
	{
		_wprintf_p(L"Can't create your file!\n");
	}
	else
	{
		_wprintf_p(L"Created your file successfully\n");
		::CloseHandle(hf);
	}

	return 0;
}
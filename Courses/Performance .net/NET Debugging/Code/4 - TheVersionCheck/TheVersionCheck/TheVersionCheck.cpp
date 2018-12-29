
#include <stdio.h>
#include <windows.h>

int wmain(int argc, wchar_t* argv[])
{
	_putws(L"Welcome to the stupid version checking program!");
	_putws(L"Let me see if I support your platform...");

	OSVERSIONINFO verInfo = {0};
	verInfo.dwOSVersionInfoSize = sizeof(verInfo);
	if (!::GetVersionEx(&verInfo))
	{
		_putws(L"I can't even get your platform version!");
	}
	else
	{
		if (verInfo.dwMajorVersion != 5)
		{
			_putws(L"I don't support Windows versions before 2000 and XP.");
			_putws(L"Please upgrade at least to Windows XP Service Pack 2 and try again.");
			_wprintf_p(L"[You are running: NT %d.%d]\n", verInfo.dwMajorVersion, verInfo.dwMinorVersion);
		}
		else
		{
			_putws(L"OK, I'm willing to play on your platform.");
		}
	}
	_putws(L"Press any key to exit.");
	_getchar_nolock();

	return 0;
}
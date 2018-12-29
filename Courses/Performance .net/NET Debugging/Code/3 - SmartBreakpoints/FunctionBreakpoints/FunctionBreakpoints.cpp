
#define UNICODE
#include <windows.h>
#include <cstdio>
#include <cstdlib>

HANDLE g_hEvent;

void buggy_function()
{
	if (rand() % 57 == 0)
	{
		::CloseHandle(g_hEvent);
	}
}

int wmain(int argc, wchar_t* argv[])
{
	g_hEvent = ::CreateEvent(NULL, FALSE, FALSE, L"Sasha");
	::CloseHandle(0);

	while (true)
	{
		if (FALSE == ::SetEvent(g_hEvent))
		{
			DWORD err;
			_wprintf_p(L"Error occurred: 0x%x\n",
				err = ::GetLastError());
			wchar_t buf[1024];
			::FormatMessage(
				FORMAT_MESSAGE_FROM_SYSTEM, NULL, err,
				0, buf, ARRAYSIZE(buf), NULL);
			_wprintf_p(L"Error description: %s\n",
				buf);
			break;
		}
		buggy_function();
	}
}
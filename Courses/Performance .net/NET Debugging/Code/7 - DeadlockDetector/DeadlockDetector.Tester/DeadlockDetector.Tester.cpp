
#include <windows.h>

HANDLE g_hMutex1;
HANDLE g_hMutex2;

DWORD WINAPI Thread1(LPVOID)
{
	WaitForSingleObject(g_hMutex1, INFINITE);
	Sleep(1000);
	WaitForSingleObject(g_hMutex2, INFINITE);
	Sleep(1000);
	ReleaseMutex(g_hMutex1);
	Sleep(1000);
	ReleaseMutex(g_hMutex2);
	return 0;
}

DWORD WINAPI Thread2(LPVOID)
{
	WaitForSingleObject(g_hMutex2, INFINITE);
	Sleep(1000);
	WaitForSingleObject(g_hMutex1, INFINITE);
	Sleep(1000);
	ReleaseMutex(g_hMutex2);
	Sleep(1000);
	ReleaseMutex(g_hMutex1);
	return 0;
}

int main()
{
	g_hMutex1 = CreateMutex(NULL, FALSE, L"Mutex1");
	g_hMutex2 = CreateMutex(NULL, FALSE, L"Mutex2");

	HANDLE rghThreads[2];
	rghThreads[0] = CreateThread(NULL, 0, Thread1, NULL, 0, NULL);
	rghThreads[1] = CreateThread(NULL, 0, Thread2, NULL, 0, NULL);

	WaitForMultipleObjects(2, rghThreads, TRUE, INFINITE);

	return 0;
}


// Handles.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>

HANDLE g_hEvent;

DWORD WINAPI MyThread(LPVOID)
{
	printf("Thread initialized - starting work\n");
	Sleep(5000);
	printf("Thread finished - waiting for termination event\n");
	WaitForSingleObject(g_hEvent, INFINITE);
	return 0;
}

int _tmain(int argc, _TCHAR* argv[])
{
	HANDLE hThread = CreateThread(NULL, 0, MyThread, NULL, 0, NULL);

	g_hEvent = CreateEvent(NULL, TRUE, FALSE, L"MyEvent");
	Sleep(6000);

	printf("MAIN: Press any key to signal the thread's termination event\n");
	getchar();
	
	CloseHandle(g_hEvent);
	SetEvent(g_hEvent);

	printf("MAIN: Waiting for the thread to terminate\n");
	WaitForSingleObject(hThread, INFINITE);

	return 0;
}


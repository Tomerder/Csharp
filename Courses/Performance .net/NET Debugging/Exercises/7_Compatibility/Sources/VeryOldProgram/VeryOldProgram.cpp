// VeryOldProgram.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <Windows.h>

void PrintError()
{
	wprintf(L"This program cannot run because its configuration settings are corrupt.\n");
	getchar();
}

int _tmain(int argc, _TCHAR* argv[])
{
	HKEY hKey;
	LSTATUS status = RegOpenKey(HKEY_LOCAL_MACHINE, L"MyOldApplication", &hKey);
	if (status != ERROR_SUCCESS)
	{
		PrintError();
		return 1;
	}

	WCHAR str[100];
	DWORD strLen = ARRAYSIZE(str)*sizeof(WCHAR);
	status = RegGetValue(hKey, L"Settings", L"LicensedUsername", RRF_RT_REG_SZ, NULL, str, &strLen);
	if (status != ERROR_SUCCESS)
	{
		RegCloseKey(hKey);
		PrintError();
		return 1;
	}

	if (wcscmp(str, L"UserMcUser") != 0)
	{
		RegCloseKey(hKey);
		PrintError();
		return 1;
	}

	wprintf(L"This program is licensed to %s.\nThank you for using our great program. Good day.\n", str);
	RegCloseKey(hKey);

	getchar();
	return 0;
}


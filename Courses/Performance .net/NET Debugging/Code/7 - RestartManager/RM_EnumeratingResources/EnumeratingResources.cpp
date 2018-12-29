// EnumeratingResources.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <windows.h>
#include <RestartManager.h>

#pragma comment (lib, "Rstrtmgr.lib")

void EnumerateFileUsage(LPCWSTR pszName)
{
	DWORD dVal;
	DWORD dwSessionHandle = -1;
	WCHAR sessKey[CCH_RM_SESSION_KEY+1];
	UINT nProcInfo = 100;
	UINT nProcInfoNeeded;
	RM_PROCESS_INFO *rgProcInfo = NULL;
	DWORD lpdwRebootReason = 0;
	DWORD nServices = 0; //=1
	//LPCWSTR rgsServices[] = { L"iisadmin" };
	DWORD nProcs = 0;
	DWORD nFiles = 1;
	LPCWSTR* rgsFiles = new LPCWSTR;
	RM_PROCESS_INFO* rgAffectedApps;
	UINT nAffectedApps;

	rgsFiles[0] = pszName;
	rgAffectedApps = new RM_PROCESS_INFO[nProcInfo];

	// Starting Session
	dVal = RmStartSession( &dwSessionHandle, 0, sessKey );
	if (dVal != ERROR_SUCCESS)
		goto RM_END;

	// Register items
	dVal = RmRegisterResources(
			dwSessionHandle,
			nFiles, rgsFiles,			// Files
			nProcs, NULL,			// Processes
			nServices, /*rgsServices*/NULL );		// Services
	if (dVal != ERROR_SUCCESS)
		goto RM_END;

	// Getting affected apps
	dVal = RmGetList(
			dwSessionHandle,
			&nProcInfoNeeded,
			&nAffectedApps, rgAffectedApps, &lpdwRebootReason );
	if (dVal != ERROR_SUCCESS)
		goto RM_END;

	for (int i = 0; i < nAffectedApps; ++i)
	{
		printf("%S %d\n", rgAffectedApps[i].strAppName, rgAffectedApps[i].Process.dwProcessId);
	}

	// Shut down apps
	dVal = RmShutdown( dwSessionHandle, 0, NULL );
	if (dVal != ERROR_SUCCESS)
		goto RM_END;

	puts("Apps shut down, waiting for key to continue\n");
	getchar();

	// Restart apps
	dVal = RmRestart( dwSessionHandle, 0, NULL );
	if (dVal != ERROR_SUCCESS)
		goto RM_END;


RM_END:

	if (rgAffectedApps)
	{
		delete [] rgAffectedApps;
		rgAffectedApps = NULL;
	}
	
	if (dwSessionHandle != -1)
	{
		// Clean up session
		dVal = RmEndSession( dwSessionHandle );
		dwSessionHandle = -1;
	}

	delete rgsFiles;
}

int _tmain(int argc, _TCHAR* argv[])
{
	EnumerateFileUsage(L"CalculatorDll.dll");
	//EnumerateFileUsage(L"d:\\Documents and Settings\\Sasha\\My Documents\\Courses\\Windows 2008 Server\\Demos\\release\\TestRecovery.exe");

	return 0;
}


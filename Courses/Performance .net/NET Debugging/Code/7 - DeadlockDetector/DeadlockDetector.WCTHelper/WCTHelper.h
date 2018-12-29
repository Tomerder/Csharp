
#pragma once

#include <windows.h>
#include <wct.h>
#include <psapi.h>
#include <tlhelp32.h>
#include <wchar.h>
#include <stdio.h>
#include <stdlib.h>

namespace DeadlockDetector {
namespace WctHelper {

	typedef struct _STR_ARRAY
	{
		wchar_t Desc[32];
	} STR_ARRAY;
	STR_ARRAY STR_OBJECT_TYPE[] =
	{
		{L"CriticalSection"},
		{L"SendMessage"},
		{L"Mutex"},
		{L"Alpc"},
		{L"Com"},
		{L"ThreadWait"},
		{L"ProcWait"},
		{L"Thread"},
		{L"ComActivation"},
		{L"Unknown"},
		{L"Max"}
	};

	BOOL GrantDebugPrivilege()
	{
		BOOL             fSuccess    = FALSE;
		HANDLE           TokenHandle = NULL;
		TOKEN_PRIVILEGES TokenPrivileges;

		if (!OpenProcessToken(GetCurrentProcess(),
							  TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY,
							  &TokenHandle))
		{
			goto Cleanup;
		}

		TokenPrivileges.PrivilegeCount = 1;

		if (!LookupPrivilegeValue(NULL,
								  SE_DEBUG_NAME,
								  &TokenPrivileges.Privileges[0].Luid))
		{
			goto Cleanup;
		}

		TokenPrivileges.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;

		if (!AdjustTokenPrivileges(TokenHandle,
								   FALSE,
								   &TokenPrivileges,
								   sizeof(TokenPrivileges),
								   NULL,
								   NULL))
		{
			goto Cleanup;
		}

		fSuccess = TRUE;

	 Cleanup:

		if (TokenHandle)
		{
			CloseHandle(TokenHandle);
		}

		return fSuccess;
	}

	HWCT g_WctHandle = NULL;
	HMODULE g_Ole32Hnd = NULL;

	BOOL InitCOMAccess()
	{
		HMODULE               ModuleHandle = NULL;
		PCOGETCALLSTATE       CallStateCallback;
		PCOGETACTIVATIONSTATE ActivationStateCallback;

		// Get a handle to OLE32.DLL. You must keep this handle around
		// for the life time for any WCT session.
		g_Ole32Hnd = LoadLibrary(L"ole32.dll");
		if (!g_Ole32Hnd)
		{
			return FALSE;
		}

		// Retrieve the function addresses for the COM helper APIs.
		CallStateCallback = (PCOGETCALLSTATE)
			GetProcAddress(g_Ole32Hnd, "CoGetCallState");
		if (!CallStateCallback)
		{
			return FALSE;
		}

		ActivationStateCallback = (PCOGETACTIVATIONSTATE)
			GetProcAddress(g_Ole32Hnd, "CoGetActivationState");
		if (!ActivationStateCallback)
		{
			return FALSE;
		}

		// Register these functions with WCT.
		RegisterWaitChainCOMCallback(CallStateCallback,
									 ActivationStateCallback);
		return TRUE;
	}

	void PrintWaitChain(DWORD ThreadId, System::Text::StringBuilder^ resultString);

	BOOL CheckThreads(DWORD ProcId, System::Text::StringBuilder^ resultString)
	{
		HANDLE process = OpenProcess(PROCESS_ALL_ACCESS, FALSE, ProcId);
		if (process == NULL)
			return FALSE;

		//WCHAR file[MAX_PATH];
		//if (GetProcessImageFileName(process, file, ARRAYSIZE(file)) > 0)
		//{
		//	PCWSTR filePart = wcsrchr(file, L'\\');
		//	if (filePart)
		//	{
		//		filePart++;
		//	}
		//	else
		//	{
		//		filePart = file;
		//	}

		//	printf("%S", filePart);
		//}

		HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPTHREAD, ProcId);
		if (snapshot == NULL)
			return FALSE;

		THREADENTRY32 thread;
		thread.dwSize = sizeof(thread);

		if (Thread32First(snapshot, &thread))
		{
			do
			{
				if (thread.th32OwnerProcessID == ProcId)
				{
					// Open a handle to this specific thread
					HANDLE threadHandle = OpenThread(THREAD_ALL_ACCESS,
													 FALSE,
													 thread.th32ThreadID);
					if (threadHandle != NULL)
					{
						DWORD exitCode;
						GetExitCodeThread(threadHandle, &exitCode);
						if (exitCode == STILL_ACTIVE)
						{
							PrintWaitChain(thread.th32ThreadID, resultString);
						}
						CloseHandle(threadHandle);
					}
				}
			} while (Thread32Next(snapshot, &thread));
		}

		CloseHandle(snapshot);
		CloseHandle(process);
		return TRUE;
	}

	void PrintWaitChain(DWORD ThreadId, System::Text::StringBuilder^ resultString)
	{
		WAITCHAIN_NODE_INFO NodeInfoArray[WCT_MAX_NODE_COUNT];
		DWORD               Count, i;
		BOOL                IsCycle;

		printf("%d: ", ThreadId);

		Count = WCT_MAX_NODE_COUNT;

		if (!GetThreadWaitChain(g_WctHandle,
								NULL,
								WCTP_GETINFO_ALL_FLAGS,
								ThreadId,
								&Count,
								NodeInfoArray,
								&IsCycle))
		{
			/*printf("Error (0X%x)\n", GetLastError());*/
			return;
		}

		if (Count > WCT_MAX_NODE_COUNT)
		{
			/*printf("Found additional nodes %d\n", Count);*/
			Count = WCT_MAX_NODE_COUNT;
		}

		// Loop over all the nodes returned and print useful information.
		for (i = 0; i < Count; i++)
		{
			switch (NodeInfoArray[i].ObjectType)
			{
				case WctThreadType:
					resultString->AppendFormat("[{0}:{1}:{2}]->",
						   NodeInfoArray[i].ThreadObject.ProcessId,
						   NodeInfoArray[i].ThreadObject.ThreadId,
						   ((NodeInfoArray[i].ObjectStatus == WctStatusBlocked) ? "blocked" : "ready"));
					break;

				default:
					// A synchronization object.

					if (NodeInfoArray[i].LockObject.ObjectName[0] != L'\0')
					{
						resultString->AppendFormat("[{0}:{1}]->",
							gcnew System::String(STR_OBJECT_TYPE[NodeInfoArray[i].ObjectType-1].Desc),
							gcnew System::String(NodeInfoArray[i].LockObject.ObjectName));
					}
					else
					{
						resultString->AppendFormat("[{0}]",
							gcnew System::String(STR_OBJECT_TYPE[NodeInfoArray[i].ObjectType-1].Desc));
					}
					if (NodeInfoArray[i].ObjectStatus == WctStatusAbandoned)
					{
						resultString->Append("<abandoned>");
					}
					break;
			}
		}

		resultString->Append("[End]");

		if (IsCycle)
		{
			resultString->Append(" !!!Deadlock!!!");
		}

		resultString->AppendLine();
	}

	public ref class NativeWctHelper abstract sealed
	{
	public:
		static System::String^ GetWaitChainRepresentationForProcess(int processId)
		{
			if (g_Ole32Hnd == NULL)
			{
				if (!InitCOMAccess())
					return "<Error>";
				GrantDebugPrivilege();
			}

			g_WctHandle = OpenThreadWaitChainSession(0, NULL);
			if (NULL == g_WctHandle)
				goto Cleanup;

			System::Text::StringBuilder^ resultString = gcnew System::Text::StringBuilder();
			CheckThreads(processId, resultString);

			CloseThreadWaitChainSession(g_WctHandle);

			return resultString->ToString();

Cleanup:
			if (NULL != g_Ole32Hnd)
				CloseHandle(g_Ole32Hnd);
			return "<Error>";
		}
	};

}
}
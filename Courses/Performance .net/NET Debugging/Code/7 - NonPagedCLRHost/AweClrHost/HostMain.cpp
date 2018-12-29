//
//Non-paged CLR Host
//
//	By	Sasha Goldshtein - http://blogs.microsoft.co.il/blogs/sasha
//	and Alon Fliess		 - http://blogs.microsoft.co.il/blogs/alon
//
//All rights reserved (2008).  When incorporating any significant portion of the
//code into your own project, you must retain this copyright notice.
//

#include "stdafx.h"

#include "HostControl.h"
#pragma comment (lib, "mscoree.lib")

int wmain(int argc, wchar_t* argv[])
{
	wprintf(L"AWE CLR host by Sasha Goldshtein and Alon Fliess - "
#if defined(_WIN64)
		L"64-bit"
#else
		L"32-bit"
#endif
		L"\n");
	wprintf(L"Sasha's blog: http://blogs.microsoft.co.il/blogs/sasha\n");
	wprintf(L"Alon's blog:  http://blogs.microsoft.co.il/blogs/alon\n");
	wprintf(L"Contact us:   http://blogs.microsoft.co.il/blogs/sasha/contact.aspx\n");
	wprintf(L"---------------------------------------------------------------------------\n");
	if (argc < 1)
	{
		wprintf(L"Usage: %S <AssemblyPath> [<EntryTypeName>] [<EntryMethodName>] [<Parameter>]\n", argv[0]);
		wprintf(L"\nThe entry point method must accept a single string parameter and return Int32.\n");
	}

	HRESULT hr;
	ICLRRuntimeHost* pCLR;

	hr = ::CorBindToRuntimeEx(L"v2.0.50727", L"svr", STARTUP_SERVER_GC|STARTUP_HOARD_GC_VM,
		CLSID_CLRRuntimeHost, IID_ICLRRuntimeHost, (LPVOID*)&pCLR);
	if (FAILED(hr)) 
	{
		wprintf(L"Failed to load CLR: 0x%X\n", hr);
		ExitProcess(-1);
	}

	HostControl hostControl;

	hr = pCLR->SetHostControl(&hostControl);
	if (FAILED(hr))
	{
		wprintf(L"Failed to set CLR host control: 0x%X\n", hr);
		ExitProcess(-1);
	}

	hr = pCLR->Start();
	if (FAILED(hr))
	{
		wprintf(L"Failed to start CLR: 0x%X\n", hr);
		ExitProcess(-1);
	}

	const wchar_t* szApplication = argc<2 ? L"TestApplication.exe" : argv[1];
	const wchar_t* szEntryType = argc<3 ? L"TestApplication.Program" : argv[2];
	const wchar_t* szEntryMethod = argc<4 ? L"Main" : argv[3];
	const wchar_t* szParameter = argc<5 ? hostControl.EffectiveMinWorkingSetSizeString() : argv[4];

	DWORD dwReturn;
	hr = pCLR->ExecuteInDefaultAppDomain(szApplication, szEntryType, szEntryMethod, szParameter, &dwReturn);
	if (FAILED(hr))
	{
		wprintf(L"Failed to execute assembly: 0x%X\n", hr);
	}
	else
	{
		wprintf(L"Assembly returned: %d\n", dwReturn);
	}

	hr = pCLR->Stop();
	if (FAILED(hr))
	{
		wprintf(L"Failed to stop CLR: 0x%X\n", hr);
		ExitProcess(-1);
	}

	//TODO: Figure out why the app triggers a DEP when exiting if the CRT does the exit dance.
	//For now, use ExitProcess(...) instead of CRT.

	ExitProcess(0);

	return 0;
}


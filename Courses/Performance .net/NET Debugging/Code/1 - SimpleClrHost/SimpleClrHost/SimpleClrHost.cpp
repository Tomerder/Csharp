// SimpleClrHost.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <mscoree.h>
#pragma comment (lib, "mscoree.lib")

#import "D:\\Users\\Sasha\\Documents\\Courses\\NET Debugging\\Code\\1 - SimpleClrHost\\HostControl\\Debug\\HostControl.dll" named_guids

int _tmain(int argc, _TCHAR* argv[])
{
	CoInitializeEx(NULL, COINIT_MULTITHREADED);

	{
	ICLRRuntimeHost* pRuntimeHost;
	HRESULT hr = CorBindToRuntimeEx(L"v2.0.50727", NULL, 0, CLSID_CLRRuntimeHost, IID_ICLRRuntimeHost, (LPVOID*)&pRuntimeHost);

	HostControlLib::ISimpleHostControlPtr pHostControl(HostControlLib::CLSID_SimpleHostControl);
	IHostControl* pRealHostControl;
	pHostControl->QueryInterface(IID_IHostControl, (void**)&pRealHostControl);
	pRuntimeHost->SetHostControl(pRealHostControl);

	pRuntimeHost->Start();

	DWORD retVal;
	hr = pRuntimeHost->ExecuteInDefaultAppDomain(L"D:\\Users\\Sasha\\Documents\\Courses\\NET Debugging\\Code\\1 - SimpleClrHost\\TestAssembly\\bin\\Debug\\TestAssembly.dll",
		L"TestAssembly.TestClass", L"MyFunc", L"Hello World!", &retVal);

	pRuntimeHost->Stop();
	}

	CoUninitialize();

	return 0;
}


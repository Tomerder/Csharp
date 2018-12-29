//
//Non-paged CLR Host
//
//	By	Sasha Goldshtein - http://blogs.microsoft.co.il/blogs/sasha
//	and Alon Fliess		 - http://blogs.microsoft.co.il/blogs/alon
//
//All rights reserved (2008).  When incorporating any significant portion of the
//code into your own project, you must retain this copyright notice.
//

#include "StdAfx.h"

#include "HostControl.h"
#include "HostMalloc.h"

SIZE_T HostControl::EffectiveMinWorkingSetSize() const
{
	return m_dwEffectiveMinWorkingSetSize;
}

LPCWSTR HostControl::EffectiveMinWorkingSetSizeString() const
{
	static wchar_t pszMinWsSize[200];
	wsprintf(pszMinWsSize, L"%Iu", EffectiveMinWorkingSetSize());
	return pszMinWsSize;
}

HostControl::HostControl() : m_cRef(0), m_pMemoryCallback(NULL), m_dwEffectiveMinWorkingSetSize(0)
{
	MEMORYSTATUSEX memoryStatus;
	memoryStatus.dwLength = sizeof(memoryStatus);
	if (!GlobalMemoryStatusEx(&memoryStatus))
	{
		wprintf(L"Failed to call GlobalMemoryStatusEx: %d\n", GetLastError());
		ExitProcess(1);
	}

#if defined(_WIN64)
	SIZE_T sizeToAsk = min(memoryStatus.ullTotalPhys, memoryStatus.ullTotalVirtual);
#else
#pragma warning (push)
#pragma warning (disable:4244)
	SIZE_T sizeToAsk = min(0xFFFFFFFF, memoryStatus.ullTotalPhys);
#pragma warning (pop)
#endif
	sizeToAsk -= 32*1024*1024;

	HMODULE hKernel32 = ::GetModuleHandle(L"kernel32.dll");
	PFN_SetProcessWorkingSetSizeEx pfnSetProcessWorkingSetSizeEx = 
		(PFN_SetProcessWorkingSetSizeEx) ::GetProcAddress(hKernel32, "SetProcessWorkingSetSizeEx");
	if (pfnSetProcessWorkingSetSizeEx != NULL)
	{
		wprintf(L"SetProcessWorkingSetSizeEx is present\n");

		DWORD dwQuotaFlags = QUOTA_LIMITS_HARDWS_MIN_ENABLE|QUOTA_LIMITS_HARDWS_MAX_DISABLE;
		while (!pfnSetProcessWorkingSetSizeEx(::GetCurrentProcess(), sizeToAsk, sizeToAsk, dwQuotaFlags))
		{
			//Ensure we don't overflow:
			if (sizeToAsk <= WorkingSetLimitSizeDecrement)
			{
				wprintf(L"Failed to set working set size limits for host.\n");
				break;
			}

			if (GetLastError() == ERROR_INVALID_PARAMETER)
			{
				//MIN_ENABLE and MAX_DISABLE are not support, revert to:
				dwQuotaFlags = QUOTA_LIMITS_HARDWS_MIN_DISABLE|QUOTA_LIMITS_HARDWS_MAX_DISABLE;
				//and retry:
				continue;
			}
			if (GetLastError() == ERROR_NO_SYSTEM_RESOURCES)
			{
				//Need to decrease min request:
				sizeToAsk -= WorkingSetLimitSizeDecrement;
				continue;
			}

			wprintf(L"Failed to set working set size limit to %Iu bytes: %d\n", sizeToAsk, GetLastError());
			break;
		}
	}
	else
	{
		while (!SetProcessWorkingSetSize(::GetCurrentProcess(), sizeToAsk, sizeToAsk))
		{
			//Ensure we don't overflow:
			if (sizeToAsk <= WorkingSetLimitSizeDecrement)
			{
				wprintf(L"Failed to set working set size limits for host.\n");
				break;
			}

			if (GetLastError() == ERROR_NO_SYSTEM_RESOURCES)
			{
				//Need to decrease min request:
				sizeToAsk -= WorkingSetLimitSizeDecrement;
				continue;
			}
							
			wprintf(L"Failed to set working set size limit to %Iu bytes: %d\n", sizeToAsk, GetLastError());
			break;
		}
	}
	wprintf(L"Successfully set working set size limit to %Iu bytes\n", sizeToAsk);
	m_dwEffectiveMinWorkingSetSize = sizeToAsk;
}


ULONG __stdcall HostControl::AddRef()
{
	return InterlockedIncrement(&m_cRef);
}

ULONG __stdcall HostControl::Release()
{
	return InterlockedDecrement(&m_cRef);
}

HRESULT __stdcall HostControl::QueryInterface(REFIID riid, void** ppvObject)
{
	if (riid == IID_IUnknown)
	{
		*ppvObject = static_cast<IUnknown*>(static_cast<IHostControl*>(this));
		return S_OK;
	}
	if (riid == IID_IHostControl)
	{
		*ppvObject = static_cast<IHostControl*>(this);
		return S_OK;
	}
	if (riid == IID_IHostMemoryManager)	//This is also the memory manager
	{
		*ppvObject = static_cast<IHostMemoryManager*>(this);
		return S_OK;
	}

	*ppvObject = NULL;
	return E_NOINTERFACE;
}

HRESULT __stdcall HostControl::GetHostManager(REFIID riid, void** ppObject)
{
	if (riid == IID_IHostMemoryManager)
	{
		*ppObject = static_cast<IHostMemoryManager*>(this);
		return S_OK;
	}

	*ppObject = NULL;
	return E_NOINTERFACE;
}

HRESULT __stdcall HostControl::SetAppDomainManager(DWORD dwAppDomainID, IUnknown* pUnkAppDomainManager)
{
	return S_OK;
}

HRESULT __stdcall HostControl::AcquiredVirtualAddressSpace(LPVOID startAddress, SIZE_T size)
{
	wprintf(L"HostControl::AcquiredVirtualAddressSpace: Acquired %Iu bytes from address 0x%IX\n", size, startAddress);

	return S_OK;
}

HRESULT __stdcall HostControl::ReleasedVirtualAddressSpace(LPVOID startAddress)
{
	wprintf(L"HostControl::ReleasedVirtualAddressSpace: Released address 0x%IX\n", startAddress);

	return S_OK;
}

HRESULT __stdcall HostControl::NeedsVirtualAddressSpace(LPVOID startAddress, SIZE_T size)
{
	wprintf(L"HostControl::NeedsVirtualAddressSpace: Going to use %Iu bytes from address 0x%IX\n", size, startAddress);

	return S_OK;
}

HRESULT __stdcall HostControl::RegisterMemoryNotificationCallback(ICLRMemoryNotificationCallback* pCallback)
{
	wprintf(L"HostControl::RegisterMemoryNotificationCallback: Registered memory notification callback\n");
	m_pMemoryCallback = pCallback;

	return S_OK;
}

HRESULT __stdcall HostControl::GetMemoryLoad(DWORD* pMemoryLoad, SIZE_T* pAvailableBytes)
{
	MEMORYSTATUSEX memoryStatus;
	memoryStatus.dwLength = sizeof(memoryStatus);
	if (!GlobalMemoryStatusEx(&memoryStatus))
	{
		wprintf(L"HostControl::GetMemoryLoad: Failed to call GlobalMemoryStatusEx: %d\n", GetLastError());
		return E_FAIL;
	}

	*pMemoryLoad = memoryStatus.dwMemoryLoad;
#if defined(_WIN64)
	*pAvailableBytes = memoryStatus.ullAvailPhys;
#else
#pragma warning (push)
#pragma warning (disable:4244)
	*pAvailableBytes = min(0xFFFFFFFF, memoryStatus.ullAvailPhys);
#pragma warning (pop)
#endif

	wprintf(L"HostControl::GetMemoryLoad: GetMemoryLoad called and returned %d memory load and %Iu available bytes\n",
		*pMemoryLoad, *pAvailableBytes);

	return S_OK;
}

HRESULT __stdcall HostControl::VirtualAlloc(void* pAddress, SIZE_T dwSize, DWORD flAllocationType,
	DWORD flProtect, EMemoryCriticalLevel dwCriticalLevel, void** ppMem)
{
	*ppMem = ::VirtualAlloc(pAddress, dwSize, flAllocationType, flProtect);
	
	if (*ppMem == NULL)
	{
		wprintf(L"HostControl::VirtualAlloc: Failed to allocate %Iu bytes at 0x%IX\n", dwSize, pAddress);
		return E_OUTOFMEMORY;
	}
	
	if (flAllocationType & MEM_COMMIT)
	{
		if (::VirtualLock(*ppMem, dwSize))
		{
			wprintf(L"HostControl::VirtualAlloc: Successfully locked %Iu bytes in memory\n", dwSize);
		}
		else
		{
			wprintf(L"HostControl::VirtualAlloc: Failed to lock %Iu bytes in memory: %d\n", dwSize, GetLastError());
		}
	}

	wprintf(L"HostControl::VirtualAlloc: Successfully allocated %Iu bytes at 0x%IX\n", dwSize, pAddress);
	return S_OK;
}

HRESULT __stdcall HostControl::VirtualFree(LPVOID lpAddress, SIZE_T dwSize, DWORD dwFreeType)
{
	if (::VirtualUnlock(lpAddress, dwSize))
	{
		wprintf(L"HostControl::VirtualFree: Successfully unlocked %Iu bytes in memory\n", dwSize);
	}
	else
	{
		wprintf(L"HostControl::VirtualFree: Failed to unlock %Iu bytes in memory\n", dwSize);
	}

	BOOL bRes = ::VirtualFree(lpAddress, dwSize, dwFreeType);

	if (!bRes)
	{
		wprintf(L"HostControl::VirtualFree: Failed to free %Iu bytes at 0x%IX\n", dwSize, lpAddress);
		return E_FAIL;
	}
	wprintf(L"HostControl::VirtualFree: SirtualFree successfully freed %Iu bytes at 0x%IX\n", dwSize, lpAddress);
	return S_OK;
}

HRESULT __stdcall HostControl::VirtualProtect(void* lpAddress, SIZE_T dwSize,
	DWORD flNewProtect, DWORD* pflOldProtect)
{
	BOOL bRes = ::VirtualProtect(lpAddress, dwSize, flNewProtect, pflOldProtect);
	if (!bRes)
	{
		wprintf(L"HostControl::VirtualProtect: Failed to protect %Iu bytes at 0x%IX\n", dwSize, lpAddress);
		return E_FAIL;
	}
	wprintf(L"HostControl::VirtualProtect: Successfully protected %Iu bytes at 0x%IX\n", dwSize, lpAddress);
	return S_OK;
}

HRESULT __stdcall HostControl::VirtualQuery(void* lpAddress, void* lpBuffer, SIZE_T dwLength, SIZE_T* pResult)
{
	*pResult = ::VirtualQuery(lpAddress, (PMEMORY_BASIC_INFORMATION)lpBuffer, dwLength);
	wprintf(L"HostControl::VirtualQuery: Called for 0x%IX\n", lpAddress);
	return S_OK;
}

HRESULT __stdcall HostControl::CreateMalloc(DWORD dwMallocType, IHostMalloc** ppMalloc)
{
	wprintf(L"HostControl::CreateMalloc: Asked for an allocator of type %d\n", dwMallocType);
	*ppMalloc = new HostMalloc((MALLOC_TYPE)dwMallocType);
	return S_OK;
}
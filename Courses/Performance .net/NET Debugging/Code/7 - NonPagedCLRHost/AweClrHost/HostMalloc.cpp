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

#include "HostMalloc.h"

HostMalloc::HostMalloc(MALLOC_TYPE type)
{
	m_dwAllocFlags = 0;

	DWORD dwHeapFlags = 0;
	if (type & MALLOC_EXECUTABLE)
	{
		dwHeapFlags |= HEAP_CREATE_ENABLE_EXECUTE;
	}
	if ((type & MALLOC_THREADSAFE) == 0)
	{
		dwHeapFlags |= HEAP_NO_SERIALIZE;
		m_dwAllocFlags |= HEAP_NO_SERIALIZE;
	}
	m_hMallocHeap = ::HeapCreate(dwHeapFlags, 0, 0);
}

ULONG __stdcall HostMalloc::AddRef()
{
	return InterlockedIncrement(&m_cRef);
}

ULONG __stdcall HostMalloc::Release()
{
	ULONG cRef = InterlockedDecrement(&m_cRef);
	if (cRef == 0)
	{
		delete this;
	}
	return cRef;
}

HRESULT __stdcall HostMalloc::QueryInterface(REFIID riid, void** ppvObject)
{
	if (riid == IID_IUnknown)
	{
		*ppvObject = static_cast<IUnknown*>(this);
		return S_OK;
	}
	if (riid == IID_IHostMalloc)
	{
		*ppvObject = static_cast<IHostMalloc*>(this);
		return S_OK;
	}

	*ppvObject = NULL;
	return E_NOINTERFACE;
}

HRESULT __stdcall HostMalloc::Alloc(SIZE_T cbSize, EMemoryCriticalLevel dwCriticalLevel, void** ppMem)
{
	*ppMem = ::HeapAlloc(m_hMallocHeap, m_dwAllocFlags, cbSize);
	wprintf(L"IHostMalloc::Alloc called for %d bytes and returned 0x%p\n", cbSize, *ppMem);
	if (*ppMem == NULL)
	{
		return E_OUTOFMEMORY;
	}
	return S_OK;
}

HRESULT __stdcall HostMalloc::DebugAlloc(SIZE_T cbSize, EMemoryCriticalLevel dwCriticalLevel,
	char* pszFileName, int iLineNo, void** ppMem)
{
	wprintf(L"IHostMalloc::DebugAlloc called from file %s line %d\n", pszFileName, iLineNo);
	return Alloc(cbSize, dwCriticalLevel, ppMem);
}

HRESULT __stdcall HostMalloc::Free(void* pMem)
{
	wprintf(L"IHostMalloc::Free called for 0x%p\n", pMem);
	if (FALSE == ::HeapFree(m_hMallocHeap, 0, pMem))
	{
		return E_FAIL;
	}
	return S_OK;
}
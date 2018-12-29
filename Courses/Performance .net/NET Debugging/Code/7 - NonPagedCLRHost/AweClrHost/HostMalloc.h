//
//Non-paged CLR Host
//
//	By	Sasha Goldshtein - http://blogs.microsoft.co.il/blogs/sasha
//	and Alon Fliess		 - http://blogs.microsoft.co.il/blogs/alon
//
//All rights reserved (2008).  When incorporating any significant portion of the
//code into your own project, you must retain this copyright notice.
//

class HostMalloc : public IHostMalloc
{
	LONG m_cRef;
	HANDLE m_hMallocHeap;
	DWORD m_dwAllocFlags;
public:
	HostMalloc(MALLOC_TYPE type);
	
	//IUnknown
	ULONG __stdcall AddRef();
	ULONG __stdcall Release();
	HRESULT __stdcall QueryInterface(REFIID riid, void** ppvObject);

	//IHostMalloc
	HRESULT __stdcall Alloc(SIZE_T cbSize, EMemoryCriticalLevel dwCriticalLevel, void** ppMem);
	HRESULT __stdcall DebugAlloc(SIZE_T cbSize, EMemoryCriticalLevel dwCriticalLevel,
		char* pszFileName, int iLineNo, void** ppMem);
	HRESULT __stdcall Free(void* pMem);
};
//
//Non-paged CLR Host
//
//	By	Sasha Goldshtein - http://blogs.microsoft.co.il/blogs/sasha
//	and Alon Fliess		 - http://blogs.microsoft.co.il/blogs/alon
//
//All rights reserved (2008).  When incorporating any significant portion of the
//code into your own project, you must retain this copyright notice.
//

class HostControl : public IHostControl, public IHostMemoryManager
{
	LONG m_cRef;
	ICLRMemoryNotificationCallback* m_pMemoryCallback;
	static const int WorkingSetLimitSizeDecrement = 16*1024*1024;
	SIZE_T m_dwEffectiveMinWorkingSetSize;
	typedef BOOL (WINAPI *PFN_SetProcessWorkingSetSizeEx)(HANDLE, SIZE_T, SIZE_T, DWORD);
public:
	HostControl();

	SIZE_T EffectiveMinWorkingSetSize() const;
	LPCWSTR EffectiveMinWorkingSetSizeString() const;

	//IUnknown
	ULONG __stdcall AddRef();
	ULONG __stdcall Release();
	HRESULT __stdcall QueryInterface(REFIID riid, void** ppvObject);

	//IHostControl
	HRESULT __stdcall GetHostManager(REFIID riid, void** ppObject);
	HRESULT __stdcall SetAppDomainManager(DWORD dwAppDomainID, IUnknown* pUnkAppDomainManager);

	//IHostMemoryManager
	HRESULT __stdcall AcquiredVirtualAddressSpace(LPVOID startAddress, SIZE_T size);
	HRESULT __stdcall ReleasedVirtualAddressSpace(LPVOID startAddress);
	HRESULT __stdcall NeedsVirtualAddressSpace(LPVOID startAddress, SIZE_T size);
	HRESULT __stdcall RegisterMemoryNotificationCallback(ICLRMemoryNotificationCallback* pCallback);
	HRESULT __stdcall GetMemoryLoad(DWORD* pMemoryLoad, SIZE_T* pAvailableBytes);
	HRESULT __stdcall VirtualAlloc(void* pAddress, SIZE_T dwSize, DWORD flAllocationType,
		DWORD flProtect, EMemoryCriticalLevel dwCriticalLevel, void** ppMem);
	HRESULT __stdcall VirtualFree(LPVOID lpAddress, SIZE_T dwSize, DWORD dwFreeType);
	HRESULT __stdcall VirtualProtect(void* lpAddress, SIZE_T dwSize,
		DWORD flNewProtect, DWORD* pflOldProtect);
	HRESULT __stdcall VirtualQuery(void* lpAddress, void* lpBuffer, SIZE_T dwLength, SIZE_T* pResult);
	HRESULT __stdcall CreateMalloc(DWORD dwMallocType, IHostMalloc** ppMalloc);
};

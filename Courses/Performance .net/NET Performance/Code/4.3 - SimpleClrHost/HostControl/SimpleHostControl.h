// SimpleHostControl.h : Declaration of the CSimpleHostControl

#pragma once
#include "resource.h"       // main symbols

#include "HostControl.h"

#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

#include <mscoree.h>

// CSimpleHostControl

class ATL_NO_VTABLE CSimpleHostControl :
	public CComObjectRootEx<CComMultiThreadModel>,
	public CComCoClass<CSimpleHostControl, &CLSID_SimpleHostControl>,
	public IDispatchImpl<ISimpleHostControl, &IID_ISimpleHostControl, &LIBID_HostControlLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
	public IHostGCManager,
	public IHostControl
{
public:
	CSimpleHostControl()
	{
	}

	DECLARE_REGISTRY_RESOURCEID(IDR_SIMPLEHOSTCONTROL)


	BEGIN_COM_MAP(CSimpleHostControl)
		COM_INTERFACE_ENTRY(ISimpleHostControl)
		COM_INTERFACE_ENTRY(IDispatch)
		COM_INTERFACE_ENTRY(IHostGCManager)
		COM_INTERFACE_ENTRY(IHostControl)
	END_COM_MAP()



	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:

	// IHostGCManager Methods
public:
	STDMETHOD(ThreadIsBlockingForSuspension)()
	{
		return S_OK;
	}
	STDMETHOD(SuspensionStarting)()
	{
		wprintf(L"GC is starting\n");
		return S_OK;
	}
	STDMETHOD(SuspensionEnding)(unsigned long Generation)
	{
		wprintf(L"GC is ending for gen %d\n", Generation);
		return S_OK;
	}

	// IHostControl Methods
public:
	STDMETHOD(GetHostManager)(REFIID riid, void * * ppObject)
	{
		if (riid == IID_IHostGCManager)
		{
			return QueryInterface(IID_IHostGCManager, ppObject);
		}
		*ppObject = NULL;
		return E_NOINTERFACE;
	}
	STDMETHOD(SetAppDomainManager)(unsigned long dwAppDomainID, LPUNKNOWN pUnkAppDomainManager)
	{
		return S_OK;
	}
};

OBJECT_ENTRY_AUTO(__uuidof(SimpleHostControl), CSimpleHostControl)



/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0550 */
/* at Tue Sep 22 21:34:42 2009
 */
/* Compiler settings for .\HostControl.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 7.00.0550 
    protocol : dce , ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 440
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __HostControl_h__
#define __HostControl_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ISimpleHostControl_FWD_DEFINED__
#define __ISimpleHostControl_FWD_DEFINED__
typedef interface ISimpleHostControl ISimpleHostControl;
#endif 	/* __ISimpleHostControl_FWD_DEFINED__ */


#ifndef __SimpleHostControl_FWD_DEFINED__
#define __SimpleHostControl_FWD_DEFINED__

#ifdef __cplusplus
typedef class SimpleHostControl SimpleHostControl;
#else
typedef struct SimpleHostControl SimpleHostControl;
#endif /* __cplusplus */

#endif 	/* __SimpleHostControl_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __ISimpleHostControl_INTERFACE_DEFINED__
#define __ISimpleHostControl_INTERFACE_DEFINED__

/* interface ISimpleHostControl */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ISimpleHostControl;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("25909038-2C1D-4C2A-92CF-D512F8D31D3F")
    ISimpleHostControl : public IDispatch
    {
    public:
    };
    
#else 	/* C style interface */

    typedef struct ISimpleHostControlVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISimpleHostControl * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISimpleHostControl * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISimpleHostControl * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ISimpleHostControl * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ISimpleHostControl * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ISimpleHostControl * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ISimpleHostControl * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        END_INTERFACE
    } ISimpleHostControlVtbl;

    interface ISimpleHostControl
    {
        CONST_VTBL struct ISimpleHostControlVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISimpleHostControl_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISimpleHostControl_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISimpleHostControl_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISimpleHostControl_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define ISimpleHostControl_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define ISimpleHostControl_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define ISimpleHostControl_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISimpleHostControl_INTERFACE_DEFINED__ */



#ifndef __HostControlLib_LIBRARY_DEFINED__
#define __HostControlLib_LIBRARY_DEFINED__

/* library HostControlLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_HostControlLib;

EXTERN_C const CLSID CLSID_SimpleHostControl;

#ifdef __cplusplus

class DECLSPEC_UUID("43BDF965-5383-44E8-8F09-EEF75192170A")
SimpleHostControl;
#endif
#endif /* __HostControlLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif



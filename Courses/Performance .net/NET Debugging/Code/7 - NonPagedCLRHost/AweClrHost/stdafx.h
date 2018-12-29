//
//Non-paged CLR Host
//
//	By	Sasha Goldshtein - http://blogs.microsoft.co.il/blogs/sasha
//	and Alon Fliess		 - http://blogs.microsoft.co.il/blogs/alon
//
//All rights reserved (2008).  When incorporating any significant portion of the
//code into your own project, you must retain this copyright notice.
//

#pragma once

#include "targetver.h"

#include <stdio.h>
#include <windows.h>
#include <mscoree.h>

#define NO_TRACE

#ifdef NO_TRACE
#define wprintf DoNothing_wprintf
int DoNothing_wprintf(const wchar_t* pszFormat, ...);
#endif
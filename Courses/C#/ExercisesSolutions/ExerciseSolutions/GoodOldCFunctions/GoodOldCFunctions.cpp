// GoodOldCFunctions.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "GoodOldCFunctions.h"


extern "C" void StripWhiteSpaceAndPunctuation(char* text, int* nWhitespace, int* nPunctuation)
	{
	    *nWhitespace = 0;
	    *nPunctuation = 0;
	    char* p = text;
	    do {
	        if (isspace(*text)) {
	            ++*nWhitespace;
	        }
	        else if (ispunct(*text)) {
	            ++*nPunctuation;
	        }
	        else {
	            *p++ = *text;
	        }
	    } while (*text++);
	}

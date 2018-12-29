// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the GOODOLDCFUNCTIONS_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// GOODOLDCFUNCTIONS_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef GOODOLDCFUNCTIONS_EXPORTS
#define GOODOLDCFUNCTIONS_API __declspec(dllexport)
#else
#define GOODOLDCFUNCTIONS_API __declspec(dllimport)
#endif

extern "C" GOODOLDCFUNCTIONS_API void StripWhiteSpaceAndPunctuation(char* text, int* nWhitespace, int* nPunctuation);
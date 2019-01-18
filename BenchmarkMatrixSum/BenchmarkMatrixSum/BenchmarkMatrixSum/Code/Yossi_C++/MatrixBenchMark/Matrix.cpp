#include "stdafx.h"
#include "Matrix.h"
#include <stdlib.h>
//#include <string>
#include <malloc.h>
#include <fstream>
#include <iostream>

using namespace std;
//using namespace System;

typedef struct MatrixSumData
{
	int startLine;
	int endLine;
	int result;
	bool calcDone;
} MatrixSumDataStruct, *MatrixSumDataStructPtr;

typedef struct MatrixParseData
{
	int numOfLinesToParse;
	int startLine;
	bool parsingDone;
} MatrixParseDataStruct, *MatrixParseDataStructPtr;

int Matrix::element[MATRIX_SIZE][MATRIX_SIZE];
char Matrix::stringToParse[MATRIX_SIZE][30000] = { 0 };
Matrix:: Matrix(void)
{
	// init matrix to random values
	for (int i = 0; i < MATRIX_SIZE; i++)
	{
		for (int j = 0; j < MATRIX_SIZE; j++)
		{
			element[i][j] = rand();
		}
	}	
}

int Matrix::SumMatrix(int numOfThreads)
{
	int sum = 0;
	HANDLE* hThreadArray;
	MatrixSumDataStructPtr pThreadDataArray;
	DWORD* dwThreadIdArray;
	bool allThreadsDone = false;
	int numOfThreadsDone = 0;

	hThreadArray = (HANDLE*)malloc(sizeof(HANDLE) * numOfThreads);
	dwThreadIdArray = (DWORD*)malloc(sizeof(DWORD) * numOfThreads);
	pThreadDataArray = (MatrixSumDataStructPtr)HeapAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY,
		sizeof(MatrixSumDataStruct) * numOfThreads);
	
	for (int i = 0; i < numOfThreads; i++)
	{
		pThreadDataArray[i].calcDone = false;
		pThreadDataArray[i].startLine = MATRIX_SIZE / numOfThreads * i;
		pThreadDataArray[i].endLine = pThreadDataArray[i].startLine + MATRIX_SIZE / numOfThreads - 1;
		hThreadArray[i] = CreateThread(
			NULL,                   // default security attributes
			0,                      // use default stack size  
			(LPTHREAD_START_ROUTINE)SumSubMatrix,       // thread function name
			(LPVOID)&(pThreadDataArray[i]),          // argument to thread function 
			0,                      // use default creation flags 
			&dwThreadIdArray[i]);   // returns the thread identifier 
	}
	while (!allThreadsDone)
	{
		numOfThreadsDone = 0;
		for (int i = 0; i < numOfThreads; i++)
		{
			if (pThreadDataArray[i].calcDone)
			{
				numOfThreadsDone++;
			}
		}
		if (numOfThreadsDone == numOfThreads)
		{
			allThreadsDone = true;
		}
	}
	for (int i = 0; i < numOfThreads; i++)
	{
		sum += pThreadDataArray[i].result;
	}
	return sum;
}

DWORD WINAPI Matrix::SumSubMatrix(LPVOID lpParam)
{
	MatrixSumDataStructPtr sumData;
	int sum = 0;

	sumData = (MatrixSumDataStructPtr)lpParam;
	for (int i = sumData->startLine; i <= sumData->endLine; i++)
	{
		for (int j = 0; j < MATRIX_SIZE; j++)
		{
			sum += element[i][j];
		}
	}
	sumData->result = sum;
	sumData->calcDone = true;
	return sum;
}

Matrix::Matrix(char* fileName, int numOfThreadsToParse)
{	
	HANDLE* hThreadArray;
	MatrixParseDataStructPtr pThreadDataArray;
	DWORD* dwThreadIdArray;
	bool allThreadsDone = false;
	int numOfThreadsDone = 0;
	int numOfLines = 0;
	SYSTEMTIME st1, st2;
	
	GetSystemTime(&st1);
	ifstream myfile(fileName);
	if (myfile.is_open())
	{
		while (!myfile.eof())
		{
			myfile >> stringToParse[numOfLines];
			numOfLines++;
		}
		myfile.close();
	}
	else 
		cout << "Unable to open file";
	GetSystemTime(&st2);
	fileReadingTime = CalculateDeltaTime(st1, st2);
	
	// parse the file using some threads
	
	GetSystemTime(&st1);
	hThreadArray = (HANDLE*)malloc(sizeof(HANDLE) * numOfThreadsToParse);
	dwThreadIdArray = (DWORD*)malloc(sizeof(DWORD) * numOfThreadsToParse);
	pThreadDataArray = (MatrixParseDataStructPtr)HeapAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY,
		sizeof(MatrixParseDataStruct) * numOfThreadsToParse);

	for (int i = 0; i < numOfThreadsToParse; i++)
	{
		pThreadDataArray[i].parsingDone = false;
		pThreadDataArray[i].startLine = numOfLines / numOfThreadsToParse * i;
		pThreadDataArray[i].numOfLinesToParse = numOfLines / numOfThreadsToParse;
		hThreadArray[i] = CreateThread(
			NULL,                   // default security attributes
			0,                      // use default stack size  
			(LPTHREAD_START_ROUTINE)ParseSubMatrix,       // thread function name
			(LPVOID)&(pThreadDataArray[i]),          // argument to thread function 
			0,                      // use default creation flags 
			&dwThreadIdArray[i]);   // returns the thread identifier 
	}
	while (!allThreadsDone)
	{
		numOfThreadsDone = 0;
		for (int i = 0; i < numOfThreadsToParse; i++)
		{
			if (pThreadDataArray[i].parsingDone)
			{
				numOfThreadsDone++;
			}
		}
		if (numOfThreadsDone == numOfThreadsToParse)
		{
			allThreadsDone = true;
		}
	}
	GetSystemTime(&st2);
	parsingTime = CalculateDeltaTime(st1, st2);
	return;
}

DWORD WINAPI Matrix::ParseSubMatrix(LPVOID lpParam)
{
	MatrixParseDataStructPtr parseData;
	int numOfLinesToParse, line, column;
	char *token, *nextToken = NULL;
	bool firstTokenInLine;

	parseData = (MatrixParseDataStructPtr)lpParam;
	line = parseData->startLine;
	column = 0;
	for (int line = parseData->startLine; line <= parseData->numOfLinesToParse + parseData->startLine; line++)
	{
		firstTokenInLine = true;
		column = 0;
		do
		{
			if (true == firstTokenInLine)
			{
				token = strtok_s(stringToParse[line], ",", &nextToken);
				firstTokenInLine = false;
			}
			else
			{
				token = strtok_s(NULL, ",", &nextToken);
			}
			if (token != NULL)
			{
				element[line][column] = atof(token);
				if (column < MATRIX_SIZE - 1)
				{
					column++;
				}
			}
		} while (token != NULL);
	}
	parseData->parsingDone = true;
	return 1;

}

int CalculateDeltaTime(SYSTEMTIME time1, SYSTEMTIME time2)
{
	if (time2.wHour > time1.wHour)
	{
		time2.wMinute += (time2.wHour - time1.wHour) * 60;
	}
	if (time2.wMinute > time1.wMinute)
	{
		time2.wSecond += (time2.wMinute - time1.wMinute) * 60;
	}
	if (time2.wSecond > time1.wSecond)
	{
		time2.wMilliseconds += (time2.wSecond - time1.wSecond) * 1000;
	}

	return time2.wMilliseconds - time1.wMilliseconds;
}
#include <windows.h>
//#include <string>
//using namespace std;
#define MATRIX_SIZE 5000



class Matrix
{
public:
	static int element[MATRIX_SIZE][MATRIX_SIZE];
	static char stringToParse[MATRIX_SIZE][30000];

	Matrix(void);
	Matrix(char *fileName, int numOfThreadsToParse);
	int SumMatrix(int numOfThreads);
	int parsingTime;
	int fileReadingTime;
private:

	//int SumSubMatrix(int startLine, int endLine);
	static DWORD WINAPI SumSubMatrix(LPVOID lpParam);
	static DWORD WINAPI ParseSubMatrix(LPVOID lpParam);
}; 

int CalculateDeltaTime(SYSTEMTIME time1, SYSTEMTIME time2);
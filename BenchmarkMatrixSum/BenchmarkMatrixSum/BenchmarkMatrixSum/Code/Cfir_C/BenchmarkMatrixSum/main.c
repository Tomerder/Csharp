#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include <inttypes.h>
#include <time.h>
#include <immintrin.h>
#include <omp.h>

#define MATRIX_SIZE			5000
#define MATRIX_LINE_LENGTH	32768

#define TIME_MEASURE_START				\
			{							\
				clock_t	tStart = 0u;	\
				clock_t	tEnd = 0u;		\
				tStart = clock();
#define TIME_MEASURE_END(s)				\
				tEnd = clock();			\
				printf("%s: %fsecs.\n", \
					s,					\
					(float)(tEnd - tStart) / CLOCKS_PER_SEC);	\
			}


int matrixReadFile(char *sFileName, char *sFileData)
{
	FILE	*pfMatrix	= NULL;	
	
	pfMatrix = fopen(sFileName, "r");

	fread(sFileData, 1, 119454999, pfMatrix);

	fclose(pfMatrix);

	return 0;
}

int matrixParseFile(char *sFileData, int16_t *pMatrix)
{
	int		i = 0, j = 0;
	char	*sLine = NULL;
	char	*sToken = NULL;

#pragma omp parallel for private(sLine, j, sToken)
	for (i = 0; i < 5000; i++)
	{
		sLine = &sFileData[i * 23891];

		sToken = strtok(sLine, ",");
		for (j = 0; j < 4999; j++)
		{
			pMatrix[j + (i * 5000)] = (int16_t)atoi(sToken);
			sToken = strtok(NULL, ",");
		}
		pMatrix[j + (i * 5000)] = (int16_t)atoi(sToken);
	}

	return 0;
}

uint64_t matrixSum(int16_t *pMatrix)
{
	int32_t	 i		= 0u;
	uint64_t uiSum	= 0llu;

#pragma omp parallel for reduction(+:uiSum)
	for (i = 0; i < MATRIX_SIZE * MATRIX_SIZE / 2; i += 16)
	{
		__m256i reg16ints1 = _mm256_loadu_si256(&pMatrix[i]);
		__m256i reg16ints2 = _mm256_loadu_si256(&pMatrix[i+(MATRIX_SIZE * MATRIX_SIZE / 2)]);
		__m256i reg16intsRes = _mm256_adds_epu16(reg16ints1, reg16ints2);
		_mm256_storeu_si256(&pMatrix[i], reg16intsRes);
		uiSum +=	pMatrix[i] +
					pMatrix[i + 1] +
					pMatrix[i + 2] +
					pMatrix[i + 3] +
					pMatrix[i + 4] +
					pMatrix[i + 5] +
					pMatrix[i + 6] +
					pMatrix[i + 7] +
					pMatrix[i + 8] +
					pMatrix[i + 9] +
					pMatrix[i + 10] +
					pMatrix[i + 11] +
					pMatrix[i + 12] +
					pMatrix[i + 13] +
					pMatrix[i + 14] +
					pMatrix[i + 15];
	}

	return uiSum;
}

int main(int argc, char *argv[])
{
	int16_t *pi16Matrix		= NULL;
	uint64_t ui64Sum		= 0llu;
	char	*sFileData = NULL;

	printf("\n___---~~~ OpenMP + SIMD implementation ~~~---___\n");

	TIME_MEASURE_START
	sFileData = (char *)malloc(119455000);
	matrixReadFile(argv[1], sFileData);
	TIME_MEASURE_END("\tRead file")

	TIME_MEASURE_START
	pi16Matrix = (int16_t *)_aligned_malloc(MATRIX_SIZE * MATRIX_SIZE * sizeof(int16_t), 16);
	matrixParseFile(sFileData, pi16Matrix);
	TIME_MEASURE_END("\tParse file")

	TIME_MEASURE_START
	ui64Sum = matrixSum(pi16Matrix);
	TIME_MEASURE_END("\tMatrix sum")
	
	printf("\tSum = %lld\n", ui64Sum);

	free(sFileData);
	_aligned_free(pi16Matrix);

	printf("\n___---~~~ FLEX implementation ~~~---___\n");

	TIME_MEASURE_START
	lexMatrix(argv[1]);
	TIME_MEASURE_END("\tFLEX implementation")

	return 0;
}
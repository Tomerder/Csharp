%{
#define YY_NO_UNISTD_H

#include <stdio.h>
#include <inttypes.h>
#include <string.h>

uint64_t uiSum	= 0llu;
%}

DIGIT	[0-9]

%%

{DIGIT}+	{
				uiSum += (int16_t)atoi(yytext);
            }
","|"\r"|"\n"	/* eat char */

%%

int yywrap()
{
	return 1;
}

int lexMatrix(char *sFileName)
{
	yyin = fopen(sFileName, "r");
	
	yylex();
	
	printf("Sum = %lld\n", uiSum);
	
	return 0;
}

/* program to show the use of 'break' and 'continue' statements in C:
   This program takes int input and prints the number (for 100 times or untill gets an odd number).
   Also ignore the input which is divisible by 4 */

#include<stdio.h>
int main()
{
	int num;
	for(int i = 0; i < 100; i++)
	{
		scanf("%d",&num);

		if(num % 2 == 1) break;

		if(num % 4 == 0) continue;
		else printf("Your number = %d\n",num);
	}
	return 0;
}
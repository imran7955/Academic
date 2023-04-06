#include<stdio.h>
int main()
{
	int num,rev;
	scanf("%d",&num);
	rev = 0;
	while(num != 0)
	{
		int last_digit = num % 10;
		rev = rev * 10 + last_digit;
		num /= 10;
	}
	num = rev;
	printf("%d\n",num);
	return 0;
}
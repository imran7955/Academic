/* Take an int input and print the largest digit present in it (in words) */

#include<stdio.h>
int main()
{
	char map[10][6] = {"Zero","One","Two","Three","Four","Five","Six","Seven","Eight","Nine"};
	int num,largest = 0;

	printf("Enter an Integer: ");
	scanf("%d",&num);

	while(num != 0)
	{
		int last_digit = num % 10;
		if(last_digit > largest) largest = last_digit;
		num /= 10;
	}
	printf("%s is the largest digit.\n",map[largest]);
	
	return 0;
}
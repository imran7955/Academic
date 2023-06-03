#include<stdio.h>
int main()
{
	int n; 
	scanf("%d",&n);
	int mat[n][n];
	for(int i = 0; i < n; i++)
		for(int j = 0; j < n; j++)
			scanf("%d",&mat[i][j]);
	
	int sum = 0;
	int l = 0,r = n-1;
	for(int i = 0; i < n; i++)
	{
		sum += mat[i][l] + mat[i][r];
		l++,r--;
	}
	
	printf("Sum = %d\n",sum);

	
	return 0;
}
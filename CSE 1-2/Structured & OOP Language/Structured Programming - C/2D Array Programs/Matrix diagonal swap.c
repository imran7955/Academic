#include<stdio.h>
int main()
{
	int n; 
	scanf("%d",&n);
	int mat[n][n];
	for(int i = 0; i < n; i++)
		for(int j = 0; j < n; j++)
			scanf("%d",&mat[i][j]);
	int l = 0,r = n-1;
	for(int i = 0; i < n; i++)
	{
		//swap(mat[i][l],mat[i][r]);
		int temp = mat[i][l];
		mat[i][l] = mat[i][r];
		mat[i][r] = temp;
		l++,r--;
	}

	for(int i = 0; i < n; i++)
	{
		for(int j = 0; j < n; j++)
			printf("%d ", mat[i][j]);
		printf("\n");
	}
	return 0;
}
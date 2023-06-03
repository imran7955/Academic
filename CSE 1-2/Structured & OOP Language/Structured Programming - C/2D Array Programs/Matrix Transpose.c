#include<stdio.h>
int main()
{
	int n; 
	scanf("%d",&n);
	int mat[n][n];
	for(int i = 0; i < n; i++)
		for(int j = 0; j < n; j++)
			scanf("%d",&mat[i][j]);
	for(int i = 0; i < n; i++)
	{
		for(int j = i; j < n; j++)
		{
			//swap(mat[i][j],mat[j][i]);
			int temp = mat[i][j];
			mat[i][j] = mat[j][i];
			mat[j][i] = temp; 
		}
	}

	for(int i = 0; i < n; i++)
	{
		for(int j = 0; j < n; j++)
			printf("%d ", mat[i][j]);
		printf("\n");
	}
	return 0;
}
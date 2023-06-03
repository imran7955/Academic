#include<stdio.h>
int main()
{
	int m,n; 
	scanf("%d %d",&m, &n);
	int mat1[m][n];
	for(int i = 0; i < m; i++)
		for(int j = 0; j < n; j++)
			scanf("%d",&mat1[i][j]);
	int p;
	scanf("%d %d",&n,&p);
	int mat2[n][p];
	for(int i = 0; i < n; i++)
		for(int j = 0; j < p; j++)
			scanf("%d",&mat2[i][j]);

	
	int ans[m][p];
	
	for(int i = 0; i < m; i++)
	{
		for(int j = 0; j < p; j++)
		{
			ans[i][j] = 0;
			for(int k = 0; k < n; k++)
				ans[i][j] += mat1[i][k] * mat2[k][j];
		}
	}

	for(int i = 0; i < m; i++)
	{
		for(int j = 0; j < p; j++)
			printf("%d ", ans[i][j]);
		printf("\n");
	}
	return 0;
}
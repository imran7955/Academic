#include<stdio.h>
int main()
{
	int n; 
	scanf("%d",&n);
	int mat[n][n];
	for(int i = 0; i < n; i++)
		for(int j = 0; j < n; j++)
			scanf("%d",&mat[i][j]);
		
	int flag = 1;
	for(int i = 0; i < n; i++)
	{
		for(int j = i; j < n; j++)
		{
			//swap(mat[i][j],mat[j][i]);
			if(mat[i][j] != mat[j][i]) flag = 0;
		}
	}

	if(flag == 0)
		printf("Not symetric\n");
	else
		printf("symetric\n");
	return 0;
}
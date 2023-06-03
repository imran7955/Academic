#include<stdio.h>
int main()
{
	int r,c; 
	scanf("%d %d",&r,&c);
	int mat1[r][c],mat2[r][c],ans[r][c];

	for(int i = 0; i < r; i++)
		for(int j = 0; j < c; j++)
			scanf("%d",&mat1[i][j]);

	for(int i = 0; i < r; i++)
		for(int j = 0; j < c; j++)
			scanf("%d",&mat2[i][j]);

	
	for(int i = 0; i < r; i++)
	{
		for(int j = 0; j < c; j++)
			ans[i][j] = mat1[i][j] + mat2[i][j];
	}
	

	for(int i = 0; i < r; i++)
	{
		for(int j = 0; j < c; j++)
			printf("%d ", ans[i][j]);
		printf("\n");
	}
	return 0;
}
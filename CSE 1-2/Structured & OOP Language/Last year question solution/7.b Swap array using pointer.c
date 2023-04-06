#include<stdio.h>
void PrintArray(int arr[],int n)
{
	for(int i = 0; i < n; i++)
		printf("%d ",arr[i]);
	printf("\n");
}
int main()
{
	int arr1[] = {1,2,3,4,5},arr2[] = {6,7,8,5,9};
	for(int i = 0; i < 5; i++)
	{
		int temp = *(arr1+i);
		*(arr1+i) = *(arr2+i);
		*(arr2+i) = temp;
	}
	PrintArray(arr1,5);
	PrintArray(arr2,5);
	return 0;
}
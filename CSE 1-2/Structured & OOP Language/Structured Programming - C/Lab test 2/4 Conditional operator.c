#include <stdio.h>

int main()
{
    int x,y; 
    scanf("%d", &x);
    y = x > 0 == 1 ? : (x == 0 ? 0 : -1);
    printf("X = %d Y = %d\n",x,y);
    return 0;
}
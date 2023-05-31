#include <stdio.h>

int main()
{
    int x,y;
    scanf("%d %d", &x, &y);
    if(x >= 0 && y >= 0)
        printf("(%d,%d) is on the 1st Quadrant\n",x,y);
    else if(x < 0 && y >= 0)
        printf("(%d,%d) is on the 2nd Quadrant\n",x,y);
    else if(x <= 0 && y < 0)
        printf("(%d,%d) is on the 3rd Quadrant\n",x,y);
    else
        printf("(%d,%d) is on the 4th Quadrant\n",x,y);
    return 0;
}
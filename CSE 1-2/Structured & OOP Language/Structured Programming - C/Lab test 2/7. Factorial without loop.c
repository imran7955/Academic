#include <stdio.h>
void fact(int prev,int i)
{
    printf("%d! = %d\n",i,prev*i);
    if(i < 10)
        fact(prev*i,i+1);
}
int main()
{
    fact(1,1);
    return 0;
}

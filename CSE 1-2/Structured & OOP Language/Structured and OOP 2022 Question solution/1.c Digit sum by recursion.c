#include <stdio.h>
int digitSum(int n)
{
    if(n < 10)
        return n;
    else
        return (n%10) + digitSum(n/10);
}

int main() {
    int num = 1456;
    int d = digitSum(num);
    printf("%d\n",d);
    return 0;
}

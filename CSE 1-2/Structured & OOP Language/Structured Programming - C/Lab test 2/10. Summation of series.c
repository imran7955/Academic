#include <stdio.h>
#include <string.h>
int main()
{
    int n;
    double ans = 0;
    scanf("%d", &n);
    for(int i = 1; i <= n; i++)
    {
        double num = 4.0/(2*i-1);
        if(i%2) ans += num;
        else ans -= num;
    }
    printf("%lf\n",ans);
}
#include <stdio.h>

int main()
{
    float f,c;
    scanf("%f", &f);
    c = (f - 32) * 5 / 9;
    printf("%.2f degree fahrenheit = %.2f degree celsius\n",f,c);
    return 0;
}

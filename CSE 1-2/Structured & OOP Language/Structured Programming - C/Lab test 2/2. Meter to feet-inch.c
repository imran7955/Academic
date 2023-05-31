#include <stdio.h>

int main()
{
    float hm,hi,total;
    int hf;
    scanf("%f", &hm);
    total = hm * 39.37;
    hf = total / 12;
    hi = total - (hf * 12);
    printf("%.2f m = %d feet %.2f inch\n",hm,hf,hi);
    return 0;
}
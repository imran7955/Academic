#include <stdio.h>
#include <string.h>
int main()
{
    char spell[10][6] = {"Zero","One","Two","Three","Four","Five","Six","Seven","Eight","Nine"};
    char num[20];
    scanf("%s", num);
    for(int i = 0; i < strlen(num); i++)
    {
        int d = num[i] - '0';
        printf("%s ",spell[d]);
    }
    return 0;
}
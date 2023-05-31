#include <stdio.h>

int main()
{
    char c;
    scanf("%c", &c);
    if(c == 'a' || c == 'A' || c == 'e' || c == 'E' || c == 'i' || c == 'I' || c == 'o' || c == 'O' || c == 'u' || c == 'U')
        printf("Vowel\n");
    else if( (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') )
        printf("Consonant\n");
    else if(c >= '0' && c <= '9')
        printf("Digit\n");
    else
        printf("Neither\n");
    return 0;
}
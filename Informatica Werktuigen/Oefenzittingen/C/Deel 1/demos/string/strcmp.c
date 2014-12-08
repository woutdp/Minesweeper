#include <stdio.h>
#include <string.h>

int main()
{
    const char* s1 = "abc";
    const char* s2 = "def";
    printf("%i\n", strcmp(s1, s2));
    printf("%i\n", strcmp(s2, s1));
    printf("%i\n", strcmp(s1, s1));
    return 0;
}


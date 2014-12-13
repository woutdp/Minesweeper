#include <stdio.h>
#include <string.h>
#include <stdlib.h>

int main()
{
    const char* src = "Test";
    char* dest = malloc(/*?*/);;
    strcpy(dest, src);
    printf("%s\n", dest);
    return 0;
}


#include <stdio.h>
#include "main.h"
//using namespace std

int main()
{
    int result;
    int a = 3;
    int b = 2;

    result = power(a,b);
    printf("%i \n", result);
    swap(&a, &b);
    result = power(a,b);
    printf("%i \n", result);

    return 0;
}

int power(int a, int b)
{
    int result = 1;
    for (int i = 0; i < b; ++i)
    {
        result *= a;
    }

    return result;
}

void swap(int* a, int* b)
{
    int temp = *a;
    *a = *b;
    *b = temp;
}
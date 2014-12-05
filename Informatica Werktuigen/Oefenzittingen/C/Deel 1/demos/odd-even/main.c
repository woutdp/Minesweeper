#include <stdio.h>

#include "odd-even.h"

int main()
{
    int ten_is_even = is_even(10);
    printf("10 is even: %i\n", ten_is_even);

    int ten_is_odd = is_odd(10);
    printf("10 is odd: %i\n", ten_is_odd);

    return 0;
}

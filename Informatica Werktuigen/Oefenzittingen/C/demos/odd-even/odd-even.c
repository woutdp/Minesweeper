#include "odd-even.h"

int is_odd(int i)
{
    if (i == 0)
        return 0;
    else
        return is_even(i - 1);
}

int is_even(int i)
{
    if (i == 0)
        return 1;
    else
        return is_odd(i - 1);
}


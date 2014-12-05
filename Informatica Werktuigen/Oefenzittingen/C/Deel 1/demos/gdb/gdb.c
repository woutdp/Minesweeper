#include <stdlib.h>

void double_integer(int* i)
{
    *i *= 2;
}

void run()
{
    int* i = NULL;
    // TODO initialize i!
    double_integer(i);
}

int main()
{
    run();
    return 0;
}

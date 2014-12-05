#include <stdio.h>

int factorial(int n)
{
	int result = 1;

	while(n > 0)
	{
		result *= n;
        --n;
	}

	return result;
}

int main()
{
	printf("factorial(3) = %i\n", factorial(3));
	return 0;
}

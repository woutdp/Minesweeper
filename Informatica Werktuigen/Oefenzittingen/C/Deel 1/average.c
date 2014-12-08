#include <stdio.h>

double average(int a, int b)
{
	double sum = (double)a + (double)b;
	double result = sum / 2;
	return result;
}

int main()
{
	// Average of 1000 MiB and 2000 MiB, expressed in bytes. This should
	// result in in 1500 * 1024 * 1024 = 1572864000.
	int bytes1 = 1000 * 1024 * 1024;
	int bytes2 = 2000 * 1024 * 1024;

	double avgbytes = average(bytes1, bytes2);
	printf("average(%i, %i) = %f\n", bytes1, bytes2, avgbytes);

	return 0;
}

#include <stdio.h>
#include <assert.h>

#include "list.h"

int test_list()
{
	int value = 0;
	struct List *list = list_create();

	if (list_length(list) != 0) {
		printf("list_length of empty list should be zero\n");
		return 0;
	}


	// Insert value 101 and test functions
	list_insert(list, 0, 101);
	if (list_length(list) != 1) {
		printf("list_length should be 1\n");
		return 0;
	}

	if (list_get(list, 0, &value) == 0) {
		printf("Error in list_get (1)\n");
		return 0;
	}
	if (value != 101) {
		printf("list_get should return value 101\n");
		return 0;
	}


	// Insert value 202 and test functions
	list_insert(list, 0, 202);
	if (list_length(list) != 2) {
		printf("list_length should return 2\n");
		return 0;
	}

	if (list_get(list, 0, &value) == 0) {
		printf("Error in list_length (2)\n");
		return 0;
	}
	if (value != 202) {
		printf("list_get should return 202\n");
		return 0;
	}


	// Test remove function
	if (list_remove(list, 1) == 0) {
		printf("Error in list_remove\n");
		return 0;
	}
	if (list_length(list) != 1) {
		printf("list_length should return 1 (after remove)\n");
		return 0;
	}
	

	// Test pop function
	if (list_pop(list, &value) == 0) {
		printf("Error in list_pop\n");
		return 0;
	}
	if (value != 202) {
		printf("list_pop should return 202\n");
		return 0;
	}
	if (list_length(list) != 0) {
		printf("list_length should return 0 (after pop)\n");
		return 0;
	}


	// TODO: Add our own test functions (!!)

	return 1;
}


int test_dlist()
{
	// TODO: Add your own test functions (!!)

	return 1;
}


int test_stack()
{
	// TODO: Add your own test functions (!!)

	return 1;
}


int test_evaluate()
{
	int value;

	// Test 1
	if (evaluate("10", &value) == 0) {
		printf("Error evaluating string\n");
		return 0;
	}
	if (value != 10) {
		printf("Evaluate returned wrong result\n");
		return 0;
	}

	// Test 2
	if (evaluate("6 4 + 10 *", &value) == 0) {
		printf("Error evaluating string\n");
		return 0;
	}
	if (value != 100) {
		printf("Evaluate returned wrong result\n");
		return 0;
	}

	// TODO: Add more test! For example: using negative numbers, testing
	// that it returns 0 on invalid expressions, etc.

	return 1;
}


int main(int argc, char *argv[])
{
	test_list();
	test_dlist();
	test_stack();
	test_evaluate();

	return 0;
}


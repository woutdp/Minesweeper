#include <stdio.h>
#include <assert.h>

#include "list.h"

#define KNRM  "\x1B[0m"
#define KRED  "\x1B[31m"
#define KGRN  "\x1B[32m"
#define KYEL  "\x1B[33m"
#define KBLU  "\x1B[34m"
#define KMAG  "\x1B[35m"
#define KCYN  "\x1B[36m"
#define KWHT  "\x1B[37m"

int test_list()
{
    int value = 0;
    struct List *list = list_create();
    printf("===TEST LINKED LIST===\n");

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


    // Test prepend function
    list_prepend(list, 0);
    if (list->first == NULL) {
        printf("PREPEND:The first element shouldn't be zero\n");
        return 0;
    }
    list_prepend(list, 403);
    list_get(list, 0, &value);
    if (value != 403) {
        printf("PREPEND:value should be 403\n");
        return 0;
    }
    if (list_length(list) != 2) {
        printf("PREPEND:list_length should return 2\n");
        return 0;
    }
    list_pop(list, &value);
    if (value != 0) {
        printf("PREPEND:pop should return 0\n");
        return 0;
    }
    list_pop(list, &value);

    // Test insert sorted
    list_append(list, 5);
    list_append(list, 10);
    list_append(list, 15);
    list_append(list, 20);
    list_append(list, 25);
    if (list_length(list) != 5) {
        printf("LIST SORTED:list_length should return 5\n");
        return 0;
    }
    list_insert_sorted(list, 7);
    if (list_length(list) != 6) {
        printf("LIST SORTED:list_length should return 6\n");
        return 0;
    }
    list_get(list, 1, &value);
    if (value != 7) {
        printf("LIST SORTED:value should be 7\n");
        return 0;
    }
    list_insert_sorted(list, 3);
    list_get(list, 0, &value);
    if (value != 3) {
        printf("LIST SORTED:value should be 3\n");
        return 0;
    }

    // Reverse print
    printf("ORIGINAL: \t");
    list_print(list);
    printf("REVERSE: \t");
    list_print_reverse(list);

    // Test list_remove_all
    list_insert_sorted(list, 10);
    list_insert_sorted(list, 10);
    list_insert_sorted(list, 10);
    list_remove_all(list, 10);
    list_remove_all(list, 3);
    list_remove_all(list, 25);
    if (list_length(list) != 4) {
        printf("REMOVE ALL:list_length should return 4\n");
        return 0;
    }

    list_delete(list);
    return 1;
}


int test_dlist()
{
    int value = 0;
    struct DList* list = dlist_create();
    printf("===TEST DOUBLE LINKED LIST===\n");

    //APPEND
    dlist_append(list, 5);
    if (dlist_length(list) != 1) {
        printf("APPEND: length should be 1\n");
        return 0;
    }

    //GET VALUE
    dlist_append(list, 7);
    dlist_append(list, 10);
    dlist_append(list, 15);
    if(dlist_get(list, 0, &value) == 0)
    {
        printf("GET VALUE: an error occured, maybe out of range?\n");
        return 0;
    }
    if (value != 5) {
        printf("GET VALUE: value should be 5\n");
        return 0;
    }
    if(dlist_get(list, 3, &value) == 0)
    {
        printf("GET VALUE: an error occured, maybe out of range?\n");
        return 0;
    }
    if (value != 15) {
        printf("GET VALUE: value should be 15, but is %i instead\n", value);
        return 0;
    }
    if(dlist_get(list, 2, &value) == 0)
    {
        printf("GET VALUE: an error occured, maybe out of range?\n");
        return 0;
    }
    if (value != 10) {
        printf("GET VALUE: value should be 10, but is %i instead\n", value);
        return 0;
    }
    if (dlist_length(list) != 4)
    {
        printf("GET VALUE: length should be 4, but is %i instead\n", dlist_length(list));
        return 0;
    }

    //INSERT
    dlist_insert(list, -1, 100);
    if(dlist_get(list, 0, &value) == 0)
    {
        printf("INSERT: an error occured, maybe out of range?\n");
        return 0;
    }
    if (value != 100) {
        printf("INSERT: value should be 100, but is %i instead\n", value);
        return 0;
    }
    if (dlist_length(list) != 5)
    {
        printf("INSERT: length should be 5, but is %i instead\n", dlist_length(list));
        return 0;
    }
    dlist_insert(list, 2, 200);
    if(dlist_get(list, 2, &value) == 0)
    {
        printf("INSERT: an error occured, maybe out of range?\n");
        return 0;
    }
    if (value != 200) {
        printf("INSERT: value should be 200, but is %i instead\n", value);
        return 0;
    }
    if (dlist_length(list) != 6)
    {
        printf("INSERT: length should be 6, but is %i instead\n", dlist_length(list));
        return 0;
    }
    dlist_insert(list, 8, 300);
    if(dlist_get(list, 6, &value) == 0)
    {
        printf("INSERT: an error occured, maybe out of range?\n");
        return 0;
    }
    if (value != 300) {
        printf("INSERT: value should be 300, but is %i instead\n", value);
        return 0;
    }
    if (dlist_length(list) != 7)
    {
        printf("INSERT: length should be 7, but is %i instead\n", dlist_length(list));
        return 0;
    }

    //REMOVE
    if(dlist_remove(list, 13) != 0)
    {
        printf("REMOVE: This should give an error because out of bounds\n");
        return 0;
    }
    if(dlist_remove(list, -1) != 0)
    {
        printf("REMOVE: This should give an error because out of bounds\n");
        return 0;
    }
    if(dlist_remove(list, 0) != 1)
    {
        printf("REMOVE: An error occured\n");
        return 0;
    }
    if (dlist_length(list) != 6)
    {
        printf("REMOVE: length should be 6, but is %i instead\n", dlist_length(list));
        return 0;
    }
    if (dlist_get(list, 0, &value) == 0)
    {
        printf("REMOVE: an error occured\n");
        return 0;
    }
    if (value != 5) {
        printf("INSERT: value should be 5, but is %i instead\n", value);
        return 0;
    }
    if(dlist_remove(list, 0) != 1)
    {
        printf("REMOVE: An error occured\n");
        return 0;
    }
    if (dlist_length(list) != 5)
    {
        printf("REMOVE: length should be 5, but is %i instead\n", dlist_length(list));
        return 0;
    }
    if(dlist_remove(list, 1) != 1)
    {
        printf("REMOVE: An error occured\n");
        return 0;
    }
    if (dlist_length(list) != 4)
    {
        printf("REMOVE: length should be 4, but is %i instead\n", dlist_length(list));
        return 0;
    }
    if (dlist_get(list, 0, &value) == 0)
    {
        printf("REMOVE: an error occured\n");
        return 0;
    }
    if (value != 200) {
        printf("INSERT: value should be 200, but is %i instead\n", value);
        return 0;
    }
    if (dlist_get(list, 1, &value) == 0)
    {
        printf("REMOVE: an error occured\n");
        return 0;
    }
    if (value != 10) {
        printf("INSERT: value should be 10, but is %i instead\n", value);
        return 0;
    }
    if(dlist_remove(list, 3) != 1)
    {
        printf("REMOVE: An error occured\n");
        return 0;
    }
    if (dlist_get(list, 2, &value) == 0)
    {
        printf("REMOVE: an error occured\n");
        return 0;
    }
    if (value != 15) {
        printf("INSERT: value should be 15, but is %i instead\n", value);
        return 0;
    }
    if(dlist_remove(list, 0) != 1)
    {
        printf("REMOVE: An error occured\n");
        return 0;
    }
    if(dlist_remove(list, 0) != 1)
    {
        printf("REMOVE: An error occured\n");
        return 0;
    }
    if(dlist_remove(list, 0) != 1)
    {
        printf("REMOVE: An error occured\n");
        return 0;
    }

    //INSERT TEST AGAIN, EDGE CASES
    dlist_insert(list, -1, 5);
    dlist_insert(list, -1, 7);
    dlist_insert(list, 1, 9);
    dlist_remove(list, 2);
    dlist_insert(list, 2, 1);
    if (dlist_get(list, 0, &value) == 0)
    {
        printf("REMOVE: an error occured\n");
        return 0;
    }
    if (value != 7) {
        printf("INSERT: value should be 7, but is %i instead\n", value);
        return 0;
    }
    if (dlist_get(list, 1, &value) == 0)
    {
        printf("REMOVE: an error occured\n");
        return 0;
    }
    if (value != 9) {
        printf("INSERT: value should be 9, but is %i instead\n", value);
        return 0;
    }
    if (dlist_get(list, 2, &value) == 0)
    {
        printf("REMOVE: an error occured\n");
        return 0;
    }
    if (value != 1) {
        printf("INSERT: value should be 1, but is %i instead\n", value);
        return 0;
    }

    // Reverse print
    printf("ORIGINAL: \t");
    dlist_print(list);
    printf("REVERSE: \t");
    dlist_print_reverse(list);

    dlist_delete(list);
    return 1;
}


int test_stack()
{
    printf("===TEST STACK===\n");
    // TODO: Add your own test functions (!!)

    return 1;
}


int test_evaluate()
{
    printf("===TEST EVALUATE===\n");

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
    printf(KYEL);
    test_list();
    test_dlist();
    test_stack();
    //test_evaluate();

    printf(KNRM);
    return 0;
}


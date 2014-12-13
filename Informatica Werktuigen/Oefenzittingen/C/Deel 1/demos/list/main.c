#include "list.h"

#include <stdio.h>

int main()
{
    // list = []
    struct List* list = list_create();

    // list.append(...)
    list_append(list, 5);
    list_append(list, 7);
    list_append(list, 12);

    // print(list)
    list_print(list);

    int i = 1;

    // try:
    //     value = list[i]
    //     print(...)
    // except IndexError:
    //     print('Index out of range')

    int value;
    if (list_get(list, i, &value))
        printf("list[%d] = %d\n", i, value);
    else
        puts("Index out of range");

    // del list
    list_delete(list);

    return 0;
}

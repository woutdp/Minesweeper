#include <stdlib.h>
#include <stdio.h>

#include "list.h"

// Create an empty list
//
// Python: list = []
struct List* list_create()
{
    struct List* list = malloc(sizeof(struct List));
    list->first = NULL;
    return list;
}

// Append the given value to the given list
//
// Python: list.append(value)
void list_append(struct List* list, int value)
{
    // create a new ListNode to store the value in
    struct ListNode* node = malloc(sizeof(struct ListNode));
    node->value = value;
    node->next = NULL;

    // if the list is empty, make the new node the first node
    if (list->first == NULL)
        list->first = node;
    else
    {
        // find the last node and set the new node as its next node
        struct ListNode* current = list->first;

        while (current->next != NULL)
            current = current->next;

        current->next = node;
    }
}

// Return the length of the given list (i.e., the number of values in it)
//
// Python: length = len(list)
int list_length(struct List* list)
{
    int length = 0;
    struct ListNode* current = list->first;

    while (current != NULL)
    {
        length++;
        current = current->next;
    }

    return length;
}

// Get the value of the element at the given index and store it in the memory
// location pointed to by value. If the given index is out of range, 0 is
// returned, otherwise 1 is returned.
//
// Python: value = list[i]
// (An IndexError would correspond to a return value of 0)
int list_get(struct List* list, int index, int* value)
{
    if (index < 0 || index >= list_length(list))
        return 0;

    struct ListNode* current = list->first;

    for (int i = 0; i < index; i++)
        current = current->next;

    *value = current->value;
    return 1;
}

// Delete the given list
//
// Python: del list
void list_delete(struct List* list)
{
    struct ListNode* current = list->first;

    while (current != NULL)
    {
        struct ListNode* previous = current;
        current = current->next;
        free(previous);
    }

    free(list);
}

// Print a human-readable representation of the given list
//
// Python: print(list)
void list_print(struct List* list)
{
    printf("[");

    struct ListNode* current = list->first;

    while (current != NULL)
    {
        printf("%d", current->value);

        // no comma after last value
        if (current->next != NULL)
            printf(", ");

        current = current->next;
    }

    printf("]\n");
}

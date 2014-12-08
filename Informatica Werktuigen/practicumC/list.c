#include <stdlib.h>
#include <stdio.h>
#include <string.h>

#include "list.h"

// ==================== Enkelvoudige Gelinkte Lijst ====================

// Create an empty list
//
// Python: list = []
struct List* list_create()
{
    struct List* list = malloc(sizeof(struct List));
    list->first = NULL;
    return list;
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

// Get the value of the element at the given index and store it in the memory
// location pointed to by value. If the given index is out of range, 0 is
// returned, otherwise 1 is returned.
//
// Python: value = list[i]
// (An IndexError would correspond to a return value of 0)
int list_get(struct List* list, int index, int* value)
{
    struct ListNode* current = list->first;

    if (index < 0 || index >= list_length(list))
        return 0;

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

// Remove the element at the given index from the given list. If the given index
// is out of range, 0 is returned, otherwise 1 is returned.
//
// Python: del list[i]
// (An IndexError would correspond to a return value of 0)
int list_remove(struct List* list, int index)
{
    if (index < 0) return 0;

    struct ListNode* current = list->first;
    struct ListNode* previous;
    for (int i = 0; i < index; ++i)
    {
        previous = current;
        current = current->next;
        if (current == NULL)
            return 0;
    }

    previous->next = current->next;
    free(current);

    return 1;
}

// Get the value of the last element and store it in the memory location pointed
// to by value. The last element is removed from the list. If the list is emtpy,
// 0 is returned, otherwise 1 is returned.
//
// Python: value = list.pop()
// (An IndexError would correspond to a return value of 0)
int list_pop(struct List* list, int* value)
{
    struct ListNode* current = list->first;
    struct ListNode* previous = NULL;

    if (current == NULL)
        return 0;

    while(current->next != NULL)
    {
        previous = current;
        current = current->next;
    }

    *value = current->value;
    if (previous != NULL)
        previous->next = NULL;
    else
        list->first = NULL;

    free(current);

    return 1;
}

// Prepend the value to the front of the list.
//
// Python: list.insert(0, value)
void list_prepend(struct List* list, int value)
{
    struct ListNode* node = malloc(sizeof(struct ListNode));
    node->value = value;
    node->next = list->first;

    list->first = node;
}

// Insert the element before the given index in the list. A negative index
// means the element should be added to the front (prepended). An index that
// is too high means the element should be added to the back (appended).
//
// Python: list.insert(index, value)
// (Note that the behavior for negative indices differs slightly in Python)
void list_insert(struct List* list, int index, int value)
{
    // create a new ListNode to store the value in
    struct ListNode* node = malloc(sizeof(struct ListNode));
    node->value = value;
    node->next = NULL;

    //Get the first node in the list
    struct ListNode* current = list->first;

    if (current == NULL)
    {
        list->first = node;
    }
    else if (index <= 0)
    {
        node->next = current;
        list->first = node;
    }
    else
    {
        for (int i = 0; i < index; ++i)
        {
            current = current->next;
            if (current->next == NULL)
                break;
        }
        node->next = current->next;
        current->next = node;
    }
}

// Insert the value at the correct position in a sorted list. Assume that the list
// is sorted from lowest to highest (ascending). The list must remain sorted!
void list_insert_sorted(struct List* list, int value)
{
    // create a new ListNode to store the value in
    struct ListNode* node = malloc(sizeof(struct ListNode));
    node->value = value;
    node->next = NULL;

    //Get the first node in the list
    struct ListNode* current = list->first;
    struct ListNode* previous = NULL;

    while(current != NULL && value > current->value)
    {
        previous = current;
        current = current->next;
    }

    if (previous != NULL)
        previous->next = node;
    else
        list->first = node;

    node->next = current;
}

// Print the elements of the list in reverse order. For example, if the list contains
// the numbers 3, 7, and 1, it should print "[1, 7, 3]\n".
//
// Python: print(list[::-1])
void list_print_reverse(struct List* list)
{
    struct List reverseList;
    reverseList.first = NULL;

    struct ListNode* current = list->first;

    while (current != NULL)
    {
        struct ListNode* previous = current;
        current = current->next;
        list_prepend(&reverseList, previous->value);
    }

    list_print(&reverseList);
}

void list_remove_all(struct List* list, int value)
{
    struct ListNode* current = list->first;
    struct ListNode* previous = NULL;

    while (current != NULL)
    {
        if (current->value == value)
        {
            if (previous == NULL)
            {
                list->first = current->next;
                free(current);
            }
            else
            {
                previous->next = current->next;
                free(current);
                current = previous->next;
            }
        }
        else
        {
            previous = current;
            current = current->next;
        }
    }
}


// ==================== Dubbel Gelinkte Lijst ====================

// Create an empty list
//
// Python: list = []
struct DList* dlist_create()
{
    struct DList *dlist = malloc(sizeof(struct DList));
    dlist->first = NULL;
    dlist->last = NULL;
    dlist->length = 0;
    return dlist;
}

// Print a human-readable representation of the given list
//
// Python: print(list)
void dlist_print(struct DList* dlist)
{
    printf("[");

    struct DListNode* current = dlist->first;

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

// Delete the given list
//
// Python: del list
void dlist_delete(struct DList *dlist)
{
    struct DListNode* curr = dlist->first;

    while (curr != NULL) {
        struct DListNode* todel = curr;
        curr = curr->next;
        free(todel);
    }

    // FUNCTIE AANGEPAST!
    // De list pointer werd niet vrijgesteld
    // Originele code had een memory leak
    free(dlist);
}

// Print the elements of the list in reverse order. For example, if the list contains
// the numbers 3, 7, and 1, it should print "[1, 7, 3]\n".
//
// Python: print(list[::-1])
void dlist_print_reverse(struct DList* dlist)
{
    printf("[");

    struct DListNode* current = dlist->last;

    while (current != NULL)
    {
        printf("%d", current->value);

        // no comma after last value
        if (current->prev != NULL)
            printf(", ");

        current = current->prev;
    }

    printf("]\n");
}

// Return the length of the given list (i.e., the number of values in it)
//
// Python: length = len(list)
int dlist_length(struct DList* dlist)
{
    return dlist->length;
}


// Get the value of the element at the given index and store it in the memory
// location pointed to by value. If the given index is out of range, 0 is
// returned, otherwise 1 is returned.
//
// Python: value = list[i]
// (An IndexError would correspond to a return value of 0)
int dlist_get(struct DList* list, int index, int* value)
{
    if (list->length <= index || index < 0)
        return 0;

    struct DListNode* current;

    if (index <= list->length/2)
    {
        current = list->first;
        for (int i = 0; i < index; ++i)
            current = current->next;
    }
    else
    {
        current = list->last;
        for (int i = list->length; i > index+1; --i)
            current = current->prev;
    }

    *value = current->value;
    return 1;
}

// Append the given value to the given list
//
// Python: list.append(value)
void dlist_append(struct DList* dlist, int value)
{
    struct DListNode *node = malloc(sizeof(struct DListNode));
    node->value = value;
    node->prev = NULL;
    node->next = NULL;

    if (dlist->last == NULL)
    {
        dlist->first = node;
        dlist->last = node;
    }
    else
    {
        dlist->last->next = node;
        node->prev = dlist->last;
        dlist->last = node;
    }

    dlist->length = dlist->length+1;
}

// Insert the element before the given index in the list. A negative index
// means the element should be added to the front (prepended). An index that
// is too high means the element should be added to the back (appended).
//
// Python: list.insert(index, value)
// (Note that the behavior for negative indices differs slightly in Python)
void dlist_insert(struct DList* dlist, int index, int value)
{
    //In cade index is bigger than length, append the element
    if (dlist_length(dlist) <= index)
    {
        dlist_append(dlist, value);
        return;
    }

    //In case the list is empty
    if (dlist_length(dlist) == 0)
    {
        dlist_append(dlist, value);
        return;
    }

    struct DListNode *node = malloc(sizeof(struct DListNode));
    node->value = value;
    node->prev = NULL;
    node->next = NULL;

    //In case the index is negative, make it the first element
    if (index < 0)
        index = 0;

    //Prepend is a special case
    if (index == 0)
    {
        node->next = dlist->first;
        dlist->first->prev = node;
        dlist->first = node;
        dlist->length = dlist->length+1;
        return;
    }

    struct DListNode* current;
    if (index <= dlist->length/2)
    {
        current = dlist->first;
        for (int i = 0; i < index; ++i)
            current = current->next;
    }
    else
    {
        current = dlist->last;
        for (int i = dlist->length; i > index+1; --i)
            current = current->prev;
    }

    node->next = current;
    node->prev = current->prev;
    current->prev->next = node;
    current->prev = node;

    dlist->length = dlist->length+1;
}

// Remove the element at the given index from the given list. If the given index
// is out of range, 0 is returned, otherwise 1 is returned.
//
// Python: del list[i]
// (An IndexError would correspond to a return value of 0)
int dlist_remove(struct DList* dlist, int index)
{
    //In case the element doesn't exist (out of range)
    if (dlist->length <= index || index < 0)
        return 0;

    //In case it is the only element
    if(dlist->first == dlist->last)
    {
        free(dlist->first);
        dlist->first = NULL;
        dlist->last = NULL;
        dlist->length = dlist->length-1;
        return 1;
    }

    //In case it is the first element
    if(index == 0)
    {
        struct DListNode* todel = dlist->first;
        dlist->first = dlist->first->next;
        dlist->first->prev = NULL;
        free(todel);

        dlist->length = dlist->length-1;
        return 1;
    }

    //In cas it is the last element
    if(index == dlist->length-1)
    {
        struct DListNode* todel = dlist->last;
        dlist->last = dlist->last->prev;
        dlist->last->next = NULL;
        free(todel);

        dlist->length = dlist->length-1;
        return 1;
    }

    //Non of the above
    struct DListNode* current;
    if (index <= dlist->length/2)
    {
        current = dlist->first;
        for (int i = 0; i < index; ++i)
            current = current->next;
    }
    else
    {
        current = dlist->last;
        for (int i = dlist->length; i > index+1; --i)
            current = current->prev;
    }

    current->prev->next = current->next;
    current->next->prev = current->prev;
    free(current);

    dlist->length = dlist->length-1;
    return 1;
}


// ==================== Stacks ====================

// Create an empty stack
struct Stack* stack_create()
{
    struct Stack* stack = malloc(sizeof(struct Stack));
    stack->top = NULL;
    return stack;
}

// Push a new element on the stack
void stack_push(struct Stack* stack, int x)
{
    struct StackNode* node = malloc(sizeof(struct StackNode));
    node->value = x;

    if (stack_isempty(stack) == 1)
        node->next = NULL;
    else
        node->next = stack->top;

    stack->top = node;
}

// Get the value of the last added element to the stack and store it in the
// memory location pointed to by value. If the stack is empty, 0 is returned,
// otherwise 1 is returned.
int stack_pop(struct Stack* stack, int *value)
{
    if (stack_isempty(stack) == 1)
        return 0;

    struct StackNode* todel = stack->top;
    stack->top = stack->top->next;
    *value = todel->value;

    free(todel);
    return 1;
}

// Returns 1 if the stack is empty (i.e. there are no elements to pop).
// Otherwise it returns 0.
int stack_isempty(struct Stack* stack)
{
    if (stack->top == NULL)
        return 1;
    return 0;
}

// Delete the given stack
void stack_delete(struct Stack* stack)
{
    struct StackNode* todel = stack->top;

    while(todel != NULL)
    {
        stack->top = stack->top->next;
        free(todel);

        todel = stack->top;
    }

    free(stack);
}



// Evaluate the postfix expression given in parameter `formula`.
//
// Returns 0 if formula is an invalid postfix expression, and 1 if it is a valid
// postfix expression. The result is returned using the pointer `result`.
int evaluate(char* formula, int* result)
{
    struct Stack* stack = stack_create();
    int length = strlen(formula);
    *result = 0;

    char temp[100];
    strcpy (temp,"");
    int val1;
    int val2;
    int returnVal = 1;
    for (int i = 0; i < length+1; ++i)
    {
        if(formula[i] == ' ' || formula[i] == '\0')
        {
            if (strcmp(temp, "+") == 0)
            {
                if(stack_pop(stack, &val1) == 0 || stack_pop(stack, &val2) == 0)
                    returnVal = 0;
                stack_push(stack, val2+val1);
            }
            else if(strcmp(temp, "-") == 0)
            {
                if(stack_pop(stack, &val1) == 0 || stack_pop(stack, &val2) == 0)
                    returnVal = 0;
                stack_push(stack, val2-val1);
            }
            else if(strcmp(temp, "*") == 0)
            {
                if(stack_pop(stack, &val1) == 0 || stack_pop(stack, &val2) == 0)
                    returnVal = 0;
                stack_push(stack, val2*val1);
            }
            else if(strcmp(temp, "/") == 0)
            {
                if(stack_pop(stack, &val1) == 0 || stack_pop(stack, &val2) == 0)
                    returnVal = 0;
                stack_push(stack, val2/val1);
            }
            else
            {
                stack_push(stack, atoi(temp));
            }
            strcpy(temp,"");
        }
        else
        {
            strncat(temp, &formula[i],1);
        }
    }

    if(stack_pop(stack, result) == 0)
        returnVal = 0;

    stack_delete(stack);
    return returnVal;
}

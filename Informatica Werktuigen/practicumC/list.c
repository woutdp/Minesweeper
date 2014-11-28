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
}

// Get the value of the last element and store it in the memory location pointed
// to by value. The last element is removed from the list. If the list is emtpy,
// 0 is returned, otherwise 1 is returned.
//
// Python: value = list.pop()
// (An IndexError would correspond to a return value of 0)
int list_pop(struct List* list, int* value)
{
}

// Prepend the value to the front of the list.
//
// Python: list.insert(0, value)
void list_prepend(struct List* list, int value)
{
}

// Insert the element before the given index in the list. A negative index
// means the element should be added to the front (prepended). An index that
// is too high means the element should be added to the back (appended).
//
// Python: list.insert(index, value)
// (Note that the behavior for negative indices differs slightly in Python)
void list_insert(struct List* list, int index, int value)
{
}

// Insert the value at the correct position in a sorted list. Assume that the list
// is sorted from lowest to highest (ascending). The list must remain sorted!
void list_insert_sorted(struct List* list, int value)
{
}

// Print the elements of the list in reverse order. For example, if the list contains
// the numbers 3, 7, and 1, it should print "[1, 7, 3]\n".
//
// Python: print(list[::-1])
void list_print_reverse(struct List* list)
{
}

void list_remove_all(struct List* list, int value)
{
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
}

// Print the elements of the list in reverse order. For example, if the list contains
// the numbers 3, 7, and 1, it should print "[1, 7, 3]\n".
//
// Python: print(list[::-1])
void dlist_print_reverse(struct DList* dlist)
{
}

// Return the length of the given list (i.e., the number of values in it)
//
// Python: length = len(list)
int dlist_length(struct DList* dlist)
{
}


// Get the value of the element at the given index and store it in the memory
// location pointed to by value. If the given index is out of range, 0 is
// returned, otherwise 1 is returned.
//
// Python: value = list[i]
// (An IndexError would correspond to a return value of 0)
int dlist_get(struct DList* list, int index, int* value)
{
}

// Append the given value to the given list
//
// Python: list.append(value)
void dlist_append(struct DList* dlist, int value)
{
}

// Insert the element before the given index in the list. A negative index
// means the element should be added to the front (prepended). An index that
// is too high means the element should be added to the back (appended).
//
// Python: list.insert(index, value)
// (Note that the behavior for negative indices differs slightly in Python)
void dlist_insert(struct DList* dlist, int index, int value)
{
}

// Remove the element at the given index from the given list. If the given index
// is out of range, 0 is returned, otherwise 1 is returned.
//
// Python: del list[i]
// (An IndexError would correspond to a return value of 0)
int dlist_remove(struct DList* dlist, int index)
{
}


// ==================== Stacks ====================

// Create an empty stack
struct Stack* stack_create()
{
}

// Push a new element on the stack
void stack_push(struct Stack* stack, int x)
{
}

// Get the value of the last added element to the stack and store it in the
// memory location pointed to by value. If the stack is empty, 0 is returned,
// otherwise 1 is returned.
int stack_pop(struct Stack* stack, int *value)
{
}

// Returns 1 if the stack is empty (i.e. there are no elements to pop).
// Otherwise it returns 0.
int stack_isempty(struct Stack* stack)
{
}

// Delete the given stack
void stack_delete(struct Stack* stack)
{
}



// Evaluate the postfix expression given in parameter `formula`.
//
// Returns 0 if formula is an invalid postfix expression, and 1 if it is a valid
// postfix expression. The result is returned using the pointer `result`.
int evaluate(char* formula, int* result)
{
}


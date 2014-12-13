int list_pop_front1(struct List* list, int* value)
{
	if (list->first == NULL)
		return 0;

	// Get value first item and free it
	*value = list->first->value;
	free(list->first);

	// Update pointer to first element
	list->first = list->first->next;

	return 1;
}
int list_pop_front2(struct List* list, int* value)
{
	struct ListNode *pop = list->first;
	if (pop == NULL)
		return 0;

	value = pop->value;
	list->first = pop->next;
	free(pop);

	return 1;
}
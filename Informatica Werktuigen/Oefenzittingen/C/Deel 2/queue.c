#include "queue.h"

struct Queue* QueueCreate()
{
    struct Queue* q = malloc(sizeof(struct Queue));
    return q;
}

void QueueCreate(struct Queue* q)
{
    struct QueueNode* current = q->first;
    struct QueueNode* previous;
    while(current != NULL)
    {
        previous = current;
        current = current->next;
        free(previous);
    }

    free(q);
}

void QueuePrint(struct Queue* q)
{
    if (q == NULL)
        return;

    struct QueueNode* current = q->first;
    while(current != NULL)
    {
        printf("%i\n", current->value);

        current = current->next;
    }
}

void Enqueue(struct Queue* q)
{

}

void Dequeue(struct Queue* q)
{

}
struct QueueNode
{
    int value;
    struct QueueNode* next;
};

struct Queue
{
    struct QueueNode* first;
    struct QueueNode* last;
};

struct Queue* QueueCreate();
void QueueDestroy(struct Queue* q);

void Enqueue(struct Queue* q);
void Dequeue(struct Queue* q);

void QueuePrint(struct Queue* q);
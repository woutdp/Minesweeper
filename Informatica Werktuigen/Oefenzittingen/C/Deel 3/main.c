#include <stdlib.h>
#include <stdio.h>

#include "tree.h"

int main(int argc, char* argv[])
{
    struct Tree* tree = tree_create();
    free(tree);

    return 0;
}
#include <stdlib.h>
#include <stdio.h>

#include "tree.h"

struct Tree* tree_create()
{
    struct Tree* tree = malloc(sizeof(struct Tree));
    tree->root = NULL;
    return tree;
}

void tree_delete()
{

}

void tree_insert(struct Tree* tree, int value)
{
    struct TreeNode* node = malloc(sizeof(struct TreeNode));
    node->value = value;
    node->left_node = NULL;
    node->right_node = NULL;

    // NOT COMPLETE
    if(tree->node == NULL)
    {
        tree->root = node;
        return;
    }

    struct TreeNode* current = tree->root;
    struct TreeNode* previous = current;

    if(current == NULL)
    {
        previous = node;
        return;
    }

    if(value == current->value) return;
    if(value > current->value)
    {

    }

    if(value < current->value)
    {

    }
}

void tree_print(struct Tree* tree)
{

}

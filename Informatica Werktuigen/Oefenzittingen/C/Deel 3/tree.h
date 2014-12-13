struct Tree
{
    struct TreeNode* root;
};

struct TreeNode
{
    int value;
    struct TreeNode* left_child;
    struct TreeNode* right_child;
};

struct Tree* tree_create();
void tree_delete();
void tree_insert(struct Tree* tree, int value);
void tree_print(struct Tree* tree);
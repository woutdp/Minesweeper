#include <stdio.h>
#include <string.h>

#include "queue.h"

int StringLength(const char* str)
{
    int counter = 0;
    while(str[counter] != '\0')
        ++counter;

    return counter;
}

void StringLongPrint(const char* str)
{
    int counter = 0;
    while(str[counter] != '\0')
    {
        printf("%c\n", str[counter]);
        ++counter;
    }
}

void StringCapitalize(const char* in, char* out)
{
    int counter = 0;
    while(in[counter] != '\0')
    {
        if (islower(in[counter]))
            out[counter] = toupper(in[counter]);
        else
            out[counter] = in[counter];
        ++counter;
    }
}

int StringCompare(const char* str1, const char* str2)
{
    int counter = 0;
    int diff = 0;
    while(str1[counter] == str2[counter])
        ++counter;

    diff = str1[counter] - str2[counter];
    return diff;
}

void StringCat(char* destination, const char* source)
{
    int len = strlen(destination);
    int counter = 0;
    while(source[counter] != '\0')
    {
        destination[counter + len] = source[counter];
        ++counter;
    }
    destination[counter+len] = '\0';
}

int StringCountWords(const char* str)
{
    int wordCount = 0;
    int counter = 0;
    while(str[counter] != '\0')
    {
        if (isspace(str[counter]) == 0) //not a space char, so a word
        {
            ++counter;
            ++wordCount;
        }

        while(str[counter] != '\0' && isspace(str[counter]) == 0) //not at end and not a space
            ++counter;

        while(str[counter] != '\0' && isspace(str[counter]) != 0) // is a space
            ++counter;
    }

    return wordCount;
}

int main(int argc, char* argv[])
{
    char* str = "A long ";
    char* str2 = "word";

    //////TESTS
    //strlen
    int len = strlen(str);
    printf("\n");
    if(len != StringLength(str))
    {
        printf("Length StringLength returns a wrong size, should return %i.\n", len);
    }

    //long print
    StringLongPrint(str);

    //capitalize
    char capitalized[40];
    StringCapitalize(str, capitalized);
    puts(capitalized);

    //string compare
    int origStringCompare = strcmp(str, str2);
    int newStringCompare = StringCompare(str, str2);
    printf("orig = %i; new = %i;\n", origStringCompare, newStringCompare);

    //string compare
    char newCat[100];
    strcpy(newCat, "This ");
    strcat(newCat, "is ");
    StringCat(newCat, "SPARTA!");
    puts(newCat);

    //count words
    int wordCount = StringCountWords("  a \n  test  b c ");
    printf("Wordcount = %i\n", wordCount);


    //QUEUE
    printf("QUEUETESTS\n");
    struct Queue* q = QueueCreate();
    QueuePrint(q);
    QueueDestroy(q);

    return 0;
}
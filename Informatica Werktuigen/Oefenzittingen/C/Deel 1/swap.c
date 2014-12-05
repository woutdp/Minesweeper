int main()
{
    int value1 = 3;
    int value2 = 7;
    
    printf("Value1 = %d en value2 = %d\n", value1, value2);
    swap(&value1, &value2);
    printf("Value1 = %d en value2 = %d\n", value1, value2);
    
    return 0;
}
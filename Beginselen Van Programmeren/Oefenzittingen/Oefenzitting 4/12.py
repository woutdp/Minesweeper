__author__ = 'Wout De Puysseleir'

def string_multiple(n, string):
    stringSum = ""
    for i in range(n):
        stringSum += string
    return stringSum

def string_multiple_times(n, string):
    string = string*n
    return string

string = input("Input a string: ")
n = int(input("How many times: "))

newString = string_multiple_times(n, string)
print(newString)

__author__ = 'Wout De Puysseleir'

str = str(input("Input a string to reverse\n"))
length = len(str)
newStr = ""

for i in range(length,-1,-1):
    newStr += str[i:i+1]

print (newStr)

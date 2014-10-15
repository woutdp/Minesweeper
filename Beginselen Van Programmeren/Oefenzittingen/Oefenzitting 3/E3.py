__author__ = 'Wout De Puysseleir'

n = int(input("Enter a string of values \n"))
solution = n
counter = 0
while n != 0:
    n = int(input())
    if counter%2 == 0:
        solution -= n
    else:
        solution += n
    counter += 1

print("The alternating sum = ", solution)

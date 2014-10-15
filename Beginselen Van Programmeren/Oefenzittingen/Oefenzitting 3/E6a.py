__author__ = 'Wout De Puysseleir'

n = int(input("Input a positive integer: "))
sum = 1

for i in range(1,n+1):
    sum *= i

print(sum)

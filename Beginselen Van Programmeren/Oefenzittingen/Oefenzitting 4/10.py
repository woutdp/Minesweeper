__author__ = 'Wout De Puysseleir'

def Collatz(n):
    amountOfOperations = 0
    while n != 1:
        if n % 2 == 0:
            n //= 2
        else:
            n *= 3
            n += 1
        print(n)
        amountOfOperations += 1
    print("Amount of operations: ", amountOfOperations)

n = int(input("Input a number: "))
Collatz(n)

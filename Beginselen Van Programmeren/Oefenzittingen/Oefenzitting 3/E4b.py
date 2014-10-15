__author__ = 'Wout De Puysseleir'

n = int(input("Enter a positive integer:"))

for j in range(2, n):
    prime = True
    for i in range(2,j):
        if j%i == 0:
            i = n
            prime = False
    if prime: print(j)

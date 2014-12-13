__author__ = 'Wout De Puysseleir'

n = int(input("Enter a positive integer:"))
prime = True

for i in range(2, n):
    if n%i == 0:
        prime = False
        i = n

if(prime):
    print(n, "is a prime number")
else:
    print(n, "is NOT a prime number")

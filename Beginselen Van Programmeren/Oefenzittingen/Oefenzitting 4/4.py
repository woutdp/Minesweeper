def isPrime(n):
    for i in range(2, n):
        if n % i == 0:
            return False
    return True

def printAllPrimesUntil(n):
    for i in range(2,n):
        if isPrime(i):
            print(i)

n = int(input("Give me a prime: "))
printAllPrimesUntil(n)
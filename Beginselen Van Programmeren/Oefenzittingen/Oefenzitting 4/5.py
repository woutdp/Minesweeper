def factorial(n):
    sum = 1
    for i in range(1, n+1):
        sum *= i
    return sum

def estimateExp1(exp, n):
    e = 0
    for i in range(n):
        e += exp ** i / factorial(i)
    return e

def estimateExp2(exp, delta):
    e = 0
    smaller = False
    i = 0
    while smaller == False:
        term = exp ** i / factorial(i)
        if (term < delta):
            smaller = True
        e += term
        i += 1
    return e

n = float(input("What should be the accuracy: "))
print(estimateExp2(1,n))
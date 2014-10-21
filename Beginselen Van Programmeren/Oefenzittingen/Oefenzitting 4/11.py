import math
__author__ = 'Wout De Puysseleir'

def AreaOfCirlce(r):
    A = r*r*math.pi
    return A

def SumOfAreasUntil(r, n):
    sum = 0
    for i in range(1, n+1):
       sum += AreaOfCirlce(r*i)
    return sum

r = int(input("Input radius in cm: "))
n = int(input("Input amount: "))
print(SumOfAreasUntil(r, n), "cm²")

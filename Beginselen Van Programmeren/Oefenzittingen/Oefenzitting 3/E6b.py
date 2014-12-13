__author__ = 'Wout De Puysseleir'

x = int(input("We're going to calculate e^x, input x: "))
acc = int(input("How accurate do you want it to be, the higher the number the more accurate: "))
ex = 0
for n in range(acc):
    fac = 1
    for i in range(1,n+1):
        fac *= i
    sol = x**n/ fac
    ex += sol

print(ex)
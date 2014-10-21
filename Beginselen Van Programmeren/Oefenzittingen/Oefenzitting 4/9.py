__author__ = 'Wout De Puysseleir'

p = 1.23
A = 2.5
Cd = 0.2

def Fd(p, v, A, Cd):
    Fd = (1/2)*p*(v**2)*A*Cd
    return Fd

def RequiredWattsToOvercome(Fd, v):
    P = Fd * v
    return P

def HorsePower(P):
    Hp = P/746
    return Hp

v = float(input("What is your velocity? "))
Fd = Fd(p,v,A,Cd)
print("Fd = ", Fd)
P = RequiredWattsToOvercome(Fd,v)
print("Watts = ", P)
Hp = HorsePower(P)
print("Horsepower = ", Hp)

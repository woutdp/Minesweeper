__author__ = 'Wout De Puysseleir'

x = 1
basis = 7
bound = 10000
#STAP 1: PRECONDITIE: x = 1, basis = 7, bound = 10.000
while x <= bound: #INVARIANT x bevat x * 7 *...* 7 waar x maximaal 1 keer meer een macht van is genomen dan bounds
    x = x * basis
#STAP 2: POSTCONDITIE: x = de kleinste macht van 7 groter dan 10.000

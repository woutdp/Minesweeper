__author__ = 'Wout De Puysseleir'

amount = int(input("Enter the numbers of terms: "))

sum = 0
for i in range(amount):
    sum += i+1

print("The sum of the first", amount, "natural numbers is", sum)

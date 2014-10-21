__author__ = 'Wout De Puysseleir'

def creditcard(creditCardNumber):
    sum = 0
    for i in range(1,5):
        strNumber = str(creditCardNumber)
        sum += int(strNumber[i*2-1:i*2])

    sumDouble = 0
    for i in range(0,4):
        strNumber = str(creditCardNumber)
        tempNr = int(strNumber[i*2:i*2+1])*2
        for j in range(0,len(str(tempNr))):
            sumDouble += int(str(tempNr)[j:j+1])
    totSum = sum + sumDouble

    if(str(totSum).endswith('0')):
        print("The credit card number is valid")
    else:
        print("The credit card number is not valid")
        mod = totSum % 10
        if (mod//2 <= 2):
            correctedNumber = creditCardNumber + mod
        else:
            correctedNumber = creditCardNumber - mod
        print("Your credit card number could be", correctedNumber)


n = int(input("Enter 8 digit credit card number: "))
while len(str(n)) != 8:
    n = int(input("This is not 8 integers long, try again: "))
creditcard(n)
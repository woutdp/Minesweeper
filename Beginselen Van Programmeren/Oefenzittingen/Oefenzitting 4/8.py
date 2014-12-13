__author__ = 'Wout De Puysseleir'

def getCheckDigit(code):
    strCode = str(code)
    sum = 0
    for i in range(len(strCode)):
       sum += int(strCode[i])
    check = sum % 10
    check = abs(check - 10)
    return check

def numberLookup(code):
    if code ==1:
        return ":::||"
    elif code ==2:
        return "::||:"
    elif code ==3:
        return "::|:|"
    elif code ==4:
        return ":|::|"
    elif code ==5:
        return ":|:|:"
    elif code ==6:
        return ":||::"
    elif code ==7:
        return "|:::|"
    elif code ==8:
        return "|::|:"
    elif code ==9:
        return "|:|::"
    elif code ==0:
        return "||:::"

def convertToBarcode(code):
    strCode = str(code)
    rStr = ""
    for i in range(len(strCode)):
        rStr += numberLookup(int(strCode[i]))
    rStr += numberLookup(getCheckDigit(strCode[i]))
    return rStr

code = int(input("Input your code: "))
print(convertToBarcode(code))

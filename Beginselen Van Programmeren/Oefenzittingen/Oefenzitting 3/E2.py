__author__ = 'Wout De Puysseleir'

degrees = input("Enter your degrees in celcius:")
while degrees != 'q':
    far = float(degrees)  *  9/5 + 32
    print("Fahrenheit =", far)
    degrees = input("Enter your degrees in celcius:")

def toFahrenheit(c):
    F = 32 + 1.8*c
    return F

c = int(input("Input your Celcius: "))

print(c, "Celcius is", toFahrenheit(c), "in Fahrenheit")

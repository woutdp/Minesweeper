__author__ = 'Wout De Puysseleir'

height = int(input("Type in the height of the pyramid:"))

for i in range(height):
    spacesL = height - i
    pyramid = ""
    for j in range(spacesL-1):
        pyramid += " "
    pounds = i*'#'*2 + '#'
    pyramid += pounds
    print(pyramid)

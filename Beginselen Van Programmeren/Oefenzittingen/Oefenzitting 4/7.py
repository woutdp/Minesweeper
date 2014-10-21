__author__ = 'Wout De Puysseleir'

def drawPyramid(height, char):
    str = ""
    for i in range(height):
        spacesL = height - i
        pyramid = ""
        for j in range(spacesL-1):
            pyramid += " "
        pounds = i*char*2 + char
        pyramid += pounds
        str += pyramid + '\n'
    return str

height = int(input("Type in the height of the pyramid:"))
char = input("Type in the char of the pyramid:")

str = drawPyramid(height, char)
print(str)

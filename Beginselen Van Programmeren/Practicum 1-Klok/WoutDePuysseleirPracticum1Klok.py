import math
import datetime
from graphics import GraphicsWindow

def AddZeroAndConvert(s):
    if(s <10):
        nString = "0"+ str(s)
    else:
        nString= str(s)
    return nString

#User Input
timeInput = input("Would you like to use the actual time (input: at) or set a custom time (input: ct)?")
while not (timeInput == "ct" or timeInput == "at"):
    print("This is not a valid input, try again. Only valid inputs are 'at' and 'ct'")
    timeInput = input("Actual time (input: at) or set a custom time (input: ct)?")

if timeInput=="ct":
    hours = int(input("What is your hours input?"))
    minutes = int(input("What is your minutes input?"))
    hours %= 24
    minutes %= 60
else:
    hours = datetime.datetime.now().time().hour
    minutes = datetime.datetime.now().time().minute

formatInput = str(input("Would you like to use 24-hour (input: pm or 24) or 12-hour (input: am or 12) format?"))
while not (formatInput == "pm" or formatInput == "am" or formatInput == "12" or formatInput == "24"):
    print("This is not a valid input, try again. Only valid inputs are 'pm', '24', 'am' and '12'")
    formatInput = str(input("24-hour format (input: 'pm' or '24') or 12-hour format (input: am or '12')?"))

if(formatInput == "12" or formatInput == "am"):
    hours %= 12

#Setup the window
windowWidth = 150
windowHeight = 180
window = GraphicsWindow(windowWidth,windowHeight)
canv = window.canvas()
canv.setBackground("white")

#Setup variables
mX = windowWidth/2
mY = windowHeight/2.5
r = windowWidth/2.5
scaleMinute = r/1.05
scaleHour = r/1.7

#Draw cirlce
canv.setColor("black")
canv.drawOval(mX-r,mY-r,r*2,r*2)
canv.setColor("white")
outline = 2
canv.drawOval(mX-r+outline,mY-r+outline,r*2-outline-2,r*2-outline*2)

#Draw minute arrow
canv.setColor("black")
minCos = math.cos((180- minutes*6.0)*math.pi/180)
minSin = math.sin((180- minutes*6.0)*math.pi/180)
canv.drawLine(mX,mY,mX+minSin*scaleMinute,mY+minCos*scaleMinute)

#Draw hour arrow
canv.setColor("black")
arHour = hours + minutes/60
minCos = math.cos((180- arHour*30.0)*math.pi/180)
minSin = math.sin((180- arHour*30.0)*math.pi/180)
canv.drawLine(mX,mY,mX+minSin*scaleHour,mY+minCos*scaleHour)

#Draw text
timeText = AddZeroAndConvert(hours) + ":" + AddZeroAndConvert(minutes)
canv.drawText(mX - 11 ,mY + r+ 10,timeText)

window.wait()

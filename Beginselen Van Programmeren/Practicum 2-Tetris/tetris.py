from tkinter import *
from copy import deepcopy
from random import randrange
import random

g_ActiveBlock = {"coord":[]}
g_Field = []
g_Pause = False

def initialize():
    # called once when the game is started (main() executed)
    # [ put your own model/representation 
    #   initialization here ]
    return {"dimensions": (10,20),
            "line":             {"coord":[(3,0),(4,0),(5,0),(6,0)], "a":(4,0), "counter":False, "amount":2}, #amount is rotation amount, 2 means only 2 rotations
            "squiggly":         {"coord":[(4,1),(5,0),(5,1),(6,0)], "a":(5,1), "counter":False, "amount":2}, #counter is to see if it needs to be turned counter clockwise
            "reverseSquiggly":  {"coord":[(4,0),(5,1),(5,0),(6,1)], "a":(5,0), "counter":False, "amount":2},
            "lBlock":           {"coord":[(3,1),(3,0),(4,0),(5,0)], "a":(4,0), "counter":False, "amount":4},
            "reverseLBlock":    {"coord":[(3,0),(4,0),(5,0),(5,1)], "a":(4,0), "counter":False, "amount":4},
            "tBlock":           {"coord":[(4,1),(4,0),(5,0),(6,0)], "a":(5,0), "counter":False, "amount":4},
            "square":           {"coord":[(4,0),(5,0),(4,1),(5,1)], "a":None,  "counter":False, "amount":0}}
    # the data structure returned from this method
    # is passed as parameter ''model'' to the functions
    # draw(), onkey() and onloop() below
    
def draw(model, canvas):
    # called after onkey() and onloop(), so every
    # X milliseconds and after each time the user
    # presses a key

    # clear canvas
    canvas.delete(ALL)

    block_height = 23
    block_margin = 2
    field_padding = 10
    default_color = "#f2f2f2"
    dimensions = model["dimensions"]

    # draws rectangle grid
    for x in range(dimensions[0]):
        for y in range(dimensions[1]):
            color = default_color
            rect = drawBlock(canvas, x, y, color, block_height, block_margin, field_padding)

    for t in g_ActiveBlock["coord"]:
        color = g_ActiveBlock["col"] # color of filled block
        rect = drawBlock(canvas, t[0], t[1], color, block_height, block_margin, field_padding)

    for t in g_Field:
        color = t[2] # color of filled block
        rect = drawBlock(canvas, t[0], t[1], color, block_height, block_margin, field_padding)


def onkey(model, keycode):
    if keycode == 113: #LEFT
        activeBlockMove("left", model)
    elif keycode == 114: #RIGHT
        activeBlockMove("right", model)
    elif keycode == 116: #DOWN
        activeBlockMove("down", model)
    elif keycode == 111: #UP - ROTATE
        activeBlockRotate(model)
    elif keycode == 27: #R
        global g_Pause
        g_Pause = not g_Pause

    #print(keycode)

def onloop(model):
    if len(g_ActiveBlock["coord"]) == 0:
        requestNewBlock(model)

    if g_Pause != True:
        activeBlockMove("down", model)


###########################################################
###############---MY OWN FUNCTIONS---######################
###########################################################
def drawBlock(canvas, x, y, color, block_height, block_margin, field_padding):
    return canvas.create_rectangle(
            x*block_height+(x+1)*block_margin+field_padding,
            y*block_height+(y+1)*block_margin+field_padding,
            (x+1)*block_height+(x+1)*block_margin+field_padding,
            (y+1)*block_height+(y+1)*block_margin+field_padding,
            fill=color, outline=color)


def activeBlockFreeze(model):
    global g_Field
    for t in g_ActiveBlock["coord"]:
        g_Field.append((t[0],t[1],g_ActiveBlock["col"]))

    deleteFullLines(model)
    requestNewBlock(model)

def blockOutOfBounds(block, direction, model):
    if (direction == "left" or direction == "right" or direction == "rotate"):
        for i in block["coord"]:
            if i[0] < 0 or i[0] >= model["dimensions"][0]:
                return True
            for t in g_Field:
                if i[0] == t[0] and i[1] == t[1]:
                    return True
    elif (direction == "down"):
        for i in block["coord"]:
            if i[1] >= model["dimensions"][1]:
                activeBlockFreeze(model)
                return True
            for t in g_Field:
                if i[0] == t[0] and i[1] == t[1]:
                    activeBlockFreeze(model)
                    return True

def activeBlockMove(direction, model):
    global g_ActiveBlock
    block = deepcopy(g_ActiveBlock)
    if (direction == "left"):
        if block["a"] != None:
            block["a"] = (block["a"][0]-1, block["a"][1])
        for i in range(len(block["coord"])):
            block["coord"][i] = (block["coord"][i][0]-1, block["coord"][i][1])
    elif (direction == "right"):
        if block["a"] != None:
            block["a"] = (block["a"][0]+1, block["a"][1])
        for i in range(len(block["coord"])):
            block["coord"][i] = (block["coord"][i][0]+1, block["coord"][i][1])
    elif (direction == "down"):
        if block["a"] != None:
            block["a"] = (block["a"][0], block["a"][1]+1)
        for i in range(len(block["coord"])):
            block["coord"][i] = (block["coord"][i][0], block["coord"][i][1] +1)

    if not blockOutOfBounds(block, direction, model):
        g_ActiveBlock = block

def activeBlockRotate(model):
    global g_ActiveBlock
    if g_ActiveBlock["a"] == None:
        return

    block = deepcopy(g_ActiveBlock)

    # MAGIC FORMULA!
    # x = y
    # y = -x
    # this will rotate the block counter clockwise

    newCoord = []
    for t in block["coord"]:
        rel = toRelative(block["a"], t)

        if block["counter"] == False:
            coor = (-rel[1], rel[0])
        else:
            coor = (rel[1], -rel[0])

        newCoord.append(toGlobal(block["a"], coor))

    block["coord"] = newCoord

    if block["amount"] == 2:
        block["counter"] = not block["counter"]

    if not blockOutOfBounds(block, "rotate", model):
        g_ActiveBlock = block

def toRelative(anchor, tuple):
    return (tuple[0] - anchor[0], tuple[1] - anchor[1])

def toGlobal(anchor, tuple):
    return (tuple[0] + anchor[0], tuple[1] + anchor[1])

def deleteFullLines(model):
    dimensions = model["dimensions"]
    for y in range(dimensions[1]):
        counter = 0
        for t in g_Field:
            if y == t[1]:
                counter += 1
        if counter == dimensions[0]:
            deleteLine(y,model)

def deleteLine(y, model):
    global g_Field
    field = []
    for t in g_Field:
        if y != t[1]:
            field.append(t)
    g_Field = field
    moveFieldDown(y)

def moveFieldDown(y):
    for i in range(len(g_Field)):
        if y > g_Field[i][1]:
            g_Field[i] = (g_Field[i][0], g_Field[i][1]+1, g_Field[i][2])

def requestNewBlock(model):
    global g_ActiveBlock

    choices = 7
    blockname = ""
    r = randrange(choices)

    if r == 0:
        blockname = "square"
    elif r == 1:
        blockname = "squiggly"
    elif r == 2:
        blockname = "reverseSquiggly"
    elif r == 3:
        blockname = "line"
    elif r == 4:
        blockname = "tBlock"
    elif r == 5:
        blockname = "lBlock"
    elif r == 6:
        blockname = "reverseLBlock"
    else:
        print("requestNewBlock: There is no blockname for this number")

    g_ActiveBlock = deepcopy(model[blockname])

    r = lambda: random.randint(150,220)
    g_ActiveBlock["col"] = '#%02X%02X%02X' % (r(),r(),r())

    checkGameOver(model)

def checkGameOver(model):
    for i in g_ActiveBlock["coord"]:
        for t in g_Field:
            if i[0] == t[0] and i[1] == t[1]:
                sys.exit()

###########################################################
# normally, you would not need to change anything in main #    
def main(update_interval, canvas_dimensions):
    def keypress(event, model, canvas):
        onkey(model, event.keycode)
        draw(model, canvas)

    def gameloop(X, model, master, canvas):
        master.after(X, gameloop, X, model, master, canvas)
        onloop(model)
        draw(model, canvas)

    # initialize your model
    model = initialize()

    # initialize top level widget
    master = Tk()
    canvas = Canvas(master, width=canvas_dimensions[0], 
            height=canvas_dimensions[1], background="white") # initialize canvas

    # bind the keypress() function to a key press event
    # while passing the model and the canvas as arguments too
    canvas.pack()
    master.bind("<Key>", lambda e: keypress(e, model, canvas))

    # start the gameloop
    gameloop(update_interval, model, master, canvas)

    # enables event handling etc. by tkinter
    master.mainloop()
    
############################################################
    
if __name__ == "__main__":
    update_interval = 500
    canvas_dimensions = (10*(23+2)+20,20*(23+2)+20)

    main(update_interval, canvas_dimensions)

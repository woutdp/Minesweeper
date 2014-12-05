from tkinter import *

def initialize():
    # called once when the game is started (main() executed)
    # [ put your own model/representation 
    #   initialization here ]
    return {"dimensions": (10,20),
             "square": {"x":4, "y":0}}
    # the data structure returned from this method
    # is passed as parameter ''model'' to the functions
    # draw(), onkey() and onloop() below
    
def draw(model, canvas):
    # called after onkey() and onloop(), so every
    # X milliseconds and after each time the user
    # presses a key

    # clear canvas
    canvas.delete(ALL)

    block_height = 20
    block_margin = 4
    dimensions = model["dimensions"]
    square = model["square"]
    for x in range(dimensions[0]):
        for y in range(dimensions[1]):
            color = "#f2f2f2"
            # default color of empty block
            if x==square["x"] and y==square["y"]:
                color = "#555"
                # color of filled block
            rect = canvas.create_rectangle(
                x*block_height+(x+1)*block_margin, 
                y*block_height+(y+1)*block_margin, 
                (x+1)*block_height+(x+1)*block_margin, 
                (y+1)*block_height+(y+1)*block_margin, 
                fill=color, outline=color)
            # draws a rectangle
    # draws rectangle grid
    
def onkey(model, keycode):
    # called when user presses a key
    # [ put your own code here ]
    if keycode == 113: #LEFT
        square = model["square"]
        square["x"] -= 1
    elif keycode == 114: #RIGHT
        square = model["square"]
        square["x"] += 1
    elif keycode == 116: #DOWN
        square = model["square"]
        square["y"] += 1

    print(keycode)

def onloop(model):
    # called every X milliseconds
    # [ put your own code here ]
    square = model["square"]
    square["y"] += 1
    
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
    master.bind("<Right>", lambda e: keypress(e, model, canvas))
    master.bind("<Left>", lambda e: keypress(e, model, canvas))
    master.bind("<Down>", lambda e: keypress(e, model, canvas))

    # start the gameloop
    gameloop(update_interval, model, master, canvas)

    # enables event handling etc. by tkinter
    master.mainloop()
    
############################################################
    
if __name__ == "__main__":
    update_interval = 500
    canvas_dimensions = (300,500)
    # [ you might want to adjust these settings ]
    main(update_interval, canvas_dimensions)

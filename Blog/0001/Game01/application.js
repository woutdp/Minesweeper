'use strict'
var Application = (function() {
    //===================================
    // CONSTANTS
    //===================================
    var     FIELDX = 20,
            FIELDY = 15,
            TILESIZE = 20,
            TILESPACE = 2,
            BORDER = 22;

    //===================================
    // VARIABLES
    //===================================
    var canvas = document.getElementById("game01");
    canvas.oncontextmenu = function (e) {
        e.preventDefault();
    };
    var ctx = canvas.getContext('2d');

    var field = new Field(FIELDX, FIELDY, TILESIZE, TILESIZE, TILESPACE, BORDER);

    //===================================
    // INPUT
    //===================================
    canvas.addEventListener("mouseup", MouseUp, false)
    canvas.addEventListener("mousemove", MouseOver, false)
    canvas.addEventListener("mousedown", MouseDown, false)
    function MouseUp(event){
        field.MouseUp(event);
    }
    function MouseOver(event){
        field.MouseOver(event);
    }
    function MouseDown(event){
        field.MouseDown(event);
    }

    //===================================
    // GAME - UPDATE/RENDER
    //===================================
    function Update(dt) {
        //resize the canvas if the field has a different size
        var width = field.GetWidth()
        var height = field.GetHeight()
        if (width !== canvas.width)
            Resize(width, height)

        //object updates
        field.Update(dt, this);
    }

    function Render(dt) {
        canvas.width = canvas.width; // This clears the canvas

        field.Render(ctx);
    }

    //===================================
    // OTHER FUNCTIONS
    //===================================
    function Resize(width, height){
        canvas.width = width;
        canvas.height = height;
    }

    Game.run({update: Update, render : Render});
})();

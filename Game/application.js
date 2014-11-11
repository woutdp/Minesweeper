'use strict'
var Application = (function() {
    //===================================
    // CONSTANTS
    //===================================
    var     FIELDX = 25,
            FIELDY = 20,
            TILESIZE = 20,
            TILESPACE = 2;

    //===================================
    // VARIABLES
    //===================================
    var canvas = document.getElementById("game");
    var ctx = canvas.getContext('2d');

    var field = new Field(FIELDX, FIELDY, TILESIZE, TILESIZE, TILESPACE);

    //===================================
    // INPUT
    //===================================
    canvas.addEventListener("click", Click, false)
    function Click(event){
        field.Click(event);
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

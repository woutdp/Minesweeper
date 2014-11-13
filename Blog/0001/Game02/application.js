'use strict'
var Application = (function() {
    //===================================
    // CONSTANTS
    //===================================
    var     FIELDX = 30,
            FIELDY = 20, //max of 50 because of paint draw image?
            TILESIZE = 22,
            TILESPACE = 2,
            DIFFICULTY = 7, //a higher number means LESS bombs
            // 4 = Admiral
            // 5 = General
            // 7 = Sergeant
            // 10 = Soldier
            // 15 = Recruit
            BORDER = 22;

    //===================================
    // VARIABLES
    //===================================
    var canvas = document.getElementById("game02");
    var resetButton = document.getElementById("resetButton");
    resetButton.addEventListener("click", ResetField);
    var ctx = canvas.getContext('2d');
    var field = new Field(FIELDX, FIELDY, TILESIZE, TILESIZE, TILESPACE, DIFFICULTY, BORDER, this);
    var mouseDown = false;

    var cache   = null;   // cached off-screen canvas

    // Prevent the right click menu from coming up
    canvas.oncontextmenu = function (e) {
        e.preventDefault();
    };

    //===================================
    // INPUT
    //===================================
    canvas.addEventListener("mouseup", MouseUp, false)
    canvas.addEventListener("mousemove", MouseOver, false)
    canvas.addEventListener("mousedown", MouseDown, false)
    function MouseUp(event){
        Invalidate();
        mouseDown = false;
        field.MouseUp(event);
    }
    function MouseOver(event){
        Invalidate();
        if (mouseDown){
            field.MouseDown(event);
        }
        field.MouseOver(event);
    }
    function MouseDown(event){
        Invalidate();
        mouseDown = true;
        field.MouseDown(event);
    }

    //===================================
    // GAME - UPDATE/RENDER
    //===================================
    function Update(dt) {
        //Resize the canvas if the field has a different size
        var width = field.GetWidth()
        var height = field.GetHeight()
        if (width !== canvas.width)
            Resize(width, height)

        //Object updates
        field.Update(dt, this);
    }

    function Render() {
        if (invalid) {
            cache = renderToCanvas(field.GetWidth(), field.GetWidth(), ActualRender, cache);
            invalid = false;
        }
        ctx.drawImage(cache, 0, 0);
    }

    function ActualRender(ctx) {
        canvas.width = canvas.width; // This clears the canvas

        ctx.font = (TILESIZE) + "px Verdana";
        field.Render(ctx);
    }

    function ResetField() {
        Invalidate();
        field.Reset();
    }

    function renderToCanvas(width, height, render, canvas) {
      canvas = canvas || createCanvas(width, height, canvas);
      render(canvas.getContext('2d'));
      return canvas;
    }

    function createCanvas(width, height) {
      var canvas = document.createElement('canvas');
      canvas.width = width;
      canvas.height = height;
      return canvas;
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

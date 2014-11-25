'use strict'
var Application = (function() {
    //===================================
    // CONSTANTS
    //===================================
    var     FIELDX = 30,
            FIELDY = parseInt(FIELDX/1.618), //max of 50 because of paint draw image?
            TILESIZE = 26,
            TILESPACE = 0.0,
            DIFFICULTY = 10, //a higher number means LESS bombs
            // 4 = Admiral
            // 5 = General
            // 7 = Sergeant
            // 10 = Soldier
            // 15 = Recruit
            BORDERW = 0,
            BORDERH = 0;

    //===================================
    // VARIABLES
    //===================================
    var canvas = document.getElementById("game");
    var gameContainer = document.getElementById("gameContainer");
    var gameMenu = document.getElementById("gameMenu");
    var container = document.getElementById("container");
    var ctx = canvas.getContext('2d');

    var resetButton = document.getElementById("resetButton");
    resetButton.addEventListener("click", ResetField);
    var resizeButton = document.getElementById("resizeButton");
    resizeButton.addEventListener("click", ResizeField);
    var resizeButton = document.getElementById("resumeGame");
    resizeButton.addEventListener("click", CloseMenu);

    var field = new Field(FIELDX, FIELDY, TILESIZE, TILESIZE, TILESPACE, DIFFICULTY, BORDERW, BORDERH);
    var mouseDown = false;

    var cache   = null;   // cached off-screen canvas

    // Prevent the right click menu from coming up
    canvas.oncontextmenu = function (e) {
        e.preventDefault();
    };

    canvas.onmousedown = function(e){
        e.preventDefault();
    };

    //===================================
    // INPUT
    //===================================
    canvas.addEventListener("mouseup", MouseUp, false)
    canvas.addEventListener("mousemove", MouseOver, false)
    canvas.addEventListener("mousedown", MouseDown, false)
    function MouseUp(event){
        mouseDown = false;
        field.MouseUp(event);
    }
    function MouseOver(event){
        if (mouseDown){
            field.MouseDown(event);
        }
        field.MouseOver(event);
    }
    function MouseDown(event){
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
        if (width != canvas.width || height != canvas.height)
            ResizeCanvas(width, height)

        if (gameContainer.offsetHeight != canvas.height || gameContainer.offsetWidth != canvas.width){
            gameContainer.setAttribute("style","height:"+ canvas.height+"px;" + "width:"+ canvas.width+"px;");
            gameMenu.setAttribute("style","height:"+ (canvas.height-BORDERH*2)+"px;" + "width:"+ (canvas.width-BORDERW*2)+"px;");
            container.setAttribute("style","width:"+ canvas.width+"px;");
        }

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
        ///canvas.width = canvas.width; // This clears the canvas

        field.Render(ctx);
    }

    function ResetField() {
        Invalidate();
        field.Reset();
    }

    function ResizeField() {
        Invalidate();
        gameMenu.style.zIndex = 2;
        gameMenu.style.color = "rgb(255,255,255)";
        canvas.style.webkitFilter = "blur(2px)";
        gameMenu.style.background = "rgba(0,0,0,0.2)";
        gameMenu.style.backgroundImage = "linear-gradient(rgba(255,255,255,0) 20%, rgba(0,0,0,0.5))" /* Standard syntax (must be last)*/
        //field.Resize(FIELDX,FIELDY);
    }

    function CloseMenu() {
        Invalidate();
        gameMenu.style.zIndex = 0;
        gameMenu.style.color = "rgb(255,255,255)";
        canvas.style.webkitFilter = "blur(0px)";
        gameMenu.style.background = "rgba(0,0,0,0.0)";
        gameMenu.style.backgroundImage = "" /* Standard syntax (must be last)*/
        //field.Resize(FIELDX,FIELDY);
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
    function ResizeCanvas(width, height){
        canvas.width = width;
        canvas.height = height+5;
    }

    Game.run({update: Update, render: Render});
})();

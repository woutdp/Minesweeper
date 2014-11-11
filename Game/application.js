'use strict'
var Application = (function() {
    //===================================
    // CONSTANTS
    //===================================
    var     FIELDX = 15,
            FIELDY = 15,
            TILESIZE = 15,
            TILESPACE = 1;

    //===================================
    // VARIABLES
    //===================================

    var canvas = document.getElementById("game");
    var ctx = canvas.getContext('2d');

    var field = new Field(FIELDX, FIELDY, TILESIZE, TILESPACE);

    //===================================
    // GAME - UPDATE/RENDER
    //===================================

    function Update(dt) {
        field.Update(dt);
    }

    function Render(dt) {
        canvas.width = canvas.width; // This clears the canvas

        field.Render(ctx);
    }

    Game.run({update: Update, render : Render});
})();

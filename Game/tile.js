'use strict'
var Tile = function(x, y, size){
    console.log('tile created');
    //===================================
    // VARIABLES
    //===================================
    this.x = x;
    this.y = y;
    this.size = size;
}

Tile.prototype.Update = function(dt){
        //console.log("Update")
    }

Tile.prototype.Render = function(ctx){
        //console.log("Render")
        ctx.fillStyle="#5095F4";
        ctx.fillRect(this.x, this.y, this.size, this.size);
}

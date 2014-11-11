'use strict'
var Field = function(fieldX,fieldY, tileSize, tileSpace){
    console.log('field created');

    //===================================
    // VARIABLES
    //===================================
    this.fieldX     = fieldX;
    this.fieldY     = fieldY;
    this.tileSize   = tileSize;
    this.tileSpace  = tileSpace;

    this.tile = [];

    for (var i = 0, l = fieldX; i < l; i++) {
        this.tile[i] = [];
        for (var j = 0, l2 = fieldY; j < l2; j++) {
            this.tile[i][j] = new Tile(i*(tileSize+tileSpace), j*(tileSize+tileSpace), tileSize);
        }
    }
}

Field.prototype.Update = function(dt){
        //console.log("Update")
        //this.x = Math.cos(new Date().getTime()/300)*100+ 100;
        //this.y = Math.sin(new Date().getTime()/300)*100+ 100;
    }

Field.prototype.Render = function(ctx){
    //console.log("Render")
    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
            this.tile[i][j].Render(ctx);
        }
    }
}

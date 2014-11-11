'use strict'

var Tile = function(x, y, w, h, nx, ny, parent){
    console.log('tile created');
    //===================================
    // VARIABLES
    //===================================
    this.nx     = nx;
    this.ny     = ny;
    this.x      = x;
    this.y      = y;
    this.w      = w;
    this.h      = h;
    this.color  = "#5095F4";
    this.bomb   = false;
    this.state  = tileState.type.HIDDEN;
    this.surrounding  = 0;
    this.parent = parent;
}

Tile.prototype.Update = function(dt){
    if (this.bomb)
        this.color = "#FF0000"
    else
        this.color = "#5095F4";
}

Tile.prototype.Render = function(ctx){
    //console.log("Render")
    var t = tileState.type;
    var text = "";
    switch(this.state) {
        case t.SHOWN:
            if (this.bomb){
                this.color = "#FF0000"
                text = "";
            }
            else{
                //not a bomb
                this.color = "#5045F4";
                if (this.surrounding != 0)
                    text = "" + this.surrounding;
            }
            break;
        case t.HIDDEN:
            this.color = "#5095F4";
            break;
        default:
        alert("A tile is not in a valid state")
    }

    //Draw the rectangle
    ctx.fillStyle = this.color;
    ctx.fillRect(this.x, this.y, this.w, this.h);

    //Draw the text
    ctx.fillStyle = "#9F9FFF";
    ctx.font="20px Verdana";
    ctx.fillText(text,this.x+4,this.y+17);
}

Tile.prototype.Click = function(event){
    var rect = {x: this.x, y: this.y, w: this.w, h: this.h}
    if (collides(rect,event.offsetX,event.offsetY)){
        this.parent.ShowTile(this);
    }
}

Tile.prototype.GetBomb = function(){
    return this.bomb;
}

Tile.prototype.SetBomb = function(val){
    this.bomb = val;
}

Tile.prototype.SetState = function(state){
    this.state = state;
}

Tile.prototype.GetState = function(){
    return this.state;
}

Tile.prototype.SetSurrounding = function(val){
    this.surrounding = val;
}

Tile.prototype.GetSurrounding = function(){
    return this.surrounding;
}

Tile.prototype.GetNX = function(){
    return this.nx;
}

Tile.prototype.GetNY = function(){
    return this.ny;
}

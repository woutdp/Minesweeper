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
    this.mouseState  = tileMouseState.type.NONE;
    this.surrounding  = 0;
    this.text   = "";
    this.parent = parent;
    this.mouseDown = false;
}

Tile.prototype.Update = function(dt){
    var s = tileState.type;
    this.text = "";
    switch(this.state) {
        case s.SHOWN:
            if (this.bomb){
                this.color = tileColors["ShownBomb"];
                this.text = "";
            }
            else{
                //not a bomb
                this.color = tileColors["ShownNormal"];
                if (this.surrounding != 0)
                    this.text = "" + this.surrounding;
            }
            break;
        case s.HIDDEN:
                this.color = tileColors["HiddenNormal"];
            break;
        case s.FLAGGED:
            this.color = tileColors["FlaggedNormal"];
            break;
        default:
        alert("A tile is not in a valid state")
    }

    //Adjust colors for Hidden states
    if (this.state === s.HIDDEN){
        // if the mouse is hovering over the tile
        if (this.mouseState === tileMouseState.type.HOVER){
            this.color = tileColors["HiddenHover"];
        }

        // if the mouse is selecting (clicking on the tile) over the tile
        if(this.mouseState === tileMouseState.type.SELECTED){
            this.color = tileColors["HiddenSelectedLeftclick"]
        }

        if(this.mouseState === tileMouseState.type.RIGHTSELECTED){
            this.color = tileColors["FlaggedSelectedRightclick"]
        }
    }

    //Adjust colors for flagged states
    if (this.state === s.FLAGGED){
        // if the mouse is hovering over the tile
        if (this.mouseState === tileMouseState.type.HOVER){
            this.color = tileColors["FlaggedHover"];
        }

        if(this.mouseState === tileMouseState.type.RIGHTSELECTED){
            this.color = tileColors["FlaggedSelected"]
        }
    }
}

Tile.prototype.Render = function(ctx){
    //console.log("Render")
    //Draw the rectangle
    ctx.fillStyle = this.color;
    ctx.fillRect(this.x, this.y, this.w, this.h);

    //Draw the text
    ctx.fillStyle = "#FFFFFF";
    ctx.font= (this.w) + "px Verdana";
    ctx.fillText(this.text,this.x+4,this.y+17);
}

Tile.prototype.MouseUp = function(event){
    var rect = {x: this.x, y: this.y, w: this.w, h: this.h}
    if (collides(rect,event.offsetX,event.offsetY)){
        if (event.button === 0)
            this.parent.ShowTile(this);
        if (event.button === 2)
            this.parent.FlagTile(this);
        this.mouseState  = tileMouseState.type.NONE;
    }
}

Tile.prototype.MouseOver = function(event){
    var rect = {x: this.x, y: this.y, w: this.w, h: this.h}
    if (collides(rect,event.offsetX,event.offsetY)){
        if (this.mouseState != tileMouseState.type.SELECTED
        &&  this.mouseState != tileMouseState.type.RIGHTSELECTED)
            this.mouseState  = tileMouseState.type.HOVER;
    }else{
        this.mouseState  = tileMouseState.type.NONE;
    }
}

Tile.prototype.MouseDown = function(event){
    var rect = {x: this.x, y: this.y, w: this.w, h: this.h}
    if (collides(rect,event.offsetX,event.offsetY)){
        if (event.button === 0)
            this.mouseState  = tileMouseState.type.SELECTED;
        if (event.button === 2)
            this.mouseState  = tileMouseState.type.RIGHTSELECTED;
    }else{
        this.mouseState  = tileMouseState.type.NONE;
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

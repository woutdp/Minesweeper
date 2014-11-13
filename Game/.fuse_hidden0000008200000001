'use strict'
var Field = function(fieldX,fieldY, tileWidth, tileHeight, tileSpace, border){
    console.log('field created');

    //===================================
    // VARIABLES
    //===================================
    this.GenerateField(fieldX,fieldY,tileWidth, tileHeight, tileSpace, border);
}

Field.prototype.Update = function(dt){
    //console.log("Update")

    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
            this.tile[i][j].Update(dt);
        }
    }
}

Field.prototype.Render = function(ctx){
    //console.log("Render")

    //Fill in the background
    roundRect(ctx,0,0,this.GetWidth(),this.GetHeight(),20, true, false);
    ctx.fillStyle="#35373e";
    ctx.fill();

    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
            this.tile[i][j].Render(ctx);
        }
    }
}

Field.prototype.MouseUp = function(event){
    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
            this.tile[i][j].MouseUp(event);
        }
    }
}

Field.prototype.MouseOver = function(event){
    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
            this.tile[i][j].MouseOver(event);
        }
    }
}

Field.prototype.MouseDown = function(event){
    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
            this.tile[i][j].MouseDown(event);
        }
    }
}

Field.prototype.GenerateField = function(fieldX, fieldY, w, h , tileSpace, border){
    this.fieldX     = fieldX;
    this.fieldY     = fieldY;
    this.total      = fieldX * fieldY;
    this.tileWidth  = w;
    this.tileHeight = h;
    this.tileSpace  = tileSpace;
    this.border     = border;

    this.tile = [];

    for (var i = 0, l = fieldX; i < l; i++) {
        this.tile[i] = [];
        for (var j = 0, l2 = fieldY; j < l2; j++) {
            this.tile[i][j] = new Tile(i*(w+tileSpace)+this.border, j*(h+tileSpace)+this.border, w, h, i, j, this);
        }
    }

    this.GenerateBombs(this.total/11);
    this.SetSurroundingValues();
    //this.RevealAll();
}

Field.prototype.GenerateBombs = function(amount){
    var curAmount = 0;
    while (curAmount < amount){
        var x = parseInt(Math.random() * this.fieldX);
        var y = parseInt(Math.random() * this.fieldY);
        if (this.tile[x][y].GetBomb() === false){
            this.tile[x][y].SetBomb(true);
            curAmount += 1;
        }
    }
}

Field.prototype.RevealAll = function(){
    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
           this.tile[i][j].SetState(tileState.type.SHOWN);
        }
    }
}

Field.prototype.FlagTile = function(tile){
    var s = tile.GetState();
    if (s === tileState.type.HIDDEN){
        tile.SetState(tileState.type.FLAGGED);
    }
    else if (s === tileState.type.FLAGGED){
        tile.SetState(tileState.type.HIDDEN);
    }
}

Field.prototype.ShowTile = function(tile){
    var s = tile.GetState();
    if (s!= tileState.type.FLAGGED && tile.GetBomb() === true)
        this.GameOver();

    if (s != tileState.type.SHOWN && s != tileState.type.FLAGGED && tile.GetSurrounding() === 0 &&
        tile.GetBomb() === false){
        tile.SetState(tileState.type.SHOWN);
        this.RevealSurrounding(tile);
    }
    else if (s === tileState.type.HIDDEN){
        tile.SetState(tileState.type.SHOWN);
    }
}

Field.prototype.RevealSurrounding = function(tile){
    var x = tile.GetNX();
    var y = tile.GetNY();

    if (x != 0)
        this.ShowTile(this.tile[x-1][y]);
    if (x < this.fieldX-1)
        this.ShowTile(this.tile[x+1][y]);
    if (y != 0)
        this.ShowTile(this.tile[x][y-1]);
    if (y < this.fieldY-1)
        this.ShowTile(this.tile[x][y+1]);

    if (x != 0 && y != 0)
        this.ShowTile(this.tile[x-1][y-1]);
    if (x < this.fieldX-1 && y != 0)
        this.ShowTile(this.tile[x+1][y-1]);
    if (y < this.fieldY-1 && x != 0)
        this.ShowTile(this.tile[x-1][y+1]);
    if (y < this.fieldY-1 && x < this.fieldX - 1)
        this.ShowTile(this.tile[x+1][y+1]);
}

Field.prototype.SetSurroundingValues = function(){
    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
           this.tile[i][j].SetSurrounding(this.CalculateSurroundingTile(this.tile[i][j]));
        }
    }
}

Field.prototype.CalculateSurroundingTile = function(tile){
    var x = tile.GetNX();
    var y = tile.GetNY();

    var surrounding = 0;

    if (x != 0)
        if (this.tile[x-1][y].GetBomb()) surrounding += 1; //left
    if (x < this.fieldX-1)
        if (this.tile[x+1][y].GetBomb()) surrounding += 1; //right
    if (y != 0)
        if (this.tile[x][y-1].GetBomb()) surrounding += 1; //up
    if (y < this.fieldY-1)
        if (this.tile[x][y+1].GetBomb()) surrounding += 1; //down

    if (x != 0 && y != 0)
        if (this.tile[x-1][y - 1].GetBomb()) surrounding += 1; //left up
    if (x < this.fieldX-1 && y != 0)
        if (this.tile[x+1][y - 1].GetBomb()) surrounding += 1; //right up
    if (y < this.fieldY-1 && x != 0)
        if (this.tile[x - 1][y+1].GetBomb()) surrounding += 1; //left down
    if (y < this.fieldY-1 && x < this.fieldX - 1)
        if (this.tile[x + 1][y+1].GetBomb()) surrounding += 1; //right down

    return surrounding;
}

Field.prototype.GameOver = function(){
    this.RevealAll()
}

Field.prototype.GetWidth = function(){
    return this.fieldX*(this.tileWidth+this.tileSpace) - this.tileSpace + this.border*2;
}

Field.prototype.GetHeight = function(){
    return this.fieldY*(this.tileHeight+this.tileSpace) - this.tileSpace + this.border*2;
}

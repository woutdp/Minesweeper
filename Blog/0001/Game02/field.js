'use strict'
var Field = function(fieldX,fieldY, tileWidth, tileHeight, tileSpace, difficulty, border){
    console.log('field created');

    //===================================
    // VARIABLES
    //===================================
    this.GenerateField(fieldX,fieldY,tileWidth, tileHeight, tileSpace, border);
    this.firstClick = false;
    this.difficulty = difficulty;
    this.gameLost = false;
    this.gameWon = false;
}

Field.prototype.Update = function(dt){
    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
            this.tile[i][j].Update(dt);
        }
    }
}

Field.prototype.Render = function(ctx){
    //Fill in the background with a rounded rectangle
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

    this.Reset()
}

Field.prototype.GenerateBombs = function(amount, tile){
    var curAmount = 0;
    while (curAmount < amount){
        var x = parseInt(Math.random() * this.fieldX);
        var y = parseInt(Math.random() * this.fieldY);
        if (this.tile[x][y].GetBomb() === false && this.tile[x][y] != tile){
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
    //Generate a field of bombs on the first click,
    //where the first click is not the clicked tiles
    if (this.firstClick === false){
        this.GenerateBombs(this.total/this.difficulty, tile);
        this.SetSurroundingValues();
        this.firstClick = true;
    }

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

Field.prototype.Reset = function(){
    this.firstClick = false;
    this.gameLost = false;
    this.gameWon = false;
    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
            this.tile[i][j].SetBomb(false);
            this.tile[i][j].SetState(tileState.type.HIDDEN);
            //this.tile[i][j].Animate("popup");
            //this.tile[i][j].Animate("popup", (i+j)/90);
            //this.tile[i][j].Animate("popup", (i*j)/this.total);
            //this.tile[i][j].Animate("popup", Math.sqrt((i*j)/this.total));
            //this.tile[i][j].Animate("popup", (i/(j+4))/2);
            //this.tile[i][j].Animate("popup", Math.sqrt((i/(j+4))/2));
            //this.tile[i][j].Animate("popup", (j/(i+4))/2);
            //this.tile[i][j].Animate("popup", Math.sin((j*i)));
            //this.tile[i][j].Animate("popup", Math.random()*0.8);
            this.tile[i][j].Animate("popup", Math.sqrt(i+1 + Math.random()*10)*Math.sqrt(j+10)/20);

            //Sideways random
            //var h = 0.5 * Math.random()/20; //The smaller the bigger the circles
            //var w = 0.5 * Math.random()/20;
            //this.tile[i][j].Animate("popup", Math.sin(i*w)*Math.cos(j*h)*2);

            //math sqrt makes it go slow to fast
            //this.tile[i][j].Animate("popup", Math.sqrt(j+i));
        }
    }
}

Field.prototype.GameOver = function(){
    this.gameLost = true;
    this.RevealAll()
}

Field.prototype.IsGameWon = function(){
    if (this.gameWon) return true;
    if (this.gameLost) return false;

    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
            if (this.tile[i][j].GetBomb() === false && this.tile[i][j].GetState() === tileState.type.HIDDEN)
                return false;
        }
    }
    return true;
}

Field.prototype.GameWon = function(){
    this.Reset()
}

Field.prototype.GetWidth = function(){
    return this.fieldX*(this.tileWidth+this.tileSpace) - this.tileSpace + this.border*2;
}

Field.prototype.GetHeight = function(){
    return this.fieldY*(this.tileHeight+this.tileSpace) - this.tileSpace + this.border*2;
}

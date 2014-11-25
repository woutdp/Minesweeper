'use strict'
var Field = function(fieldX,fieldY, tileWidth, tileHeight, tileSpace, difficulty, borderW, borderH){
    //console.log('field created');
    //===================================
    // VARIABLES
    //===================================
    this.GenerateField(fieldX,fieldY,tileWidth, tileHeight, tileSpace, borderW, borderH);
    this.firstClick = false;
    this.difficulty = difficulty;
    this.gameLost = false;
    this.gameWon = false;
    this.animationDuration = 0.0;
    this.fieldClickable = true;

    this.countdown = 1.0;
}

Field.prototype.Update = function(dt){
    if (this.animationDuration > 0) this.animationDuration -= dt;
    else this.fieldClickable = true;

    this.countdown -= dt;
    if(this.countdown <= 0){
        this.countdown = 1;
        if (this.IsGameWon())
            this.GameWon();
    }

    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
            this.tile[i][j].Update(dt);
        }
    }
}

Field.prototype.Render = function(ctx){
    //Fill in the background with a rounded rectangle
    //roundRect(ctx,0,0,this.GetWidth(),this.GetHeight(), 20, true, false);
    ctx.fillStyle = "#444549";
    //roundRect(ctx,0,0,this.GetWidth(),this.GetHeight(),4, true, false);
    ctx.fill();
    ctx.fillStyle = "#444549";
    var extraB = 0;
    ctx.fillRect(this.borderw-extraB,this.borderh-extraB,this.GetWidth()-this.borderw*2+extraB*2,this.GetHeight()-this.borderh*2+extraB*2+5);

    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
            this.tile[i][j].Render(ctx);
        }
    }
}

Field.prototype.MouseUp = function(event){
    if (this.fieldClickable)
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
    if (this.fieldClickable)
    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
            this.tile[i][j].MouseDown(event);
        }
    }
}

Field.prototype.GenerateField = function(fieldX, fieldY, w, h , tileSpace, borderW, borderH){
    this.fieldX     = fieldX;
    this.fieldY     = fieldY;
    this.total      = fieldX * fieldY;
    this.tileWidth  = w;
    this.tileHeight = h;
    this.tileSpace  = tileSpace;
    this.borderw    = borderW;
    this.borderh    = borderH;

    this.tile = [];

    for (var i = 0, l = fieldX; i < l; ++i) {
        this.tile[i] = [];
        for (var j = 0, l2 = fieldY; j < l2; ++j) {
            this.tile[i][j] = new Tile(i*(w+tileSpace)+this.borderw, j*(h+tileSpace)+this.borderh, w, h, i, j, this);
        }
    }

    this.GenerateShowing();
}

Field.prototype.GenerateShowing = function(){
    var w = this.tileWidth;
    var h = this.tileHeight;

    for (var i = 0; i < this.fieldX; ++i) {
        for (var j = 0; j < this.fieldY; ++j) {
            //this.tile[i][j].Animate("popup", (i*j)/200);
            //this.tile[i][j].Animate("popup", Math.sqrt(Math.sin(i*4)*Math.cos(j*4))*((i*j)/j)*Math.random());
            //var h = 0.5 * Math.random()/20; //The smaller the bigger the circles
            //var w = 0.5 * Math.random()/20;
            var x = Math.abs(i - (this.fieldX/2));
            var y = Math.abs(j - (this.fieldY/2));
            this.tile[i][j].Animate("popup", (Math.sqrt(Math.cos(x*3)*y/10)+0.1),0.3);
            //this.tile[i][j].Animate("popup", Math.sqrt(Math.cos(x*3)*y/10), function(){console.log("yo, I'm done");});
            //this.tile[i][j].Animate("popup", (((Math.cos(x*w*3)+1.5))/((Math.sin(x*h*5)+1.5))/10)+0.2);
        }
    }
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
        tile.Animate("popinpopupsin");
    }
    else if (s === tileState.type.FLAGGED){
        tile.SetState(tileState.type.HIDDEN);
        tile.Animate("popinpopupver");
    }

    this.CheckColorFont(tile);
}

Field.prototype.CheckColorFont = function(tile){
    var array = this.GetSurroundingTiles(tile);
    var l = array.length;
    for (var i = 0; i < l; ++i){
        if(array[i].GetState() != tileState.type.HIDDEN){
            if(this.CalculateSurroundingTileForGuessed(array[i])){
                array[i].TransitionColorFont({r:122,g:122,b:122},3.0);
            }else{
                array[i].TransitionColorFont("original",0.2);
            }
        }
    }

    if(tile.GetState() != tileState.type.HIDDEN){
        if(this.CalculateSurroundingTileForGuessed(tile)){
            tile.TransitionColorFont({r:122,g:122,b:122},3.0);
        }else{
            tile.TransitionColorFont("original",0.2);
        }
    }
}

Field.prototype.ShowTile = function(tile, automatic){
    var automatic = automatic || -1;
    //Generate a field of bombs on the first click,
    //where the first click is not the clicked tiles
    if (this.firstClick === false && this.fieldClickable === true){
        this.GenerateBombs(this.total/this.difficulty, tile);
        this.SetSurroundingValues();
        this.firstClick = true;
    }

    var s = tile.GetState();
    if (s != tileState.type.FLAGGED && tile.GetBomb() === true)
        this.GameOver();

    if (s === tileState.type.HIDDEN && s != tileState.type.FLAGGED && tile.GetSurrounding() === 0 &&
        tile.GetBomb() === false && tile.IsBeingRevealed() === false && automatic != -1){
        var animtime = automatic;
        var delay = Math.random()*0.09;
        tile.Animate("popinpopupsin", delay, animtime,function(){this.isbeingrevealed = false; this.ResetDimensions();},function(){
            this.isbeingrevealed = true;
            if (this.framePercentage >= 0.5){
                this.SetState(tileState.type.SHOWN);
                this.parent.RevealSurrounding(tile);
            }
        });
    }else if (s === tileState.type.HIDDEN && s != tileState.type.FLAGGED && tile.GetSurrounding() === 0 &&
        tile.GetBomb() === false && tile.IsBeingRevealed() === false){
        var animtime = 0.4;
        tile.Animate("popinpopupsin", 0.0, animtime,function(){this.isbeingrevealed = false;},function(){
            this.isbeingrevealed = true;
            if (this.framePercentage >= 0.5){
                this.SetState(tileState.type.SHOWN);
                this.parent.RevealSurrounding(tile);
            }
        });
    }else if (s === tileState.type.HIDDEN && tile.IsBeingRevealed() === false){
        var animtime = 0.1;
        tile.Animate("popup", 0.0, animtime,function(){this.isbeingrevealed = false;},function(){
            this.isbeingrevealed = true;
            if (this.framePercentage >= 0.6){
                this.SetState(tileState.type.SHOWN);
            }
        });
    }

    this.CheckColorFont(tile);
}

Field.prototype.RevealSurrounding = function(tile){
    var x = tile.GetNX();
    var y = tile.GetNY();

    var acceleration = 0.8;
    var newAnimTime = tile.GetAnimtime()*acceleration;
    if (newAnimTime < 0.05) newAnimTime = 0.05;
    if (x != 0)
        this.ShowTile(this.tile[x-1][y], newAnimTime);
    if (x < this.fieldX-1)
        this.ShowTile(this.tile[x+1][y],newAnimTime);
    if (y != 0)
        this.ShowTile(this.tile[x][y-1],newAnimTime);
    if (y < this.fieldY-1)
        this.ShowTile(this.tile[x][y+1],newAnimTime);

    if (x != 0 && y != 0)
        this.ShowTile(this.tile[x-1][y-1], newAnimTime);
    if (x < this.fieldX-1 && y != 0)
        this.ShowTile(this.tile[x+1][y-1], newAnimTime);
    if (y < this.fieldY-1 && x != 0)
        this.ShowTile(this.tile[x-1][y+1], newAnimTime);
    if (y < this.fieldY-1 && x < this.fieldX - 1)
        this.ShowTile(this.tile[x+1][y+1], newAnimTime);
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

Field.prototype.CalculateSurroundingTileForGuessed = function(tile){
    var x = tile.GetNX();
    var y = tile.GetNY();

    if (x != 0)
        if (this.tile[x-1][y].GetState() === tileState.type.HIDDEN) return false; //left
    if (x < this.fieldX-1)
        if (this.tile[x+1][y].GetState() === tileState.type.HIDDEN) return false; //right
    if (y != 0)
        if (this.tile[x][y-1].GetState() === tileState.type.HIDDEN) return false; //up
    if (y < this.fieldY-1)
        if (this.tile[x][y+1].GetState() === tileState.type.HIDDEN) return false; //down

    if (x != 0 && y != 0)
        if (this.tile[x-1][y - 1].GetState() === tileState.type.HIDDEN) return false; //left up
    if (x < this.fieldX-1 && y != 0)
        if (this.tile[x+1][y - 1].GetState() === tileState.type.HIDDEN) return false; //right up
    if (y < this.fieldY-1 && x != 0)
        if (this.tile[x - 1][y+1].GetState() === tileState.type.HIDDEN) return false; //left down
    if (y < this.fieldY-1 && x < this.fieldX - 1)
        if (this.tile[x + 1][y+1].GetState() === tileState.type.HIDDEN) return false; //right down

    return true;
}

Field.prototype.GetSurroundingTiles = function(tile){
    var array = [];
    var it = 0;

    var x = tile.GetNX();
    var y = tile.GetNY();

    if (x != 0)
        array[it++] = this.tile[x-1][y];
    if (x < this.fieldX-1)
        array[it++] = this.tile[x+1][y];
    if (y != 0)
        array[it++] = this.tile[x][y-1];
    if (y < this.fieldY-1)
        array[it++] = this.tile[x][y+1];

    if (x != 0 && y != 0)
        array[it++] = this.tile[x-1][y - 1];
    if (x < this.fieldX-1 && y != 0)
        array[it++] = this.tile[x+1][y - 1];
    if (y < this.fieldY-1 && x != 0)
        array[it++] = this.tile[x - 1][y+1];
    if (y < this.fieldY-1 && x < this.fieldX - 1)
        array[it++] = this.tile[x + 1][y+1];

    return array;
}

Field.prototype.Reset = function(){
    this.fieldClickable = false;
    this.firstClick = false;
    this.gameLost = false;
    this.gameWon = false;
    var maxTime = 0.0;
    for (var i = 0, l = this.fieldX; i < l; i++) {
        for (var j = 0, l2 = this.fieldY; j < l2; j++) {
            var tile = this.tile[i][j];

            var delay = Math.sqrt(i+1 + Math.random()*10)*Math.sqrt(j+10)/20;
            var animtime = 0.4;
            tile.Animate("popinpopup", delay, animtime,undefined,function(){
                if (this.framePercentage >= 0.3){
                    this.Reset();
                }
            });

            if(delay + animtime > maxTime) maxTime = delay+ animtime;
        }
    }
    this.animationDuration = maxTime;
}

Field.prototype.Resize = function(x,y){
    this.GenerateField(x,y, this.tileWidth,this.tileHeight,this.tileSpace, this.borderw, this.borderh);
    this.Reset();
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
            var t = this.tile[i][j];
            if ((t.GetBomb() === false && t.GetState() === tileState.type.HIDDEN) ||
                (t.GetState() === tileState.type.FLAGGED && t.GetBomb() === false))
                return false;
        }
    }
    return true;
}

Field.prototype.GameWon = function(){
    this.Reset()
}

Field.prototype.GetWidth = function(){
    return this.fieldX*(this.tileWidth+this.tileSpace) - this.tileSpace + this.borderw*2;
}

Field.prototype.GetFieldY = function(){
    return this.fieldY;
}

Field.prototype.GetHeight = function(){
    return this.fieldY*(this.tileHeight+this.tileSpace) - this.tileSpace + this.borderh*2;
}

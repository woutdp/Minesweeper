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
    this.ox     = x; //Original x
    this.oy     = y; //Original y
    this.w      = w;
    this.h      = h;
    this.ow     = w; //Original width
    this.oh     = h; //Original height
    this.color  = tileColors["HiddenNormal"];
    this.bomb   = false;
    this.state  = tileState.type.HIDDEN;
    this.mouseState  = tileMouseState.type.NONE;
    this.surrounding  = 0;
    this.text   = "";
    this.parent = parent;
    this.mouseDown = false;

    //Animationstuff
    this.animation = "hold";
    this.animcounter = 0.0;
    this.animtime = 0.0;
    this.animdelay = 0.0;
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
        if (this.mouseState === tileMouseState.type.NONE){
            this.color = tileColors["HiddenNormal"];
            //this.Animate("hold");
            //this.ResetDimensions();
        }

        if (this.mouseState === tileMouseState.type.HOVER){
            this.color = tileColors["HiddenHover"];
            //this.Animate("spread");
        }

        // if the mouse is selecting (clicking on the tile) over the tile
        if(this.mouseState === tileMouseState.type.SELECTED){
            this.color = tileColors["HiddenSelectedLeftclick"]
        }

        if(this.mouseState === tileMouseState.type.RIGHTSELECTED){
            this.color = tileColors["HiddenSelectedRightclick"]
        }
    }

    //Adjust colors for flagged states
    if (this.state === s.FLAGGED){
        // if the mouse is hovering over the tile
        if (this.mouseState === tileMouseState.type.NONE){
            this.color = tileColors["FlaggedNormal"];
            //this.Animate("hold");
            //this.ResetDimensions();
        }

        if (this.mouseState === tileMouseState.type.HOVER){
            this.color = tileColors["FlaggedHover"];
            //this.Animate("spread");
        }

        if(this.mouseState === tileMouseState.type.SELECTED){
            this.color = tileColors["FlaggedSelectedLeftclick"]
        }

        if(this.mouseState === tileMouseState.type.RIGHTSELECTED){
            this.color = tileColors["FlaggedSelectedRightclick"]
        }
    }

    //Animations
    if (this.animdelay > 0) this.animdelay -= dt;
    if (this.animdelay <= 0)
        switch(this.animation) {
            case "hold":
                //Do nothing
                break;
            case "popup":
                var framePercentage = this.animcounter/this.animtime;

                if (framePercentage > 1){
                    this.Animate("hold");
                    this.ResetDimensions();
                }else{
                    this.w = this.ow * framePercentage;
                    this.h = this.oh * framePercentage;
                    this.x = this.ox + (this.ow - this.w)/2
                    this.y = this.oy + (this.oh - this.h)/2
                    this.animcounter += dt;
                    Invalidate();
                }
                break;
            case "popinpopup":
                var framePercentage = this.animcounter/this.animtime;

                if (framePercentage >= 0.5){
                    this.SetBomb(false);
                    this.SetState(tileState.type.HIDDEN);
                }

                if (framePercentage > 1){
                    this.Animate("hold");
                    this.ResetDimensions();
                }else{
                    this.w = this.ow * Math.abs((framePercentage*2 - 1));
                    this.h = this.oh * Math.abs((framePercentage*2 - 1));
                    this.x = this.ox + (this.ow - this.w)/2
                    this.y = this.oy + (this.oh - this.h)/2
                    this.animcounter += dt;
                    Invalidate();
                }
                break;
            case "spread":
                var framePercentage = this.animcounter/this.animtime;

                if (framePercentage > 1){
                    this.Animate("hold");
                }else{
                    this.w = this.ow + framePercentage*5;
                    this.h = this.oh + framePercentage*5;
                    this.x = this.ox + (this.ow - this.w)/2
                    this.y = this.oy + (this.oh - this.h)/2
                    this.animcounter += dt;
                    Invalidate();
                }
                break;
            default:
            alert("Unknown animation")
        }
}

Tile.prototype.Render = function(ctx){
    //console.log("Render")
    //Draw the rectangle
    ctx.fillStyle = this.color;
    ctx.fillRect(this.x, this.y, this.w, this.h);

    //Draw the text
    ctx.fillStyle = "#FFFFFF";
    var fontSize = parseInt((this.w+this.h)/2)
    ctx.font = "600 " + fontSize + "px Arial";
    ctx.textAlign="center";
    ctx.fillText(this.text,(this.x-(13*(this.w/this.ow))+fontSize),this.y-(3*(this.h/this.oh))+fontSize);
}

Tile.prototype.MouseUp = function(event){
    var rect = this.GetCollisionRect();
    if (collides(rect,event.offsetX,event.offsetY)){
        if (event.button === 0){
            this.parent.ShowTile(this);
        }
        if (event.button === 2)
            this.parent.FlagTile(this);
        this.mouseState  = tileMouseState.type.NONE;

        //Check if the game is won
        if(this.parent.IsGameWon())
            this.parent.GameWon();
    }
}

Tile.prototype.MouseOver = function(event){
    var rect = this.GetCollisionRect();
    if (collides(rect,event.offsetX,event.offsetY)){
        if (this.mouseState != tileMouseState.type.SELECTED
        &&  this.mouseState != tileMouseState.type.RIGHTSELECTED){
            this.mouseState  = tileMouseState.type.HOVER;
        }
    }else{
        this.mouseState  = tileMouseState.type.NONE;
    }
}

Tile.prototype.MouseDown = function(event){
    var rect = this.GetCollisionRect();
    if (collides(rect,event.offsetX,event.offsetY)){
        if (event.button === 0)
            this.mouseState  = tileMouseState.type.SELECTED;
        if (event.button === 2)
            this.mouseState  = tileMouseState.type.RIGHTSELECTED;
    }else{
        this.mouseState  = tileMouseState.type.NONE;
    }
}

Tile.prototype.Animate = function(animation, delay){
    this.animdelay = delay || 0;
    switch(animation) {
        case "hold":
            this.animation = "hold";
            break;
        case "popup":
            this.w = 0;
            this.h = 0;
            this.animcounter = 0.0;
            this.animtime = 0.4;
            this.animation = "popup";
            break;
        case "popinpopup":
            this.ResetDimensions();
            this.animcounter = 0.0;
            this.animtime = 0.4;
            this.animation = "popinpopup";
            break;
        case "spread":
            this.animcounter = 0.0;
            this.animtime = 0.3;
            this.animation = "spread";
            break;
        default:
        alert("Unknown animation")
    }
}

Tile.prototype.Reset = function(){
    //this.tile[i][j].Animate("popinpopup");
    //this.tile[i][j].Animate("popup", (i+j)/90);
    //this.tile[i][j].Animate("popup", (i*j)/this.total);
    //this.tile[i][j].Animate("popup", Math.sqrt((i*j)/this.total));
    //this.tile[i][j].Animate("popup", (i/(j+4))/2);
    //this.tile[i][j].Animate("popup", Math.sqrt((i/(j+4))/2));
    //this.tile[i][j].Animate("popup", (j/(i+4))/2);
    //this.tile[i][j].Animate("popup", Math.sin((j*i)));
    //this.tile[i][j].Animate("popup", Math.random()*0.8);

    //I like this one
    this.Animate("popinpopup", Math.sqrt(this.nx+1 + Math.random()*10)*Math.sqrt(this.ny+10)/20);

    //Sideways random
    //var h = 0.5 * Math.random()/20; //The smaller the bigger the circles
    //var w = 0.5 * Math.random()/20;
    //this.tile[i][j].Animate("popup", Math.sin(i*w)*Math.cos(j*h)*2);

    //math sqrt makes it go slow to fast
    //this.tile[i][j].Animate("popup", Math.sqrt(j+i));
}

Tile.prototype.ResetDimensions = function(){
    this.x = this.ox;
    this.y = this.oy;
    this.w = this.ow;
    this.h = this.oh;
    Invalidate();
}

Tile.prototype.GetCollisionRect = function(){
    return {x: this.ox, y: this.oy, w: this.ow, h: this.oh}
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

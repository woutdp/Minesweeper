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
    this.isbeingrevealed = false;
    this.framePercentage = 0.0;
    this.endfunction = function(){};
    this.conditionalfunction = function(){};

    //Font color stuff
    this.fontcolor = {r:200,g:200,b:200};
    this.startcolor = "";
    this.endcolor = "";
    this.fontFramePercentage = 0.0;
    this.fontcounter = 0.0;
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

    this.AnimationUpdate(dt);
    this.ColorFontUpdate(dt);
}

Tile.prototype.Render = function(ctx){
    //console.log("Render")
    //Draw the rectangle
    ctx.fillStyle = this.color;
    ctx.fillRect(this.x, this.y, this.w, this.h);

    //Draw the text
    ctx.fillStyle = "#FFFFFF";
    var fontSize = Math.min(this.w,this.h);
    ctx.font = "600 " + fontSize + "px Arial";
    ctx.textAlign="center";
    ctx.fillStyle = String(this.fontcolor);
    ctx.fillText(this.text,(this.x+(10*(this.w/this.ow))),this.y+(19*(this.h/this.oh)));
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

Tile.prototype.Animate = function(animation, delay, animTime, endFunction, conditionalFunction){
    this.animdelay = delay || 0;
    this.endfunction = endFunction || function(){this.Animate("hold");};
    this.conditionalfunction = conditionalFunction || function(){};
    this.animtime = animTime || tileAnimation.animTime;
    this.animation = animation;
    this.animcounter = 0.0;

    //Start values
    switch(animation) {
        case "hold":
            break;
        case "popup":
            this.w = 0;
            this.h = 0;
            break;
        case "popinpopup":
            break;
        case "popinpopuphor":
            this.w = this.ow;
            this.h = this.oh;
            break;
        case "popinpopupver":
            this.w = this.ow;
            this.h = this.oh;
            break;
        case "popinpopupsin":
            break;
        default:
        alert("Unknown animation")
    }
}

Tile.prototype.TransitionColorFont = function(color, transitionTime){
    this.startcolor = this.fontcolor;
    this.endcolor = color;
    this.gototime = transitionTime;
    this.fontcounter = 0;
}

Tile.prototype.ColorFontUpdate = function(dt){
    //this.fontcounter += dt;
    //this.fontFramePercentage = this.fontcounter/this.gototime;

    //Interpolate(this.fontFramePercentage,this.startcolor.r,this.endcolor.r);

    //this.fontcolor = this.startcolor*this.endcolor;
    //Invalidate();
}

Tile.prototype.AnimationUpdate = function(dt){
    //Animations
    if (this.animdelay > 0) this.animdelay -= dt;
    this.framePercentage = this.animcounter/this.animtime;

    if (this.animdelay <= 0)
        if (this.animation != "hold" && this.framePercentage > 1){
            this.endfunction();
        }else{
            this.conditionalfunction();
            switch(this.animation) {
                case "hold":
                    //Do nothing
                    break;
                case "popup":
                    this.w = this.ow * this.framePercentage;
                    this.h = this.oh * this.framePercentage;
                    this.x = this.ox + (this.ow - this.w)/2
                    this.y = this.oy + (this.oh - this.h)/2
                    this.animcounter += dt;
                    Invalidate();
                    break;
                case "popinpopup":
                    this.w = this.ow * Math.abs((this.framePercentage*2 - 1));
                    this.h = this.oh * Math.abs((this.framePercentage*2 - 1));
                    this.x = this.ox + (this.ow - this.w)/2
                    this.y = this.oy + (this.oh - this.h)/2
                    this.animcounter += dt;
                    Invalidate();
                    break;
                case "popinpopuphor":
                    this.w = this.ow * Math.abs((this.framePercentage*2 - 1));
                    //this.h = this.oh * Math.abs((this.framePercentage*2 - 1));
                    this.x = this.ox + (this.ow - this.w)/2
                    this.y = this.oy + (this.oh - this.h)/2
                    this.animcounter += dt;
                    Invalidate();
                    break;
                case "popinpopupver":
                    //this.w = this.ow * Math.abs((this.framePercentage*2 - 1));
                    this.h = this.oh * Math.abs((this.framePercentage*2 - 1));
                    this.x = this.ox + (this.ow - this.w)/2
                    this.y = this.oy + (this.oh - this.h)/2
                    this.animcounter += dt;
                    Invalidate();
                    break;
                case "popinpopupsin":
                    this.w = this.ow * Math.sin(Math.PI*Math.abs((this.framePercentage*2 - 1))/2);
                    this.h = this.oh * Math.sin(Math.PI*Math.abs((this.framePercentage*2 - 1))/2);
                    this.x = this.ox + (this.ow - this.w)/2
                    this.y = this.oy + (this.oh - this.h)/2
                    this.animcounter += dt;
                    Invalidate();
                    break;
                default:
                alert("Unknown animation")
            }
        }
}

Tile.prototype.Reset = function(){
    this.SetBomb(false);
    this.SetState(tileState.type.HIDDEN);
    this.isbeingrevealed = false;
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

Tile.prototype.GetAnimtime = function(){
    return this.animtime;
}

Tile.prototype.IsBeingRevealed = function(){
    return this.isbeingrevealed;
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

//ANIMIDEAS
    //this.tile[i][j].Animate("popinpopup");
    //this.tile[i][j].Animate("popup", (i+j)/90);
    //this.tile[i][j].Animate("popup", (i*j)/this.total);
    //this.tile[i][j].Animate("popup", Math.sqrt((i*j)/this.total));
    //this.tile[i][j].Animate("popup", (i/(j+4))/2);
    //this.tile[i][j].Animate("popup", Math.sqrt((i/(j+4))/2));
    //this.tile[i][j].Animate("popup", (j/(i+4))/2);
    //this.tile[i][j].Animate("popup", Math.sin((j*i)));
    //this.tile[i][j].Animate("popup", Math.random()*0.8);
    //Sideways random
    //var h = 0.5 * Math.random()/20; //The smaller the bigger the circles
    //var w = 0.5 * Math.random()/20;
    //this.tile[i][j].Animate("popup", Math.sin(i*w)*Math.cos(j*h)*2);

    //math sqrt makes it go slow to fast
    //this.tile[i][j].Animate("popup", Math.sqrt(j+i));

'use strict'

var Tile = function(x, y, w, h, nx, ny, parent){
    //console.log('tile created');
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
    this.backgroundColor  = '#444549';
    this.bomb   = false;
    this.state  = tileState.type.HIDDEN;
    this.mouseState  = tileMouseState.type.NONE;
    this.surrounding  = 0;
    this.text   = "";
    this.parent = parent;
    this.mouseDown = false;
    this.tileInvalid = true;   // component requires redrawing ?

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
    this.defaultfontcolor = {r:182,g:182,b:182};
    this.fontcolor = this.defaultfontcolor;
    this.startcolor = "";
    this.endcolor = "";
    this.fontFramePercentage = 0.0;
    this.gototime = -1;
    this.fontcounter = 0.0;
    this.fontcolordelay = 0.0;
}

Tile.prototype.Update = function(dt){
    this.AnimationUpdate(dt);
    this.ColorFontUpdate(dt);
}

Tile.prototype.Render = function(ctx){
    if(this.tileInvalid === false) return;

    var s = parseInt(1);
    var d = 2;
    var u = 1;
    var x = parseInt(this.x);
    var y = parseInt(this.y);
    var w = parseInt(this.w);
    var h = parseInt(this.h);
    var extraDown = 0;
    var extraDownBackground = 0;
    var scale = Math.min(this.w,this.h)/this.ow;

    if (this.ny+1 === this.parent.GetFieldY()){
        extraDown = 5*scale;
        extraDownBackground = 5;
    }

    if (this.state === tileState.type.SHOWN)
        d = 1;

    this.SetFillStyle(ctx, this.backgroundColor);
    ctx.fillRect(this.ox, this.oy, this.ow, this.oh+extraDownBackground);

    //Sides
    this.SetFillStyle(ctx, ColorLuminance(this.color, -0.1));
    ctx.fillRect(x, y, w, h+extraDown);

    //Upper
    this.SetFillStyle(ctx, ColorLuminance(this.color, 0.1));
    ctx.fillRect(x, y, w, u);

    //Inner
    this.SetFillStyle(ctx, this.color);
    var height = h-u;
    var width = w-(s*2);
    if (height < 0) height = 0;
    if (width < 0) width = 0;
    ctx.fillRect(x+s, y+u, width, height);

    //Downer
    if (extraDown > 1)
        this.SetFillStyle(ctx, ColorLuminance(this.color, -0.26));
    else
        this.SetFillStyle(ctx, ColorLuminance(this.color, -0.2));
    var test = Math.abs(h-d);
    ctx.fillRect(x, y+test, w, d+extraDown);

    //Draw the text
    if (this.text != ""){
        var fontSize = Math.min(this.w-2,this.h-2);
        this.SetFont(ctx, fontSize + "px Arial");
        ctx.textAlign="center";
        this.SetFillStyle(ctx,"rgb(" + this.fontcolor.r + "," + this.fontcolor.g + "," + this.fontcolor.b + ")");
        ctx.fillText(this.text,(this.x+(12*(this.w/this.ow))),this.y+(21*(this.h/this.oh)));
    }

    this.tileInvalid = false;
}

Tile.prototype.MouseUp = function(event){
    var rect = this.GetCollisionRect();
    if (collides(rect,event.offsetX,event.offsetY)){
        if (event.button === 0){
            this.parent.ShowTile(this);
        }
        if (event.button === 2)
            this.parent.FlagTile(this);

        this.SetMouseState(tileMouseState.type.NONE);

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
            this.SetMouseState(tileMouseState.type.HOVER);
        }
    }else{
        this.SetMouseState(tileMouseState.type.NONE);
    }
}

Tile.prototype.MouseDown = function(event){
    var rect = this.GetCollisionRect();
    if (collides(rect,event.offsetX,event.offsetY)){
        if (event.button === 0)
            this.SetMouseState(tileMouseState.type.SELECTED);
        if (event.button === 2)
            this.SetMouseState(tileMouseState.type.RIGHTSELECTED);
    }else{
        this.SetMouseState(tileMouseState.type.NONE);
    }
}

Tile.prototype.CorrectSquareColors = function(){
    var s = tileState.type;
    this.text = "";

    //Adjust colors for SHOWN states
    if (this.state === s.SHOWN){
        if (this.bomb){
            this.SetColor(tileColors["ShownBomb"]);
            this.text = "";
        }
        else{
            //not a bomb
            this.SetColor(tileColors["ShownNormal"]);
            if (this.surrounding != 0)
                this.text = "" + this.surrounding;
        }
    }

    //Adjust colors for Hidden states
    if (this.state === s.HIDDEN){
        if (this.mouseState === tileMouseState.type.NONE){
            this.SetColor(tileColors["HiddenNormal"]);
        }

        if (this.mouseState === tileMouseState.type.HOVER){
            this.SetColor(tileColors["HiddenHover"]);
        }

        if(this.mouseState === tileMouseState.type.SELECTED){
            this.SetColor(tileColors["HiddenSelectedLeftclick"]);
        }

        if(this.mouseState === tileMouseState.type.RIGHTSELECTED){
            this.SetColor(tileColors["HiddenSelectedRightclick"]);
        }
    }

    //Adjust colors for flagged states
    if (this.state === s.FLAGGED){
        if (this.mouseState === tileMouseState.type.NONE){
            this.SetColor(tileColors["FlaggedNormal"]);
        }

        if (this.mouseState === tileMouseState.type.HOVER){
            this.SetColor(tileColors["FlaggedHover"]);
        }

        if(this.mouseState === tileMouseState.type.SELECTED){
            this.SetColor(tileColors["FlaggedSelectedLeftclick"]);
        }

        if(this.mouseState === tileMouseState.type.RIGHTSELECTED){
            this.SetColor(tileColors["FlaggedSelectedRightclick"]);
        }
    }
}

Tile.prototype.TransitionColorFont = function(color, transitionTime, delay){
    if (color === "original") this.endcolor = this.defaultfontcolor;
    else this.endcolor = color;

    this.startcolor = this.fontcolor;
    this.gototime = transitionTime;
    this.fontcounter = 0;
    this.fontcolordelay = delay || 0;
}

Tile.prototype.ColorFontUpdate = function(dt){
    if (this.fontcolordelay > 0) this.fontcolordelay -= dt;

    if (this.gototime >= 0 && this.fontcolordelay <= 0){
        this.fontcounter += dt;
        this.fontFramePercentage = this.fontcounter/this.gototime;

        if (this.fontFramePercentage>=1){
            this.fontcolor = this.endcolor;
            this.gototime = -1;
        }else{
            var r = parseInt(Interpolate(this.fontFramePercentage,this.startcolor.r,this.endcolor.r));
            var g = parseInt(Interpolate(this.fontFramePercentage,this.startcolor.g,this.endcolor.g));
            var b = parseInt(Interpolate(this.fontFramePercentage,this.startcolor.b,this.endcolor.b));
            this.fontcolor = {r:r,g:g,b:b};
        }
        this.TileInvalidate();
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
            alert("Unknown animation");
    }
}

Tile.prototype.AnimationUpdate = function(dt){
    if (this.animdelay > 0) this.animdelay -= dt;
    this.framePercentage = this.animcounter/this.animtime;

    if (this.animdelay <= 0)
        if (this.animation != "hold" && this.framePercentage >= 1){
            this.endfunction();
            this.ResetDimensions();
            this.Animate("hold");
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
                    this.TileInvalidate();
                    break;
                case "popinpopup":
                    this.w = this.ow * Math.abs((this.framePercentage*2 - 1));
                    this.h = this.oh * Math.abs((this.framePercentage*2 - 1));
                    this.x = Math.round(this.ox + (this.ow - this.w)/2);
                    this.y = Math.round(this.oy + (this.oh - this.h)/2);
                    this.animcounter += dt;
                    this.TileInvalidate();
                    break;
                case "popinpopuphor":
                    this.w = this.ow * Math.abs((this.framePercentage*2 - 1));
                    //this.h = this.oh * Math.abs((this.framePercentage*2 - 1));
                    this.x = this.ox + (this.ow - this.w)/2;
                    this.y = this.oy + (this.oh - this.h)/2;
                    this.animcounter += dt;
                    this.TileInvalidate();
                    break;
                case "popinpopupver":
                    //this.w = this.ow * Math.abs((this.framePercentage*2 - 1));
                    this.h = this.oh * Math.abs((this.framePercentage*2 - 1));
                    this.x = this.ox + (this.ow - this.w)/2;
                    this.y = this.oy + (this.oh - this.h)/2;
                    this.animcounter += dt;
                    this.TileInvalidate();
                    break;
                case "popinpopupsin":
                    this.w = this.ow * Math.sin(Math.PI*Math.abs((this.framePercentage*2 - 1))/2);
                    this.h = this.oh * Math.sin(Math.PI*Math.abs((this.framePercentage*2 - 1))/2);
                    this.x = this.ox + (this.ow - this.w)/2;
                    this.y = this.oy + (this.oh - this.h)/2;
                    this.animcounter += dt;
                    this.TileInvalidate();
                    break;
                default:
                    alert("Unknown animation");
            }
        }
}

Tile.prototype.Reset = function(){
    this.SetBomb(false);
    this.SetState(tileState.type.HIDDEN);
    this.isbeingrevealed = false;
    this.fontcolor = this.defaultfontcolor;
    this.TileInvalidate();
}

Tile.prototype.ResetDimensions = function(){
    this.x = this.ox;
    this.y = this.oy;
    this.w = this.ow;
    this.h = this.oh;
    this.TileInvalidate();
}

Tile.prototype.GetCollisionRect = function(){
    return {x: this.ox+1, y: this.oy+1, w: this.ow-2, h: this.oh-2}
}

Tile.prototype.GetAnimtime = function(){
    return this.animtime;
}

Tile.prototype.IsBeingRevealed = function(){
    return this.isbeingrevealed;
}

Tile.prototype.SetFont = function(ctx, font){
    if (ctx.font != font){
        ctx.font = "600 " + font;
    }
}

Tile.prototype.SetColor = function(color){
    if (this.color != color){
        this.color = color;
        this.TileInvalidate();
    }
}

Tile.prototype.SetFillStyle = function(ctx, fillstyle){
    if (ctx.fillStyle != fillstyle){
        ctx.fillStyle = fillstyle;
    }
}

Tile.prototype.GetBomb = function(){
    return this.bomb;
}

Tile.prototype.SetBomb = function(val){
    this.bomb = val;
}

Tile.prototype.SetState = function(state){
    if (state != this.state){
        this.state = state;
        this.parent.CheckColorFont(this);
        this.CorrectSquareColors();
    }
}

Tile.prototype.SetMouseState = function(state){
    if (state != this.mouseState){
        this.mouseState = state;
        this.CorrectSquareColors();
    }
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

Tile.prototype.TileInvalidate = function(){
    this.tileInvalid = true;   // call this whenever the component state changes
    Invalidate();
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

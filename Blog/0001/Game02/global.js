'use strict'
//===================================
// GLOBAL FUNCTIONS AND ENUMERATORS
//===================================
function collides(rect, x, y){
    var isCollision = false;

    var left = rect.x, right = rect.x+rect.w;
    var top = rect.y, bottom = rect.y+rect.h;
    if (right >= x
        && left <= x
        && bottom >= y
        && top <= y) {
        isCollision = true;
    }

    return isCollision;
}

function Enum(){
    for( var i = 0; i < arguments.length; ++i ){
        this[arguments[i]] = i;
    }
    return this;
}

function roundRect(ctx, x, y, width, height, radius, fill, stroke) {
    if (typeof stroke == "undefined" ) {
        stroke = true;
    }
    if (typeof radius === "undefined") {
        radius = 5;
    }
    ctx.beginPath();
    ctx.moveTo(x + radius, y);
    ctx.lineTo(x + width - radius, y);
    ctx.quadraticCurveTo(x + width, y, x + width, y + radius);
    ctx.lineTo(x + width, y + height - radius);
    ctx.quadraticCurveTo(x + width, y + height, x + width - radius, y + height);
    ctx.lineTo(x + radius, y + height);
    ctx.quadraticCurveTo(x, y + height, x, y + height - radius);
    ctx.lineTo(x, y + radius);
    ctx.quadraticCurveTo(x, y, x + radius, y);
    ctx.closePath();
    if (stroke) {
        ctx.stroke();
    }
    if (fill) {
        ctx.fill();
    }
}

var invalid = true;   // component requires redrawing ?
function Invalidate() {
    invalid = true;   // call this whenever the component state changes
}

var tileState = {};
tileState.type = new Enum("SHOWN", "HIDDEN", "FLAGGED");

var tileMouseState = {};
tileMouseState.type = new Enum("NONE", "HOVER", "SELECTED", "RIGHTSELECTED");

var tileColors = {};
tileColors["ShownNormal"]               = "#5045F4";
tileColors["ShownBomb"]                 = "#FF5555";
tileColors["HiddenNormal"]              = "#40ABFF";
tileColors["HiddenHover"]               = "#50BBFF";
tileColors["HiddenSelectedLeftclick"]   = "#5045F9";
tileColors["HiddenSelectedRightclick"]  = "#8D838F";
tileColors["FlaggedNormal"]             = "#BDA34F";
tileColors["FlaggedHover"]              = "#CDB35F";
tileColors["FlaggedSelectedLeftclick"]  = "#BDA34F";
tileColors["FlaggedSelectedRightclick"] = "#DDD3DF";

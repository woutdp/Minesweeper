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

function ColorLuminance(hex, lum) {
    // validate hex string
    hex = String(hex).replace(/[^0-9a-f]/gi, '');
    if (hex.length < 6) {
        hex = hex[0]+hex[0]+hex[1]+hex[1]+hex[2]+hex[2];
    }
    lum = lum || 0;

    // convert to decimal and change luminosity
    var rgb = "#", c, i;
    for (i = 0; i < 3; i++) {
        c = parseInt(hex.substr(i*2,2), 16);
        c = Math.round(Math.min(Math.max(0, c + (c * lum)), 255)).toString(16);
        rgb += ("00"+c).substr(c.length);
    }

    return rgb;
}

function Interpolate(p, a, b){
    return (1-p)*a + p*b;
}

var invalid = true;   // component requires redrawing ?
function Invalidate() {
    invalid = true;   // call this whenever the component state changes
    //console.log("Invalidate")
}

var tileState = {};
tileState.type = new Enum("SHOWN", "HIDDEN", "FLAGGED");

var tileMouseState = {};
tileMouseState.type = new Enum("NONE", "HOVER", "SELECTED", "RIGHTSELECTED");

var tileColors = {};
tileColors["ShownNormal"]               = "#65666B";
tileColors["ShownBomb"]                 = "#E76458";
tileColors["HiddenNormal"]              = "#15E8D6";
tileColors["HiddenHover"]               = "#FFDE4A";
tileColors["HiddenSelectedLeftclick"]   = "#35373e";
tileColors["HiddenSelectedRightclick"]  = "#DDD3DF";
tileColors["FlaggedNormal"]             = "#FB702D";
tileColors["FlaggedHover"]              = "#CDB35F";
tileColors["FlaggedSelectedLeftclick"]  = "#BDA34F";
tileColors["FlaggedSelectedRightclick"] = "#FDF3FF";

var tileAnimation = {
    animTime : 0.3
};

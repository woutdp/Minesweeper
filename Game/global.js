'use strict'
//===================================
// GLOBAL FUNCTIONS
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

var tileState = {};
tileState.type = new Enum("SHOWN","HIDDEN");

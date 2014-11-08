var canvas = document.getElementById("game"),
ctx = canvas.getContext("2d");
ctx.fillStyle   = '#FF0000';  // red
ctx.strokeStyle = '#000000';  // black

var now, //the time since startup in milliseconds
    dt   = 0, // time between last frame
    last = Timestamp(), // time from the previous frame
    fps = 60, // fps cap
    step = 1/fps; // time passed in each frame in seconds

canvas.width
var fpsmeter = new FPSMeter({decimals: 0,
                            graph: true,
                            theme: 'dark',
                            });

// Gameloop function
function Frame() {
    // This should always be called at the beginning of Frame
    fpsmeter.tickStart();

    now = Timestamp();
    dt = dt + Math.min(1, (now - last) / 1000);
    while(dt > step) {
        dt = dt - step;
        Update(step);
    }
    Render(dt);
    last = now;

    fpsmeter.tick();

    requestAnimationFrame(Frame);
}
// Update function
function Update(dt){
    console.log(step)
}

// Rendering function
function Render(dt){
}

// Will return the time since startup
function Timestamp() {
  return window.performance && window.performance.now ? window.performance.now() : new Date().getTime();
}

// call frame for the first time
requestAnimationFrame(Frame);

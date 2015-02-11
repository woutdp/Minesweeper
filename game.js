'use strict'
var Game = {
    run: function(options){
        var now, //the time since startup in milliseconds
            dt      = 0, // time between last frame
            last    = Timestamp(), // time from the previous frame
            fps     = 60, // fps cap
            update  = options.update,
            render  = options.render,
            step    = 1/fps; // time passed in each frame in seconds

        // Gameloop function
        function Frame() {
            // This should always be called at the beginning of Frame
            now = Timestamp();
            dt = dt + Math.min(1, (now - last) / 1000);
            while(dt > step) {
                dt = dt - step;
                update(step);
            }
            render();
            last = now;

            requestAnimationFrame(Frame);
        }

        // Will return the time since startup
        function Timestamp() {
          return window.performance && window.performance.now ? window.performance.now()
            : new Date().getTime();
        }

        // call frame for the first time
        requestAnimationFrame(Frame);
    },
}

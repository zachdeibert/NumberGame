"use strict";

if ( typeof(Math) === "undefined" ) {
    Math = {};
}

Math.factorial = function(n) {
    var r = n > 0 ? 1 : 0;
    for ( var i = 2; i <= n; i++ ) {
        r *= i;
    }
    return r;
};

Math.nCr = function(n, r) {
    return Math.factorial(n) / (Math.factorial(r) * Math.factorial(n - r));
};

var keys = [37, 38, 39, 40];

function preventDefault(e) {
    e = e || window.event;
    if (e.preventDefault)
        e.preventDefault();
    e.returnValue = false;
}

function keydown(e) {
    for (var i = keys.length; i--;) {
        if (e.keyCode === keys[i]) {
            preventDefault(e);
            return;
        }
    }
}

function wheel(e) {
    preventDefault(e);
}

function disable_scroll() {
    if (window.addEventListener) {
        window.addEventListener('DOMMouseScroll', wheel, false);
    }
    window.onmousewheel = document.onmousewheel = wheel;
    document.onkeydown = keydown;
}

function enable_scroll() {
    if (window.removeEventListener) {
        window.removeEventListener('DOMMouseScroll', wheel, false);
    }
    window.onmousewheel = document.onmousewheel = document.onkeydown = null;
}


//  The "refresh" function implementations are identical
//  to our regular "JavaScript-Refresh" example.  The only
//  difference from our JavaScript Refresh example is
//  we do not have a doLoad function that starts our
//  refresh timer (since we use a refresh button).

var sURL = unescape(window.location.pathname);

function refresh()
{
    window.location.href = sURL;
}

function refresh()
{
    window.location.replace( sURL );
}

function refresh()
{
    window.location.reload( false );
}
var callback = arguments[0];
var previousHeight = 0;
function getCurrentConnectionsOnPage() {
    return $('button.bt-request-buffed:contains("Connect")');
}
function keepScroll() {
    var connections = getCurrentConnectionsOnPage();
    if (previousHeight < document.body.scrollHeight && connections.length<batchLimit)
    {
        previousHeight = document.body.scrollHeight;
        window.scrollTo(0, document.body.scrollHeight);
        setTimeout(function () { keepScroll(); }, 1000);
    } else {
        
        connections.click();
        setTimeout(function () { callback(connections.length.toString()); }, 1000);
    }
};
keepScroll();
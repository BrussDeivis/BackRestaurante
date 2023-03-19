/*  ********** Online / Offline Detection **********  */

// Request a small image at an interval to determine status
// ** Get a 1x1 pixel image here: http://www.1x1px.me/
// ** Use this code with an HTML element with id="status"

//const checkOnlineStatus = async () => {
//    try {

//        var xhr = new XMLHttpRequest();
//        var file = "https://i.imgur.com/7ofBNix.png";
//        var randomNum = Math.round(Math.random() * 10000);

//        xhr.open('HEAD', file + "?rand=" + randomNum, true);
//        //xhr.send();
//        await xhr.send();

//        //xhr.addEventListener("readystatechange", processRequest, false);

//        //function processRequest(e) {
//        //    if (xhr.readyState == 4) {
//        //        if (xhr.status >= 200 && xhr.status < 304) {
//        //            alert("connection exists!");
//        //        } else {
//        //            alert("connection doesn't exist!");
//        //        }
//        //    }
//        //}


//        //const online = await fetch("http://www.1x1px.me/1pixel.png");

//        return xhr.status >= 200 && xhr.status < 304; // either true or false
//    } catch (err) {
//        return false; // definitely offline
//    }
//};



//if (AplicacionDesplegadaLocalmente === true) {
// //cada media hora
//    setInterval(async () => {
//        const result = await checkOnlineStatus();
//        const statusDisplay = document.getElementById("OnlineStatus");
//        statusDisplay.textContent = result ? "Online" : "OFFline";
//    }, 3000); // probably too often, try 30000 for every 30 seconds
//};


//window.addEventListener("load", async (event) => {
//    const statusDisplay = document.getElementById("OnlineStatus");
//    statusDisplay.textContent = (await checkOnlineStatus())
//        ? "Online"
//        : "OFFline";
//});





if (AplicacionDesplegadaLocalmente === true) {
    //cada media hora
    setInterval(async () => {
        doesConnectionExist();
    }, (TiempoEnMinutosDeVerificacionDeAccesoAInternet * 60000)); // probably too often, try 30000 for every 30 seconds
};
function manejarPopPup() {
    var popup = document.getElementById("OnlineStatus");
    popup.classList.toggle("show");
}
function doesConnectionExist() {
    var xhr = new XMLHttpRequest();
    var file = "https://i.imgur.com/7ofBNix.png";
    var randomNum = Math.round(Math.random() * 10000);

    xhr.open('HEAD', file + "?rand=" + randomNum, true);
    xhr.send();

    xhr.addEventListener("readystatechange", processRequest, false);

    function processRequest(e) {
        if (xhr.readyState == 4) {
            if (xhr.status >= 200 && xhr.status < 304) {
                //var popup = document.getElementById("OnlineStatus");
                //popup.hide();
                //popup.classList.toggle("show");
            } else {
                var popup = document.getElementById("OnlineStatus");
                popup.classList.toggle("show");
                //alert("connection doesn't exist!");
            }
        }
    }
}
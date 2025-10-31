// ==UserScript==
// @name         GetSAPCRMContract
// @namespace    http://tampermonkey.net/
// @version      2025-07-07
// @description  try to take over the world!
// @author       mochen@foss.dk
// @match        http://foss219.foss.net:50000/*
// @icon         https://www.google.com/s2/favicons?sz=64&domain=tampermonkey.net
// @grant        GM_xmlhttpRequest
// @connect      iqxnet.my
// ==/UserScript==

(function () {
    'use strict';
    console.log("GetSAPCRMContract v1.0.1.4");
    let d1selector = 'div#C1_W1_V2';
    let div1 = document.querySelectorAll(d1selector)[0];

    if (div1 === undefined) {
        console.log("Unable to find " + d1selector);
        console.log("Exiting...");
        return;
    }
    console.log("[Success]:: Target iframe found, Injecting");


    let button =
        document.createElement('button');
    button.innerHTML = 'Get IQX Contract';
    button.id = 'btn-iqx-sync';

    button.addEventListener(
        'click',
        function () {
            let contentTds = document.querySelectorAll('div.ch-grid-of');
            for (let x = 0; x < contentTds.length; x++) {
                console.log("" + x + ", " + contentTds[x].innerText);




            }
            let shipToTds = document.querySelectorAll('td.th-clr-cel.th-clr-td.th-clr-pad.th-clr-cel-dis');
            for (let x = 0; x < shipToTds.length; x++) {
                console.log("" + x + ", " + shipToTds[x].innerText);




            }


        });




    div1.parentNode.insertBefore(button, div1);



    GM_xmlhttpRequest({
        method: "GET",
        url: "http://portal.iqxnet.my:5081/ewkiqxobd/api/Organisation",
        onload: function (response) {
            console.log(response.status);
            console.log("Connection to portal iqxnet is working normally.");
        },
        onerror: function (response) {
            console.log(response.status);
            console.log(response.responseText);
        },
        ontimeout: function (response) {
            console.log(response.status);
            console.log(response.responseText);
        }
    });

})();
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
    console.log("GetSAPCRMContract v1.0.1.6");
    let d1selector = 'div#C1_W1_V2';
    let div1 = document.querySelectorAll(d1selector)[0];

    if (div1 === undefined) {
        console.log("[WARNING]:: Unable to find " + d1selector);
        console.log("[WARNING]:: Exiting...");
        return;
    }
    console.log("[Success]:: Target iframe found, Injecting button element.");


    let button =
        document.createElement('button');
    button.innerHTML = 'Get IQX Contract';
    button.id = 'btn-iqx-sync';

    button.addEventListener(
        'click',
        function () {
            let contentTds = document.querySelectorAll('div.ch-grid-of');

            //Verify current page contains contract info, if yes, split out a contract object

            let syncContractReady = {};

            syncContractReady.GD = contentTds[0].innerText === 'General Data';
            syncContractReady.ID = contentTds[2].innerText === 'ID:';
            syncContractReady.DE = contentTds[5].innerText === 'Description:';

            let syncEquipmReady = {};
            syncEquipmReady.GD = contentTds[1].innerText === 'General Data';
            syncEquipmReady.ID = contentTds[3].innerText === 'Equipment ID:';
            syncEquipmReady.DE = contentTds[6].innerText === 'Equipment:';


            console.log(syncContractReady);
            console.log(syncEquipmReady);

            if (
                syncContractReady.GD &&
                syncContractReady.ID &&
                syncContractReady.DE
            ) {

                let oContract = {};
                oContract.ContractNumber = contentTds[3].innerText;
                oContract.Description = contentTds[6].innerText;
                oContract.SoldToParty = contentTds[9].innerText;
                oContract.CustomerContact = contentTds[12].innerText;
                oContract.EmployeeResponsible = contentTds[15].innerText;

                oContract.ContractStart = contentTds[54].innerText;
                oContract.ContractEnd = contentTds[58].innerText;

                console.log(oContract);
            }
            else if (
                syncEquipmReady.GD &&
                syncEquipmReady.ID &&
                syncEquipmReady.DE
            ) {
                let oContractObj = {};
                oContractObj.SerialNumber = contentTds[40].innerText;
                oContractObj.InstrumentType = contentTds[7].innerText;
                console.log(oContractObj);
            }
            else {
                for (let x = 0; x < contentTds.length; x++) {
                    console.log("" + x + ", " + contentTds[x].innerText);
                }
            }


            let shipToTds = document.querySelectorAll('td.th-clr-cel.th-clr-td.th-clr-pad.th-clr-cel-dis');

            //TODO: Need sync logic as below:
            //If the cell is "Ship-To Party", pos = n
            //n-2 = Account ID
            //n-1 = Partner name
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
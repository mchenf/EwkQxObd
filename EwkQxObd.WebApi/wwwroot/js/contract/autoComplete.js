// Auto complete scripts for contract new form filling
//author: mochen@foss.dk
//version: 1.0.0.0
//released: 6/1/2026

import { ensureFieldDisplayOn, ensureFieldDisplayOff } from './iterateField.js'


const Number = '#iptContractNumber';
const Description = 'rowDescription';

setInputEvent($(Number), checkContractNumber);

function setInputEvent(inputElement, checkObj, num) {
    let debounceTimer;
    inputElement.on('input', function () {

        clearTimeout(debounceTimer);

        const identifier = $(this).val().trim();


        if (identifier.length === 0) {
            resetForm();
            return;
        }

        debounceTimer = setTimeout(function () {
            checkObj(identifier, num)
        }, 500);
    });
}



function formatDate(date) {
    if (!date) return;
    var d = new Date(date);
    const yyyy = d.getFullYear().toString().padStart(4, "0");
    const MM = (d.getMonth() + 1).toString().padStart(2, "0");
    const dd = d.getDate().toString().padStart(2, "0");
    return `${yyyy}-${MM}-${dd}`

}

function formatDateTime(dateTime) {
    if (!dateTime) return;
    var d = new Date(dateTime);
    const yyyy = d.getFullYear().toString().padStart(4, "0");
    const MM = (d.getMonth() + 1).toString().padStart(2, "0");
    const dd = d.getDate().toString().padStart(2, "0");

    const HH = d.getHours().toString().padStart(2, "0");
    const mm = d.getMinutes().toString().padStart(2, "0");



    return `${yyyy}-${MM}-${dd} ${HH}:${mm}`

}



function checkObj(identifier, fetchUrl, fillAction) {
    if (!identifier) return;
    fetch(fetchUrl)
        .then(response => {
            return response.json();
        })
        .then(data => {
            fillAction(data);
        })
        .catch(e => {
            console.error('Error:', e);
        })
}

function checkContractNumber(contractNumber) {

    checkObj(contractNumber,
        '/ewkiqxobd/api/contract/'+ encodeURIComponent(contractNumber) + '/exist',
        (data) => {
            if (data === true) {
                ensureFieldDisplayOff(Description);
            }
            else {
                ensureFieldDisplayOn(Description);
            }
        }
    );
}

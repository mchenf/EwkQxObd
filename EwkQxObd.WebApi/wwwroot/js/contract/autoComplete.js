// Auto complete scripts for contract new form filling
//author: mochen@foss.dk
//version: 1.0.0.0
//released: 6/1/2026

import { ensureFieldDisplayOn, ensureFieldDisplayOff } from './iterateField.js'


const ContractNumber = '#iptContractNumber';
const Description = '#rowDescription';
const DesIpt = '#iptContractDesc';
const ValidFrom = '#rowValidFrom';
const ValidTo = '#rowValidTo';
//TODO: Progressive form filling to complete

setInputEvent($(ContractNumber), checkContractNumber);
setInputEvent($(DesIpt), checkDescription);

function setInputEvent(inputElement, checkObj, num) {
    let debounceTimer;
    inputElement.on('input', function () {

        clearTimeout(debounceTimer);

        const identifier = $(this).val().trim();


        if (identifier.length === 0) {
            return;
        }

        debounceTimer = setTimeout(function () {
            checkObj(identifier, num)
        }, 500);
    });
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

function checkDescription(DescriptionText) {

    if (!DescriptionText) return;
    if (DescriptionText.length > 5) {
        ensureFieldDisplayOn(ValidFrom);
        ensureFieldDisplayOn(ValidTo);
    }
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

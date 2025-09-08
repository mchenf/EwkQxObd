// Auto complete scripts for contract obj new form filling
//author: mochen@foss.dk
//version: 1.0.0.0
//released: 9/2/2025



setInputEvent($('#inpContract'), checkContractNumber);
setInputEvent($('#inpShpt'), checkShiptoAccount);
setInputEvent($('#iptUserEmail-1'), checkUser, 1);
setInputEvent($('#iptUserEmail-2'), checkUser, 2);

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

function checkObj(identifier, fetchUrl, fillAction) {
    if (!identifier) return;
    console.log("033 the url is", fetchUrl)
    fetch(fetchUrl)
        .then(response => response.json())
        .then(data => {
            console.log('040 getting data.', data);
            if (data) {
                console.log('050 data exists.');
                fillAction(data);
            }
        })
        .catch(e => {
            console.error('Error:', e);
        })
}

function checkContractNumber(contractNumber) {

    checkObj(contractNumber,
        '/ewkiqxobd/api/contract/' + encodeURIComponent(contractNumber),
        (data) => {
            $('#inpCtrDescription').val(data.description);

            $('#iptUserEmail-1').val(data.customerContact.emailAddress)
            $('#iptUserEmail-2').val(data.employeeResponsible.emailAddress)

            $('#inpCtrValidFrom').val(formatDate(data.validFrom));
            $('#inpCtrValidTo').val(formatDate(data.validTo));
        }
    );
}

function checkShiptoAccount(shiptoNo) {
    checkObj(shiptoNo,
        '/ewkiqxobd/api/account/' + encodeURIComponent(shiptoNo),
        (data) => {
            $('#inpShptPartnerName').val(data.partnerName);
            $('#inpShptRegion').val(data.region);
            $('#inpShptCountry').val(data.country);
        }
    );
}

function checkUser(email, num) {

    const userId = `#iptUserId-${num}`
    const userName = `#iptUserFullName-${num}`

    checkObj(email,
        '/ewkiqxobd/api/contactinfo/byemail/' + email,
        (data) => {
            $(userId).val(data.id);
            $(userName).val(data.fullName);
        }
    );
}

// Form validation for contracts
//author: mochen@foss.dk
//version: 1.0.0.0
//released: 6/8/2026


//When contract no is entered, check if already exist.
//YES => Display the contract as a card
//No => Validate the contract and display more forms to fill


console.log('Form Validation Script is running.');
setInputEvent($('#iptContractNo'), validateContract);



function validateContract(inputVal) {
    console.log('Validating Contract: ' + inputVal);
    if (!inputVal) return;

    if (!/^\d+$/.test(inputVal)) {
        console.warn('Invalid input: only digits allowed.');
        return;
    }

    if (inputVal.length < 4) {
        console.warn('Insufficient contract number length.');
        return;
    }

    let fetchUrl = '/ewkiqxobd/api/contract/match/?ContractNo=' + encodeURIComponent(inputVal);
    fetch(fetchUrl)
        .then(response => {
            return response.json();
        })
        .then(data => {
            console.log("Request success", data);
            if (data.length === 0) {
                $('#iptContractNo').addClass('is-valid');
                $('#iptContractNo').removeClass('is-invalid');
                $('#sectContractForm').removeClass('d-none');
            }
            else if (data.length > 0) {
                $('#iptContractNo').addClass('is-invalid');
                $('#iptContractNo').removeClass('is-valid');
                $('#sectContractForm').addClass('d-none');

                //TODO: Add more duplicate found logic
            }
        })
        .catch(e => {
            console.error('Error:', e);
        })
}

function setInputEvent(inputElement, validateMethod) {
    let debounceTimer;
    inputElement.on('input', function () {

        clearTimeout(debounceTimer);

        const inputVal = $(this).val().trim();


        if (inputVal.length === 0) {
            return;
        }

        debounceTimer = setTimeout(function () {
            validateMethod(inputVal)
        }, 500);
    });
}
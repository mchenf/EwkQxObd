
const buUrl = '/ewkiqxobd/api/ContractObject/BusinessUnits';
const instTypeUrl = '/ewkiqxobd/api/ContractObject/InstrumentType';

fillData();
const instrumentTypes = getData(instTypeUrl);


console.log('011 Received Business Unit:', instrumentTypes);


async function fillData() {
    let businessUnits;

    businessUnits = await getData(buUrl);

    console.log('010 Received Business Unit:', businessUnits);

    const $buSelect = $('#sltInstFamily');
    $buSelect.empty().append('<option value="">Choose BU</option>')

    for (let i = 0; i < businessUnits.length; i++) {
        const opt = $('<option>', {
            value: businessUnits[i],
            text: businessUnits[i]
        });
        $buSelect.append(opt)
    }
}

async function getData(fetchUrl) {

    return fetch(fetchUrl)
        .then(response => response.json())
        .then(data => {
            console.log('040 getting data.', data.values);
            if (data) {
                console.log('050 data exists.');
                return data.values;
            }
        })
        .catch(e => {
            console.error('Error:', e);
        })
}


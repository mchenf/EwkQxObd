
const buUrl = '/ewkiqxobd/api/ContractObject/BusinessUnits';
const instTypeUrlBase = '/ewkiqxobd/api/ContractObject/InstrumentType';

fillData();


$('#sltInstFamily').on('change',
    async function () {

        let instTypeUrl = instTypeUrlBase + "/" + $('#sltInstFamily').val();

        console.log("072 Getting InstTypeUrl: ", instTypeUrl)
        console.log("073 Getting ElementObj: ", $('#sltInstFamily').val())
        let instrumentTypes = await getData(instTypeUrl);

        console.log('011 Received Business Unit:', instrumentTypes);
        $('#sltInstType').empty().append('<option value="">Select Instrument Type</option>');
        for (let i = 0; i < instrumentTypes.length; i++) {
            const opt = $('<option>', {
                value: instrumentTypes[i].instrumentTypeID,
                text: instrumentTypes[i].name
            });
            $('#sltInstType').append(opt)
        }

    }
)









async function fillData() {
    let businessUnits = await getData(buUrl);

    console.log('010 Received Business Unit:', businessUnits);

    const $buSelect = $('#sltInstFamily');
    $buSelect.empty().append('<option value="">Select Business Unit</option>')

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


// Allowing a button's click to copy to clipboard
//author: mochen@foss.dk
//version: 1.0.0.0
//released: 9/9/2025


$('#btnCopy2clipB').click(() => {
    copToClipboard();
});

function copToClipboard() {
    var text2Cpy = $('#txaCondensedConObj').val()

    navigator.clipboard.writeText(text2Cpy)
    .then(() => {
        console.log('Clipboard text copied');
    }).then(() => {
        alert('Content copied...');
    }).catch(err => {
        console.error('Failed to copy: ', err);
    });

}


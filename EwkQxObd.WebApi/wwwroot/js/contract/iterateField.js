// Auto complete scripts for contract new form filling
//author: mochen@foss.dk
//version: 1.0.0.0
//released: 6/1/2026

//OBSOLETE

export function ensureFieldDisplayOn(elmTag) {
    let elm = $(elmTag);
    if (elm.length === 0) {
        throw new Error(`Element with ID '${elmTag}' cannot be found.`);
    }
    if (elm.hasClass('d-none')) {
        elm.removeClass('d-none');
    }
}

export function ensureFieldDisplayOff(elmTag) {
    let elm = $(elmTag);
    if (elm.length === 0) {
        throw new Error(`Element with ID '${elmTag}' cannot be found.`);
    }
    if (!elm.hasClass('d-none')) {
        elm.addClass('d-none');
    }
}



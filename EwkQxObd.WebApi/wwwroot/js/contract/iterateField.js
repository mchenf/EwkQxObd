// Auto complete scripts for contract new form filling
//author: mochen@foss.dk
//version: 1.0.0.0
//released: 6/1/2026


export function toggleFieldDisplay(elmTag) {
    let elm = document.getElementById(elmTag);
    if (elm === null) {
        throw new Error(`Element with ID '${elmTag}' cannot be found.`);
    }
    if (elm.classList.contains('d-none')) {
        elm.classList.remove('d-none');
    }
    else {
        elm.classList.add('d-none');
    }
}

export function ensureFieldDisplayOn(elmTag) {
    let elm = document.getElementById(elmTag);
    if (elm === null) {
        throw new Error(`Element with ID '${elmTag}' cannot be found.`);
    }
    if (elm.classList.contains('d-none')) {
        elm.classList.remove('d-none');
    }
}

export function ensureFieldDisplayOff(elmTag) {
    let elm = document.getElementById(elmTag);
    if (elm === null) {
        throw new Error(`Element with ID '${elmTag}' cannot be found.`);
    }
    if (!elm.classList.contains('d-none')) {
        elm.classList.add('d-none');
    }
}



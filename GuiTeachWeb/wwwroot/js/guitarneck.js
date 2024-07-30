

function adjustNeck() {
    let svg_right_margin = 50;
    let svg = document.getElementById('neckSvg');
    let rect = document.getElementById('fingerBoard');
    let pageWidth = window.innerWidth;
    // Example: Adjust the rectangle width based on the page width     
    svg.setAttribute('width', pageWidth - svg_right_margin);    
    let lastFretId = lastVisibleFret();
    let lastFret = document.getElementById(lastFretId);
    let lastFretX = parseInt(lastFret.getAttribute('x1'));
    let newFingerBoardWidth = lastFretX - 20;
    // Adjust the fingerboard width based on the last visible fret
    rect.setAttribute('width', newFingerBoardWidth);
    return lastFretId;
}

function updateFrets(fb_height) {
    let frets = document.querySelectorAll('line.fret');
    let fb_top = parseInt(document.getElementById('fingerBoard').getAttribute('y'));
    let fret_count = frets.length;
    for (let i = 0; i < fret_count; i++) {
        let fretY = parseInt(frets[i].getAttribute('y1'));
        frets[i].setAttribute('y2', fretY + fb_height);
    }
}

function updateInlays() {
    let inlays = document.querySelectorAll('circle.inlay');
    let inlay_count = inlays.length;
    let newMidLine = parseInt(parseInt(document.getElementById('neckSvg').getAttribute('height')) / 2) + 5;
    for (let i = 0; i < inlay_count; i++) {
        let inlayId = inlays[i].getAttribute('id');
        if (inlayId == 'inlay12-1' || inlayId == 'inlay24-1') {
            inlays[i].setAttribute('cy',  (newMidLine - 50).toString());
        } else if (inlayId == 'inlay12-2' || inlayId == 'inlay24-2') {
            inlays[i].setAttribute('cy', (newMidLine + 50).toString());
        } else {
            inlays[i].setAttribute('cy', (newMidLine).toString());
        }
    }
};
        
/// <summary>
/// Adjust the neck width (rectangle "Height") based on the number of strings
/// does not currently support Bass guitar.
/// </summary>
function resetNeckHeight() {
    let svg = document.getElementById('neckSvg');
    let rect = document.getElementById('fingerBoard');
    let nut = document.getElementById('nut');
    let guitarStrings = document.querySelectorAll('line.guitar_string');    
    let fb_string_spacing = 30;
    let fb_edge_margin = 15;
    let stringCount = guitarStrings.length;
    let fb_StringAreaHeight = stringCount * fb_string_spacing;
    let fb_final_height = fb_StringAreaHeight + (fb_edge_margin * 2)
    rect.setAttribute('height', fb_final_height);
    nut.setAttribute('height', fb_final_height);    
    svg.setAttribute('height', fb_final_height - 20);
    updateFrets(fb_final_height);
    updateInlays();
}



function lastVisibleFret() {
    let frets = document.querySelectorAll('line.fret');
    let lastFret = frets[0];
    let svg_right_margin = 50;
    for (var i = 1; i < frets.length; i++) {
        if (frets[i].getAttribute('x1') < (window.innerWidth - svg_right_margin)) {
            lastFret = frets[i];
        }
    }
    return lastFret.getAttribute('id');
}

function addStringToNeck() {
    let guitarStrings = document.querySelectorAll('line.guitar_string');
    let fb_top = parseInt(document.getElementById('fingerBoard').getAttribute('y'));
    let fb_string_spacing = 30;
    let fb_edge_margin = 15;
    let stringCount = guitarStrings.length;
    let fb_string_area_height = stringCount * fb_string_spacing;
    let stringY = fb_top + fb_edge_margin + fb_string_area_height;
    let newString = document.createElementNS('http://www.w3.org/2000/svg', 'line');
    newString.setAttribute('class', 'guitar_string');
    newString.setAttribute('id', 'string' + stringCount);
    newString.setAttribute('x1', 10);    
    newString.setAttribute('y1', stringY);
    newString.setAttribute('x2', 1490);
    newString.setAttribute('y2', stringY);
    newString.setAttribute('stroke', 'black');
    newString.setAttribute('stroke-width', 2);
    document.getElementById('neckSvg').appendChild(newString);
    resetNeckHeight();
}

function removeStringFromNeck() {
    let guitarStrings = document.querySelectorAll('line.guitar_string');
    let stringCount = guitarStrings.length;
    if (stringCount > 1) {
        guitarStrings[stringCount - 1].remove();
        resetNeckHeight();
    }
}

function reStringNeck(targetStringCount) {
    let guitarStrings = document.querySelectorAll('line.guitar_string');
    let fb_top = parseInt(document.getElementById('fingerBoard').getAttribute('y'));
    let stringCount = guitarStrings.length;
    let fb_edge_margin = parseInt(fb_string_spacing / 2);
    let stringY = fb_top + fb_edge_margin;
    if (targetStringCount > stringCount) {
        for (let i = stringCount; i < targetStringCount; i++) {
            let newString = document.createElementNS('http://www.w3.org/2000/svg', 'line');
            newString.setAttribute('class', 'guitar_string');
            newString.setAttribute('id', 'string' + i);
            newString.setAttribute('x1', 20);
            newString.setAttribute('x2', 20);
            newString.setAttribute('y1', stringY);
            newString.setAttribute('y2', stringY);
            newString.setAttribute('stroke', 'black');
            newString.setAttribute('stroke-width', 2);
            document.getElementById('neckSvg').appendChild(newString);
            stringY += fb_string_spacing;
        }
    } else if (targetStringCount < stringCount) {
        for (let i = stringCount - 1; i >= targetStringCount; i--) {
            guitarStrings[i].remove();
        }
    }

    resetNeckHeight();
}

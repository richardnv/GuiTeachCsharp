
// Constructor function for GuitarNeck
function GuitarNeck(fretCount, tuningMidiNumbers) {
    this.fretCount = fretCount;
    this.tuningMidiNumbers = tuningMidiNumbers;
    this.svg = null;
    this.fingerBoard = null;
    this.nut = null;
    this.string_overlap_length_behind_nut = 10;
    this.lastFret = null;    
}

GuitarNeck.prototype.createGuitarNeckSVG = function() {
    const sc = Object.keys(this.tuningMidiNumbers).length;
    const svgNS = "http://www.w3.org/2000/svg";
    this.svg = document.createElementNS(svgNS, "svg");
    this.svg.setAttribute("id", "neckSvg");
    this.svg.setAttribute("width", "1500");
    this.svg.setAttribute("height", "200");

    this.fingerBoard = document.createElementNS(svgNS, "rect");
    this.fingerBoard.setAttribute("id", "fingerBoard");
    this.fingerBoard.setAttribute("x", "20");
    this.fingerBoard.setAttribute("y", "10");
    this.fingerBoard.setAttribute("width", "1500");
    this.fingerBoard.setAttribute("height", "180");
    this.fingerBoard.setAttribute("fill", "saddlebrown");    
    this.svg.appendChild(this.fingerBoard);

    this.nut = document.createElementNS(svgNS, "rect");
    this.nut.setAttribute("id", "nut");
    this.nut.setAttribute("x", "20");
    this.nut.setAttribute("y", "10");
    this.nut.setAttribute("width", "20");
    this.nut.setAttribute("height", "180");
    this.nut.setAttribute("fill", "bone");
    this.svg.appendChild(this.nut);

    for (let i = 0; i <= this.fretCount; i++) {
        let fret = document.createElementNS(svgNS, "line");
        fret.setAttribute("class", "fret");
        fret.setAttribute("id", `f${i}`);
        fret.setAttribute("x1", 20 + 60 * i);
        fret.setAttribute("y1", "10");
        fret.setAttribute("x2", 20 + 60 * i);
        fret.setAttribute("y2", "190");
        fret.setAttribute("stroke", "silver");
        fret.setAttribute("stroke-width", "5");
        this.lastFret = fret.cloneNode();
        this.svg.appendChild(fret);
    }

    const inlays = [3, 5, 7, 9, 12, 15, 17, 19, 21, 24];
    inlays.forEach((inlay, index) => {
        let circle = document.createElementNS(svgNS, "circle");
        circle.setAttribute("class", "inlay");
        circle.setAttribute("id", `inlay${inlay}`);
        circle.setAttribute("cx", 20 + 60 * inlay - 30);
        circle.setAttribute("cy", inlay === 12 || inlay === 24 ? "50" : "100");
        circle.setAttribute("r", "5");
        circle.setAttribute("fill", "white");
        this.svg.appendChild(circle);
        if (inlay === 12 || inlay === 24) {
            let circle2 = circle.cloneNode();
            circle2.setAttribute("cy", "150");
            this.svg.appendChild(circle2);
        }
    });

    for (let i = 0; i < sc; i++) {
        let string = document.createElementNS(svgNS, "line");
        let fbY = parseInt(this.fingerBoard.getAttribute("y"));        
        let fbHeight = parseInt(this.fingerBoard.getAttribute("height"));
        let stringOffset = fbHeight / sc;
        let startOffset = stringOffset / 2;
        string.setAttribute("id", `string${i}`);
        string.setAttribute("class", "guitar_string");
        string.setAttribute("x1", (10 - this.string_overlap_length_behind_nut));
        string.setAttribute("y1", (stringOffset * i) + startOffset + fbY);
        string.setAttribute("x2", (1490 + this.string_overlap_length_behind_nut));
        string.setAttribute("y2", (stringOffset * i) + startOffset + fbY);
        string.setAttribute("stroke", "black");
        string.setAttribute("stroke-width", "2");
        this.svg.appendChild(string);
        let stringY = parseInt(string.getAttribute("y1"));
        let rootNote = this.tuningMidiNumbers[i];        
        // let effectiveFretCount =  (this.lastFret.getAttribute('id'). < this.fretCount ? this.lastFretIndex : this.fretCount);
        for (let f = 0; f <= this.fretCount; f++) {
            let fret = this.svg.querySelector(`#f${f}`);
            let fretX = parseInt(fret.getAttribute("x1"));
            let note = rootNote + f;
            let noteCircle = document.createElementNS(svgNS, "circle");
            noteCircle.setAttribute("class", "note nn" + note + " string" + i + " fret" + f);
            noteCircle.setAttribute("id", `note${note}s${i}f${f}`);            
            noteCircle.setAttribute("cx", fretX - 15);
            noteCircle.setAttribute("cy", stringY);
            noteCircle.setAttribute("r", "15"); 
            noteCircle.setAttribute("fill", "gray");
            this.svg.appendChild(noteCircle);
        }
    }
   
    return this.svg;
};

GuitarNeck.prototype.adjustNeckWidth = function() {
    let svg_right_margin = 50;
    let pageWidth = window.innerWidth;
    // Example: Adjust the rectangle width based on the page width     
    this.svg.setAttribute('width', pageWidth - svg_right_margin);    
    this.lastVisibleFret();
    let lastFretX = parseInt(this.lastFret.getAttribute('x1'));
    let newFingerBoardWidth = lastFretX - 10;
    // Adjust the fingerboard width based on the last visible fret
    this.fingerBoard.setAttribute('width', newFingerBoardWidth);
    return this.lastFret.getAttribute('id');
}

GuitarNeck.prototype.updateFrets = function() {
    let frets = this.svg.querySelectorAll('line.fret');
    let fb_height = parseInt(this.fingerBoard.getAttribute('height'));
    let fret_count = frets.length;
    for (let i = 0; i < fret_count; i++) {
        let fretY = parseInt(frets[i].getAttribute('y1'));
        frets[i].setAttribute('y2', fretY + fb_height);
    }
}

GuitarNeck.prototype.updateInlays = function() {
    let inlays = this.svg.querySelectorAll('circle.inlay');
    let inlay_count = inlays.length;
    let newMidLine = parseInt(parseInt(this.svg.getAttribute('height')) / 2) + 5;
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

// Update the notes on the neck based on the tuning, fret count, and lastVisibleFret
GuitarNeck.prototype.updateNotes = function() {
    

}
        
/// <summary>
/// Adjust the neck width (rectangle "Height") based on the number of strings
/// does not currently support Bass guitar.
/// </summary>
GuitarNeck.prototype.resetNeckHeight = function() {    
    let guitarStrings = this.svg.querySelectorAll('line.guitar_string');    
    let fb_string_spacing = 30;
    let fb_edge_margin = 15;
    let stringCount = guitarStrings.length;
    let fb_StringAreaHeight = stringCount * fb_string_spacing;
    let fb_final_height = fb_StringAreaHeight + (fb_edge_margin * 2)
    this.fingerBoard.setAttribute('height', fb_final_height);
    this.nut.setAttribute('height', fb_final_height);    
    this.svg.setAttribute('height', fb_final_height - 20);
    this.updateFrets();
    this.updateInlays();
}

GuitarNeck.prototype.lastVisibleFret = function() {
    let frets = this.svg.querySelectorAll('line.fret');    
    let svg_right_margin = 50;
    for (var i = 1; i < frets.length; i++) {
        if (frets[i].getAttribute('x1') < (window.innerWidth - svg_right_margin)) {
            this.lastFretId = frets[i];
            this.lastFretIndex = i;
        }
    }    
}

GuitarNeck.prototype.addStringToNeck = function() {
    let guitarStrings = this.svg.querySelectorAll('line.guitar_string');
    let fb_top = parseInt(this.fingerBoard.getAttribute('y'));
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
    this.resetNeckHeight();
}

GuitarNeck.prototype.removeStringFromNeck = function() {
    let guitarStrings = this.svg.querySelectorAll('line.guitar_string');
    let stringCount = guitarStrings.length;
    if (stringCount > 1) {
        guitarStrings[stringCount - 1].remove();
        this.resetNeckHeight();
    }
}

// Add all notes to the fingerboard for each string and fret 
// based on each strings tuning.
/*function reNoteNeck(tuning) {
    let guitarStrings = document.querySelectorAll('line.guitar_string');
    let stringCount = guitarStrings.length;
    let fb_top = parseInt(document.getElementById('fingerBoard').getAttribute('y'));
    let fb_string_spacing = 30;
    let fb_edge_margin = 15;
    let stringY = fb_top + fb_edge_margin;
    let fb_height = stringCount * fb_string_spacing;
    let fb_bottom = fb_top + fb_height;
    let fb_width = parseInt(document.getElementById('fingerBoard').getAttribute('width'));
    let fretCount = 24;
    let fretWidth = fb_width / fretCount;
    let fretY = fb_top;
    let nut = document.getElementById('nut');
    let nutY = parseInt(nut.getAttribute('y'));
    let nutHeight = parseInt(nut.getAttribute('height'));
    let nutWidth = parseInt(nut.getAttribute('width'));
    let nutX = parseInt(nut.getAttribute('x'));
    let noteY = nutY + (nutHeight / 2) + 5;
    let noteX = nutX + 5;
    let noteSpacing = 30;
    let noteRadius = 10;
    let noteColor = 'black';
    let noteStroke = 'black';
    let noteStrokeWidth = 2;
    let noteClass = 'note';
    let noteId = '';
    let noteText = '';
    let noteTextClass = 'noteText';
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
    */
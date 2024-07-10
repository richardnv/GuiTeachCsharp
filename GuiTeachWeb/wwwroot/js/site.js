// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function createGuitarNeckSVG() {
    // Create the SVG element
    const svgNS = "http://www.w3.org/2000/svg";
    let svg = document.createElementNS(svgNS, "svg");
    svg.setAttribute("id", "neckSvg");
    svg.setAttribute("width", "1500");
    svg.setAttribute("height", "200");
    // svg.setAttributeNS(null, "xmlns", svgNS);

    let svgStringCount = 6;

    // Guitar neck
    let fingerBoard = document.createElementNS(svgNS, "rect");
    fingerBoard.setAttribute("id", "fingerBoard");
    fingerBoard.setAttribute("x", "20");
    fingerBoard.setAttribute("y", "10");
    fingerBoard.setAttribute("width", "1500");
    fingerBoard.setAttribute("height", "180");
    fingerBoard.setAttribute("fill", "saddlebrown");
    svg.appendChild(fingerBoard);

    // Nut
    let nut = document.createElementNS(svgNS, "rect");
    nut.setAttribute("id", "nut");
    nut.setAttribute("x", "20");
    nut.setAttribute("y", "10");
    nut.setAttribute("width", "20");
    nut.setAttribute("height", "180");
    nut.setAttribute("fill", "bone");
    svg.appendChild(nut);

    // Frets
    for (let i = 1; i <= 24; i++) {
        let fret = document.createElementNS(svgNS, "line");
        fret.setAttribute("class", "fret");
        fret.setAttribute("id", `f${i}`);
        fret.setAttribute("x1", 20 + 60 * i);
        fret.setAttribute("y1", "10");
        fret.setAttribute("x2", 20 + 60 * i);
        fret.setAttribute("y2", "190");
        fret.setAttribute("stroke", "silver");
        fret.setAttribute("stroke-width", "5");
        svg.appendChild(fret);
    }

    // Inlays
    const inlays = [3, 5, 7, 9, 12, 15, 17, 19, 21, 24];
    inlays.forEach((inlay, index) => {
        let circle = document.createElementNS(svgNS, "circle");
        circle.setAttribute("id", `inlay${inlay}`);
        circle.setAttribute("cx", 20 + 60 * inlay - (inlay === 12 || inlay === 24 ? 0 : 30));
        circle.setAttribute("cy", inlay === 12 || inlay === 24 ? "50" : "100");
        circle.setAttribute("r", "5");
        circle.setAttribute("fill", "white");
        svg.appendChild(circle);
        if (inlay === 12 || inlay === 24) {
        let circle2 = circle.cloneNode();
        circle2.setAttribute("cy", "150");
        svg.appendChild(circle2);
        }
    });

    // Strings
    for (let i = 1; i <= svgStringCount; i++) {
        let string = document.createElementNS(svgNS, "line");     
        let fbHeight = fingerBoard.getAttribute("height");                                    
        let stringOffset = fbHeight / svgStringCount;
        let startOffset = stringOffset / svgStringCount;

        console.log(startOffset);
        string.setAttribute("id", `string${i}`);
        string.setAttribute("x1", 10);
        string.setAttribute("y1", (stringOffset * i) - startOffset);
        string.setAttribute("x2", 1490);
        string.setAttribute("y2", (stringOffset * i) - startOffset);
        string.setAttribute("stroke", "black");
        string.setAttribute("stroke-width", "2");
        svg.appendChild(string);
    }

    // Append the SVG to the body or specific element
    document.body.appendChild(svg);
}
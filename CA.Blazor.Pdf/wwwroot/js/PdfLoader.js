export async function renderPage(base64Content, pageNumber, canvas) {
    var bytes = atob(base64Content);
    var arr = new Uint8Array(bytes.length);
    for (var i = 0; i < bytes.length; i++) {
        arr[i] = bytes.charCodeAt(i);
    }
    var pdfJsLib = window['pdfjs-dist/build/pdf'];
    var pdfDoc = await pdfJsLib.getDocument({ data: arr }).promise;

    //Carica la pagina del pdf e renderizzala nel canvas
    var pdfPage = await pdfDoc.getPage(pageNumber);
    var viewport = pdfPage.getViewport({ scale: 1.0 });
    setCanvasSize(canvas, viewport.width, viewport.height);
    var canvasContext = canvas.getContext('2d');
    await pdfPage.render({
        canvasContext,
        viewport,
    }).promise;
}

export function setCanvasSize(canvas, width, height) {
    canvas.width = width;
    canvas.height = height;
}

export function elementId(id) {
    return document.getElementById(id);
}

export function base64ToArray(base64Content) {
    var bytes = atob(base64Content);
    var arr = new Uint8Array(bytes.length);
    for (var i = 0; i < bytes.length; i++) {
        arr[i] = bytes.charCodeAt(i);
    }
    return arr;
}

export function numPages(base64Content) {
    var arr = base64ToArray(base64Content);
    var pdfJsLib = window['pdfjs-dist/build/pdf'];
    var pdfDoc = await pdfJsLib.getDocument({ data: arr }).promise;
    return pdfDoc.numPages;
}


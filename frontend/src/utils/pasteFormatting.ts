/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/no-empty-function */
/* eslint-disable @typescript-eslint/explicit-module-boundary-types */
// eslint-disable-next-line no-undef
interface QuillClipboard extends Clipboard {
    read(): Promise<any>;
}

// eslint-disable-next-line no-unused-vars
function pasteFormatting(this: any): void {
    let cursorPosition = 0;
    if (this.quill.getSelection() !== null) {
        cursorPosition = this.quill.getSelection()?.index;
    }
    // TS by default doesn't recognize navigator.clipboard.read() as a function. Cast as custom clipboard.
    const clippy = navigator.clipboard as QuillClipboard;
    clippy
        .read()
        .then((data) => {
            data[0].getType('text/html').then((blob: Blob) => {
                (new Response(blob)).text().then((txt) => {
                    this.quill.clipboard.dangerouslyPasteHTML(cursorPosition, txt);
                }).catch(() => {
                    data[0].getType('text/plain').then((textBlob: Blob) => {
                        (new Response(textBlob)).text().then((txt) => {
                            this.quill.clipboard.dangerouslyPasteHTML(cursorPosition, txt);
                        }).catch(() => { });
                    }).catch(() => { });
                });
            }).catch(() => {
                data[0].getType('text/plain').then((blob: Blob) => {
                    (new Response(blob)).text().then((txt) => {
                        this.quill.clipboard.dangerouslyPasteHTML(cursorPosition, txt);
                    }).catch(() => { });
                }).catch(() => { });
            });
        });
}

export default pasteFormatting;

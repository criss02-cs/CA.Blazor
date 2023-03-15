using CA.Blazor.Pdf.Extensions;
using CA.Blazor.Pdf.Helpers;
using Microsoft.AspNetCore.Components;

namespace CA.Blazor.Pdf
{
    public partial class PdfViewer
    {
        [Parameter]
        public string? Base64Data { get; set; } = string.Empty;
        [Parameter]
        public string? Width { get; set; } // 200px o 20%
        [Parameter]
        public string? Height { get; set; } // 200px o 20%


        private int Pages { get; set; } = 0; // metto a 0 perch� pu� capitare il file sbagliato 

        [Inject]
        public PdfJsInterop PdfJsInterop { get; set; }


        private string Errors { get; set; } = string.Empty;

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                // Controllo che la stringa sia effettivamente un base64
                if (!string.IsNullOrEmpty(Base64Data))
                {
                    if (!Base64Data.IsBase64String())
                    {
                        throw new InvalidOperationException("La stringa non � un base64");
                    }
                    // Ora controllo se il file � effettivamente un pdf
                    var mimeType = AttachmentType.GetMimeType(Base64Data);
                    //Console.WriteLine(mimeType.MimeType + " " + mimeType.Extension);
                    if (mimeType is not null && !mimeType.Extension.Equals(".pdf"))
                    {
                        throw new InvalidOperationException("Il file selezionato non � un pdf");
                    }
                    Pages = await PdfJsInterop.GetNumPages(Base64Data);
                    var canvas = await PdfJsInterop.GetElementById("pdf-canvas");
                    await PdfJsInterop.SetCanvas(canvas, 800, 600);
                    await PdfJsInterop.RenderPage(Base64Data, 1, canvas);
                }
            }
            catch (InvalidOperationException ex)
            {
                Errors = ex.Message;
            }
        }
    }
}
using CA.Blazor.Pdf.Extensions;
using CA.Blazor.Pdf.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

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

        private int ActualPage { get; set; } = 0;
        private int Pages { get; set; } = 0; // metto a 0 perché può capitare il file sbagliato 

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
                        throw new InvalidOperationException("La stringa non è un base64");
                    }
                    // Ora controllo se il file è effettivamente un pdf
                    var mimeType = AttachmentType.GetMimeType(Base64Data);
                    //Console.WriteLine(mimeType.MimeType + " " + mimeType.Extension);
                    if (mimeType is not null && !mimeType.Extension.Equals(".pdf"))
                    {
                        throw new InvalidOperationException("Il file selezionato non è un pdf");
                    }
                    Pages = await PdfJsInterop.GetNumPages(Base64Data);
                    if(Pages > 0)
                    {
                        ActualPage = 1;
                    }
                    var canvas = await PdfJsInterop.GetElementById("pdf-canvas");
                    await RenderPage(ActualPage, canvas, 0.8);
                }
            }
            catch (InvalidOperationException ex)
            {
                Errors = ex.Message;
            }
        }

        private async Task RenderPage(int page, IJSObjectReference canvas, double scale = 1.0)
        {
            if(!string.IsNullOrEmpty(Base64Data))
            {
                await PdfJsInterop.SetCanvas(canvas, 800, 600);
                await PdfJsInterop.RenderPage(Base64Data, page, canvas, scale);
            }
        }
    }
}
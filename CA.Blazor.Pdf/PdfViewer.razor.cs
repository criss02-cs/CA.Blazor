using CA.Blazor.Pdf.Extensions;
using CA.Blazor.Pdf.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace CA.Blazor.Pdf
{
    public partial class PdfViewer
    {
        [Parameter]
        public string? Base64Data { get; set; } = string.Empty;
        [Parameter]
        public string? Width { get; set; } // 200px o 20%

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
                    if (mimeType is not null && !mimeType.Extension.Equals(".pdf"))
                    {
                        throw new InvalidOperationException("Il file selezionato non è un pdf");
                    }
                    Errors = string.Empty;
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

        private async Task ChangePage(int page)
        {
            if(page > 0 && page <= Pages)
            {
                var canvas = await PdfJsInterop.GetElementById("pdf-canvas");
                ActualPage = page;
                await RenderPage(page, canvas, 0.8);
            }
        }

        private void UpdatePage(ChangeEventArgs e)
        {
            var newValue = (e.Value as string).ToInt32(); 
            if(newValue > 0) 
            { 
                ActualPage = newValue;
            }
        }

        private async Task Enter(KeyboardEventArgs e)
        {
            if(e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                var canvas = await PdfJsInterop.GetElementById("pdf-canvas");
                await RenderPage(ActualPage, canvas, 0.8);
            }
        }
    }
}
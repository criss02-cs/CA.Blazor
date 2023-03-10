using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CA.Blazor.Pdf.Extensions;
using CA.Blazor.Pdf.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

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


        private string Errors { get; set; } = string.Empty;

        protected override void OnParametersSet()
        {
            try
            {
                // Controllo che la stringa sia effettivamente un base64
                if (Base64Data is not null)
                {
                    if (!Base64Data.IsBase64String())
                    {
                        throw new InvalidOperationException("La stringa non è un base64");
                    }
                }
                // Ora controllo se il file è effettivamente un pdf
                var mimeType = AttachmentType.GetMimeType(Base64Data);
                if (mimeType is not null && !mimeType.Extension.Equals(".pdf"))
                {
                    throw new InvalidOperationException("Il file selezionato non è un pdf");
                }
            }
            catch (InvalidOperationException ex)
            {
                Errors = ex.Message;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CA.Blazor.Pdf.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CA.Blazor.Pdf
{
    public partial class PdfViewer
    {
        [Parameter]
        public string? Base64Data { get; set; }
        [Parameter]
        public string? Width { get; set; } // 200px o 20%
        [Parameter]
        public string? Height { get; set; } // 200px o 20%


        private string? Errors { get; set; }

        protected override void OnParametersSet()
        {
            try
            {
                if (Base64Data is not null)
                {
                    if (!Base64Data.IsBase64String())
                    {
                        throw new InvalidOperationException("La stringa non è un base64");
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Errors = ex.Message;
            }
        }
    }
}
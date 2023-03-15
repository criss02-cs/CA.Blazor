using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Blazor.Pdf.Extensions
{
    /// <summary>
    /// Class used to add a singleton to services for interoperability of the PdfJsInterop class to manage Javascript code
    /// </summary>
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCaBlazorPdf(this IServiceCollection services)
        {
            services.AddSingleton<PdfJsInterop>();
            return services;
        }
    }
}

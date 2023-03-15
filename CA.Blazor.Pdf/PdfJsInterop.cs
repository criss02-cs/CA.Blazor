using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CA.Blazor.Pdf
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class PdfJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public PdfJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/CA.Blazor.Pdf/js/PdfLoader.js").AsTask());
        }

        public async ValueTask SetCanvas(IJSObjectReference canvas, int width, int height)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("setCanvasSize", canvas, width, height);
        }

        public async ValueTask RenderPage(string base64Content, int pageNumber, IJSObjectReference canvas)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("renderPage", base64Content, pageNumber, canvas);
        }

        public async ValueTask<IJSObjectReference> GetElementById(string id)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<IJSObjectReference>("elementId", id);
        }

        public async ValueTask<int> GetNumPages(string base64)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<int>("numPages", base64);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
# Blazor PDFViewer

CA.Blazor.Pdf is a library that allows to show pdf in your blazor website

**This examples uses 1.1.0 or higher version of this library**

**For previous versions examples check this [readme](https://github.com/criss02-cs/CA.Blazor/tree/master/CA.Blazor.Pdf/old)**

## New Updates

Recently I published a new version of this library, now it allows to pick larger files, and a sort of pagination is present.

The PdfViewer has not more ```Height``` property, because the base structure of component has changed

## Usage
In your Program.cs you have to import the extension class, and add CA.Blazor services:
```C#
using CA.Blazor.Pdf.Extensions; // add this line
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddCaBlazorPdf(); // add this line

await builder.Build().RunAsync();
```
Now you can use CA.Blazor.PdfViewer in your project

First of all you have to insert library using:
```C#
@using CA.Blazor.Pdf
@using CA.Blazor.Pdf.Extensions
```
The second using is for a extensions class that allows the client to use methods like:
ToMemoryStreamAsync() and ToBase64String().
```C#
<InputFile OnChange="OnInputFileChange" />

<PdfViewer Base64Data="@Base64" Width="50%"/>

@code {
    public string Base64 { get; set; }
    IBrowserFile selectedFiles;

    private async void OnInputFileChange(InputFileChangeEventArgs e)
    {
        selectedFiles = e.File;
        using var memory = await selectedFiles.OpenReadStream(2000000000).ToMemoryStreamAsync();
        Base64 = memory.ToBase64String();
        this.StateHasChanged();
    }


}
```
To get base64 encoded string you have to use a MemoryStream and use the extension method ToBase64String().

You have to pass a buffer size to ```OpenReadStream()```, which default use 512 KB, otherwise you'll be not allowed to pick larger files.
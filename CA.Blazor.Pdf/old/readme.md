# Blazor PDFViewer

CA.Blazor.Pdf is a library that allows to show pdf in your blazor website

**This readme is for lower versions of 1.1.0, for higher version you have to read this [documentation](https://github.com/criss02-cs/CA.Blazor/tree/master/CA.Blazor.Pdf)**

## Usage
First of all you have to insert library using:
```C#
@using CA.Blazor.Pdf
@using CA.Blazor.Pdf.Extensions
```
The second using is for a extensions class that allows the client to use methods like:
ToMemoryStreamAsync() and ToBase64String().
```C#
<InputFile OnChange="OnInputFileChange" />

<PdfViewer Base64Data="@Base64" Height="150" Width="500"/>

@code {
    public string Base64 { get; set; }
    IBrowserFile selectedFiles;

    private async void OnInputFileChange(InputFileChangeEventArgs e)
    {
        selectedFiles = e.File;
        var memory = await selectedFiles.OpenReadStream().ToMemoryStreamAsync();
        Base64 = memory.ToBase64String();
        this.StateHasChanged();
    }

    
}
```
To get base64 encoded string you have to use a MemoryStream and use the extension method ToBase64String()
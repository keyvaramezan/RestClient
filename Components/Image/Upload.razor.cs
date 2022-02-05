using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using RestClient.Services.Core;
using System.Net.Http.Headers;

namespace RestClient.Components.Image;
public partial class Upload
{
    [Parameter]
    public int productId { get; set; }
    [Inject]
    public ISnackbar? Snackbar { get; set; }
    [Inject]
    public IImageService? Service { get; set; }
    [CascadingParameter] MudDialogInstance? MudDialog { get; set; }
    
  
    private RestClient.Components.Image.Image? _image;
    
    private List<File> files = new();
    IList<IBrowserFile> uploadfiles = new List<IBrowserFile>();
    
    

    private void UploadFiles(InputFileChangeEventArgs e)
    {

        foreach (var file in e.GetMultipleFiles())
        {
            uploadfiles.Add(file);
        }
        //TODO upload the files to the server
    }
    public async void Save()
    {
        long maxFileSize = 1024 * 1024 * 15;
        using var content = new MultipartFormDataContent();
        foreach (var file in uploadfiles)
        {
            var fileContent = 
                        new StreamContent(file.OpenReadStream(maxFileSize));

                    fileContent.Headers.ContentType = 
                        new MediaTypeHeaderValue(file.ContentType);

                    files.Add(new() { Name = file.Name });

                    content.Add(
                        content: fileContent,
                        name: "\"files\"",
                        fileName: file.Name);
        }
        
        var result = await Service!.UploadImage(productId, content);
        if (!result)
        {
            Snackbar!.Add("Upload new images failed", Severity.Error);
            return;
        }
        else
        {
            Snackbar!.Add("Upload new images success", Severity.Info);
            MudDialog!.Close(DialogResult.Ok(true));
        }
    }
    private class File
    {
        public string? Name { get; set; }
    }
}

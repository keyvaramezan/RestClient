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
    IList<IBrowserFile> files = new List<IBrowserFile>();
    private void UploadFiles(InputFileChangeEventArgs e)
    {

        foreach (var file in e.GetMultipleFiles())
        {
            files.Add(file);
        }
        //TODO upload the files to the server
    }
    public async void Save()
    {
        using var content = new MultipartFormDataContent();
        foreach (var file in files)
        {
            var fileContent =
                        new StreamContent(file.OpenReadStream());

            fileContent.Headers.ContentType =
                new MediaTypeHeaderValue(file.ContentType);
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
}

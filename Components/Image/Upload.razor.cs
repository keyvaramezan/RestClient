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
        
        var result = await Service!.UploadImage(productId, files);
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

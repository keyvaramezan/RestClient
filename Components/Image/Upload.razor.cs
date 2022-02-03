using Microsoft.AspNetCore.Components.Forms;

namespace RestClient.Components.Image;
public partial class Upload
{
    IList<IBrowserFile> files = new List<IBrowserFile>();
    private void UploadFiles(InputFileChangeEventArgs e)
    {
        foreach (var file in e.GetMultipleFiles())
        {
            files.Add(file);
        }
        //TODO upload the files to the server
    }
}

using Microsoft.AspNetCore.Components.Forms;
using RestClient.Models;

namespace RestClient.Services.Core
{
    public interface IImageService
    {
        Task<IList<ImageDto>>  GetProdcutImages(int productId);
        Task<bool> UploadImage(int productId, MultipartFormDataContent content);

        Task<bool> DeleteImage(int imageId, int productId);


    }
}
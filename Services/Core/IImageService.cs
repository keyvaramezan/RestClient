using RestClient.Models;

namespace RestClient.Services.Core
{
    public interface IImageService
    {
        Task<IList<ImageDto>>  GetProdcutImages(int productId);
    }
}
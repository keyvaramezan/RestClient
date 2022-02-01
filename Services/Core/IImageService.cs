using RestClient.Models;

namespace RestClient.Services.Core
{
    public interface IImageService
    {
        Task<IEnumerable<ImageDto>>  GetProdcutImages(int ProductId);
    }
}
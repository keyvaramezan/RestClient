using RestClient.Models;

namespace RestClient.Services.Core
{
    public interface IProductService
    {
        Task<PagedList<Product>> GetProducts(SearchRequestDto resquest);
    }
}

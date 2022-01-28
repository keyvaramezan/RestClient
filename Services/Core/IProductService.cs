using RestClient.Models;

namespace RestClient.Services.Core
{
    public interface IProductService
    {
        Task<PagedList<Product>> GetProducts(SearchRequestDto resquest);
        Task<bool> AddProduct(AddProductDto product);
        Task<Product> GetProductById(int productId);
        Task<bool> EditProduct(int productId, EditProductDto product);
    }
}

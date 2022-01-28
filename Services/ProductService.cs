using Microsoft.AspNetCore.WebUtilities;
using RestClient.Models;
using RestClient.Services.Core;
using System.Net.Http.Json;
using System.Text.Json;

namespace RestClient.Services
{
    public class ProductService : IProductService
    {
        private HttpClient _http;
        private readonly JsonSerializerOptions _options;
        public ProductService(HttpClient http)
        {
            _http = http;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
        }
        public async Task<PagedList<Product>> GetProducts(SearchRequestDto request)
        {
            //await Task.Delay(700);
            var queryStringParam = new Dictionary<string, string>
            {
                ["PageIndex"] = request.PageIndex.ToString(),
                ["PageSize"] = request.PageSize.ToString(),
                ["Sort"] = request.Sort,
            };
            if (!string.IsNullOrEmpty(request.SearchText))
            {
                queryStringParam.Add("SearchText", request.SearchText);
            }
            var uri = QueryHelpers.AddQueryString("product", queryStringParam);

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);

            var httpResponse = await _http.SendAsync(httpRequest);

            var totalCout = int.Parse(httpResponse.Headers.GetValues("X-TotalCount").First());

            var content = await httpResponse.Content.ReadAsStringAsync();

            var products = JsonSerializer.Deserialize<List<Product>>(content, _options);
         
            var result = new PagedList<Product>(products, totalCout);
            return await Task.FromResult(result);
        }

        public async Task<bool> AddProduct(AddProductDto product)
        {
            using var response = await _http.PostAsJsonAsync("product", product);
            return response.IsSuccessStatusCode;
        }

        public  Task<Product> GetProductById(int productId)
        {
           return  _http.GetFromJsonAsync<Product>($"product/{productId}")!;
        }

        public async Task<bool> EditProduct(int productId, EditProductDto product)
        {
            using var response = await _http.PutAsJsonAsync($"product/{productId}", product);
            return response.IsSuccessStatusCode;
        }
    }
}

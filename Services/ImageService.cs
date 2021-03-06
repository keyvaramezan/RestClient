using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;
using RestClient.Models;
using RestClient.Services.Core;

namespace RestClient.Services
{
    public class ImageService : IImageService
    {
        private HttpClient _http;
        private readonly JsonSerializerOptions _options;

        public ImageService(HttpClient http)
        {
            _http = http;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
        }

        public async Task<IList<ImageDto>> GetProdcutImages(int productId)
        {
            var uri = $"product/{productId}/image";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
            var httpResponse = await _http.SendAsync(httpRequest);
            var content = await httpResponse.Content.ReadAsStringAsync(); 
            var images =  JsonSerializer.Deserialize<List<ImageDto>>(content, _options);
            var result = new List<ImageDto>(images!);
            return await Task.FromResult(images!);
        }

        public async Task<bool> UploadImage(int productId, MultipartFormDataContent content)
        {

            using var response = await _http.PostAsync($"product/{productId}/image/upload", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteImage(int imageId, int productId)
        {
            using var response = await _http.DeleteAsync($"product/{productId}/image/imageId?imageId={imageId}");
            return response.IsSuccessStatusCode;
        }
    }
}
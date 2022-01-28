using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RestClient;
using RestClient.ServiceExtensions;
using RestClient.Services;
using RestClient.Services.Core;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{ 
    BaseAddress = new Uri("https://localhost:7175")
});
builder.Services.AddMudServiceWithSnackbar();
builder.Services.AddScoped<IProductService, ProductService>();
await builder.Build().RunAsync();

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using RestClient.Models;
using RestClient.Services.Core;

namespace RestClient.Components.Image
{
    public partial class Image
    {
        [Inject]
        private HttpClient? Http { get; set; }
        private MudCarousel<string>? _carousel;
        private bool _arrows = true;
        private bool _bullets = true;
        private bool _autocycle = true;
        private IList<string>? _source = new List<string>(); //{ "item 1", "Item 2", "Item 3", "Item 4", "Item 5" };
        private int selectedIndex = 2;
        private bool _isLoading = false;

        [Inject]
        public IImageService? Service { get; set; }
        [Inject]
        public IDialogService? DialogService { get; set; }

        [Parameter]
        public int productId { get; set; }
        private DialogOptions dialogOptions = new DialogOptions
        {
            CloseButton = true,
            CloseOnEscapeKey = true,
            Position = DialogPosition.TopCenter,
            MaxWidth = MaxWidth.ExtraSmall,
            FullWidth = true
        };
        protected async override Task OnInitializedAsync()
        {
           _isLoading = true;
            var result = await Service!.GetProdcutImages(productId);
            _isLoading = false;
            foreach (var item in result)
            {
                var uri = Http!.BaseAddress + item.Name;
                _source!.Add(uri);
            }
        }
        public void AddAsync()
        {
            var parameters = new DialogParameters();
            parameters.Add("productId", productId);
            var result = DialogService!.Show<Upload>("UploadImages", parameters, dialogOptions).Result;
        }
        public async Task DeleteAsync(int selectedIndex)
        {

        }
    }

}
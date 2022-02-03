using Microsoft.AspNetCore.Components;
using MudBlazor;
using RestClient.Models;
using RestClient.Services.Core;

namespace RestClient.Components.Products
{
    public partial class List
    {
        private DialogOptions dialogOptions = new DialogOptions
        {
            CloseButton = true,
            CloseOnEscapeKey = true,
            Position = DialogPosition.TopCenter,
            MaxWidth = MaxWidth.ExtraSmall,
            FullWidth = true
        };
        [Inject]
        public IProductService? Service { get; set; }
        [Inject]
        public IDialogService? DialogService { get; set; }
        private string? _searchText = "";
        private MudTable<Product>? _table;
        [Parameter]
        public EventCallback<SelectedProductEventArgs> OnSelectedProductsChanged { get; set; }
        
        public async Task<TableData<Product>> GetProductData(TableState state)
        {
            var sortDirection = state.SortDirection == SortDirection.Ascending ? "asc" : "desc";
            var sortFiled = state.SortLabel == "" ? "Id" : state.SortLabel;
            var sort = $"{sortFiled} {sortDirection}";
            var request = new SearchRequestDto
            {
                PageIndex = state.Page + 1,
                PageSize = state.PageSize,
                Sort = sort,
                SearchText = _searchText!
            };

            var tableData = new TableData<Product>();
            var result = await Service!.GetProducts(request);
            tableData.TotalItems = result.TotalCount;
            tableData.Items = result;
            return await Task.FromResult(tableData);
        }
        public async Task OnSearch(String text)
        {
            //if(string.IsNullOrEmpty(text))
            //{
            //    return;
            //}
            _searchText = text;

            await _table!.ReloadServerData();
        }
        private void OnSelectedItemsChanged(HashSet<Product> selecteds)
        {
            int[] ids = selecteds.Select(p => p.Id).ToArray();
            var eventArgs = new SelectedProductEventArgs(ids);
            OnSelectedProductsChanged.InvokeAsync(eventArgs);

        }
        public async Task ReloadAsync()
        {
          await _table!.ReloadServerData();
        }
        public void CleareSelectedItems()
        {
            _table!.SelectedItems.Clear();
        }
        public void ShowGallery(int productId)
        {
            var parameters = new DialogParameters();
            parameters.Add("productId", productId);
            DialogService!.Show<Image.Image>("ShowGallery", parameters, dialogOptions);
        }
    }
}
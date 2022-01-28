using Microsoft.AspNetCore.Components;
using MudBlazor;
using RestClient.Models;
using RestClient.Services.Core;

namespace RestClient.Components.Products
{
    public partial class List
    {
        [Inject]
        public IProductService? Service { get; set; }
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
        public void OnSearch(String text)
        {
            _searchText = text;
            _table!.ReloadServerData();

        }
        private void OnSelectedItemsChanged(HashSet<Product> selecteds)
        {
            int[] ids = selecteds.Select(p => p.Id).ToArray();
            var eventArgs = new SelectedProductEventArgs(ids);
            OnSelectedProductsChanged.InvokeAsync(eventArgs);

        }
        public void Reload()
        {
            _table!.ReloadServerData();
            _table.SelectedItems.Clear();
        }
    }
}
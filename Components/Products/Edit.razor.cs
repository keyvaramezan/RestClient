using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using RestClient.Models;
using RestClient.Services.Core;

namespace RestClient.Components.Products
{
    public partial class Edit
    {
        [Inject]
        public IProductService? Service { get; set; }
        
        [Inject]
        public ISnackbar? Snackbar { get; set; }

        private EditProductDto _model = new EditProductDto();

        private EditContext? _context;
        
        [Parameter]
        public int productId { get; set; }

        private bool _isLoading = false;
        protected async override Task OnInitializedAsync()
        {
           _context = new EditContext(_model);
           var result = await Service!.GetProductById(productId);
            _model.Name = result.Name;
            _model.Description = result.Description;
            _model.Price = result.Price;
        }
        [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

        private async void Submit()
        {
            var isValid = _context!.Validate();
            if (!isValid) return;
            var result = await Service!.EditProduct(productId ,_model);
            if (!result)
            {
                Snackbar!.Add("Adding new product failed", Severity.Error);
                return;
            }
            else
            {
                Snackbar!.Add("Adding new product success", Severity.Success);
                MudDialog!.Close(DialogResult.Ok(true));
            }

        }
        void Cancel() => MudDialog!.Cancel();
    }
}
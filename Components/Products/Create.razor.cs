using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using RestClient.Models;
using RestClient.Services.Core;

namespace RestClient.Components.Products
{
    public partial class Create
    {
        [Inject]
        public IProductService? Service { get; set; }
        [Inject]
        public ISnackbar? Snackbar { get; set; }
        private AddProductDto _model = new AddProductDto();

        private EditContext? _context;

        protected override void OnInitialized()
        {
           _context = new EditContext(_model);
        }
        [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

        private async void Submit()
        {
            var isValid = _context!.Validate();
            if (!isValid) return;
            var result = await Service!.AddProduct(_model);
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
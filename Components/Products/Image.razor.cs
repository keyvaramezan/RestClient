using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using RestClient.Models;
using RestClient.Services.Core;

namespace RestClient.Components.Products
{
    public partial class Image
    {
        private MudCarousel<string>? _carousel;
        private bool _arrows = true;
        private bool _bullets = true;
        private bool _autocycle = true;
        private IList<string> _source = new List<string>() { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" };
        private int selectedIndex = 2;
        
        [Parameter]
        public int productId { get; set; }
    }

}
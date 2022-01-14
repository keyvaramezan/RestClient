using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace RestClient.Shared
{
    public partial class AppBar
    {
        [Parameter]
        public EventCallback OnSidebarToggled { get; set; }
        public async Task ToggleSidebar()
        {
            await OnSidebarToggled.InvokeAsync();
        }
        [Parameter]
        public EventCallback OnThemeToggled { get; set; }

        public async Task ToggleTheme()
        {
            themeMode = !themeMode;
            await OnThemeToggled.InvokeAsync();
        }
        private bool themeMode = false;
        public string ThemeIcon()
        {
            return themeMode ? Icons.Material.Filled.Brightness5 : Icons.Material.Filled.Brightness4;
        }

    }
}
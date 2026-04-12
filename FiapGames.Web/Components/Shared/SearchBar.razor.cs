using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FiapGames.Web.Components.Shared;

public partial class SearchBar
{
    [Parameter] public string Placeholder { get; set; } = "Buscar jogos...";
    [Parameter] public EventCallback<string> OnSearch { get; set; }

    private string SearchText { get; set; } = string.Empty;

    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
            await OnSearch.InvokeAsync(SearchText);
    }

    private async Task ClearSearch()
    {
        SearchText = string.Empty;
        await OnSearch.InvokeAsync(SearchText);
    }
}
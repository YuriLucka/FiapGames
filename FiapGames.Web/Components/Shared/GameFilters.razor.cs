using Microsoft.AspNetCore.Components;

namespace FiapGames.Web.Components.Shared;

public partial class GameFilters
{
    [Parameter] public List<string> Categorias { get; set; } = [];
    [Parameter] public List<string> Desenvolvedores { get; set; } = [];
    [Parameter] public EventCallback<GameFilterValues> OnFilterChanged { get; set; }

    private string SelectedCategoria { get; set; } = string.Empty;
    private string SelectedPreco { get; set; } = string.Empty;
    private string SelectedDesenvolvedor { get; set; } = string.Empty;

    private async Task NotifyChange()
    {
        await OnFilterChanged.InvokeAsync(new GameFilterValues
        {
            Categoria = SelectedCategoria,
            Preco = SelectedPreco,
            Desenvolvedor = SelectedDesenvolvedor
        });
    }
}

public class GameFilterValues
{
    public string Categoria { get; set; } = string.Empty;
    public string Preco { get; set; } = string.Empty;
    public string Desenvolvedor { get; set; } = string.Empty;
}
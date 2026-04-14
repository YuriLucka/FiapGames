using Microsoft.AspNetCore.Components;

namespace FiapGames.Web.Components.Shared;

public partial class GameCard
{
    public Guid IdGame {  get; set; }
    [Parameter, EditorRequired] public string Title { get; set; } = string.Empty;
    [Parameter, EditorRequired] public string Price { get; set; } = string.Empty;
    [Parameter] public string ImageUrl { get; set; } = string.Empty;
}
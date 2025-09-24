
using BlazorApp1.Models;
using BlazorApp1.Services;

namespace BlazorApp1.Components.Pages;

public partial class Home
{
    private ApiResult<Movie>? _apiResults;

    protected override async Task OnInitializedAsync()
    {
        //Récupération des données chargés par le service.
        _apiResults = await TMDBService.GetTopRatedAsync();
    }

}

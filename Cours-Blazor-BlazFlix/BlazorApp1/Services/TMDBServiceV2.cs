using BlazorApp1.Models;

namespace BlazorApp1.Services;

/// <summary>
///     Service de chargement de données de l'API de TMDB.
/// </summary>
/// <param name="httpClient">Client http à utiliser</param>
public class TMDBServiceV2(HttpClient httpClient)
{
    public async Task<ApiResult<Movie>> GetTopRatedAsync(int page = 1)
    {
        ApiResult<Movie> result = await httpClient.GetFromJsonAsync<ApiResult<Movie>>($"3/movie/top_rated?language=fr-fr&page={page}")
            ?? throw new Exception("Api call error");

        return result;
    }
}

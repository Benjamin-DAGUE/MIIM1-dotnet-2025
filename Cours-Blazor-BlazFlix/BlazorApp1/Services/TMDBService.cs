using BlazorApp1.Models;

namespace BlazorApp1.Services;

/// <summary>
///     Service de chargement de données de l'API de TMDB.
/// </summary>
/// <param name="httpClientFactory">Usine de fabrication de client HTTP utilisé pour appeler l'API.</param>
public class TMDBService(IHttpClientFactory httpClientFactory)
{
    public async Task<ApiResult<Movie>> GetTopRatedAsync(int page = 1)
    {
        using HttpClient client = httpClientFactory.CreateClient(HttpClientNames.TMDBHttpClient);

        ApiResult<Movie> result = await client.GetFromJsonAsync<ApiResult<Movie>>($"3/movie/top_rated?language=fr-fr&page={page}")
            ?? throw new Exception("Api call error");

        return result;
    }
}

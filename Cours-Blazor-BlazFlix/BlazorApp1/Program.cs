using BlazorApp1.Components;
using BlazorApp1.Models;
using BlazorApp1.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

//HttpClient client = new HttpClient()
//{
//    BaseAddress = new Uri("https://api.themoviedb.org")
//};


//client.DefaultRequestHeaders.Authorization
//    = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
//    "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJmMzIwNjI4ZTM4MjhhYzBjZDk5YWY2MzQ1YTgyYjRmYiIsIm5iZiI6MTc1ODcwNTE0Ni40MjEsInN1YiI6IjY4ZDNiNWZhMzEzYzBjN2ZiYzQ5ZjQxYSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.zZE0bShGJ8QW6Nt6XEl_4t5KdV5mDdtIYKBdkAFF0pk");

//client.DefaultRequestHeaders.Accept.Add(
//    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

//ApiResult<Movie>? result = await client.GetFromJsonAsync<ApiResult<Movie>>("3/movie/top_rated?language=fr-fr&page=1");


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

//On enregistre un client HTTP pré-configuré nommé qui pourra être chargé avec IHttpContextFactory
//builder.Services.AddHttpClient(HttpClientNames.TMDBHttpClient, httpClient =>
//{
//    httpClient.BaseAddress = new Uri("https://api.themoviedb.org");

//    httpClient.DefaultRequestHeaders.Authorization
//        = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
//        "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJmMzIwNjI4ZTM4MjhhYzBjZDk5YWY2MzQ1YTgyYjRmYiIsIm5iZiI6MTc1ODcwNTE0Ni40MjEsInN1YiI6IjY4ZDNiNWZhMzEzYzBjN2ZiYzQ5ZjQxYSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.zZE0bShGJ8QW6Nt6XEl_4t5KdV5mDdtIYKBdkAFF0pk");

//    httpClient.DefaultRequestHeaders.Accept.Add(
//        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
//});
//builder.Services.AddSingleton<TMDBService>();

//On enregistre un client HTTP pré-configuré typé qui pourra être injecté directement dans le service TMDBServiceV2
builder.Services.AddHttpClient<TMDBServiceV2>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.themoviedb.org");

    httpClient.DefaultRequestHeaders.Authorization
        = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
        "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJmMzIwNjI4ZTM4MjhhYzBjZDk5YWY2MzQ1YTgyYjRmYiIsIm5iZiI6MTc1ODcwNTE0Ni40MjEsInN1YiI6IjY4ZDNiNWZhMzEzYzBjN2ZiYzQ5ZjQxYSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.zZE0bShGJ8QW6Nt6XEl_4t5KdV5mDdtIYKBdkAFF0pk");

    httpClient.DefaultRequestHeaders.Accept.Add(
        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

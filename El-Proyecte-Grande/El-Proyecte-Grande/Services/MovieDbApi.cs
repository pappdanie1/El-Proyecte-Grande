using System.Net;

namespace El_Proyecte_Grande.Services;

public class MovieDbApi : IMovieDbApi
{
    private readonly ILogger<MovieDbApi> _logger;

    public MovieDbApi(ILogger<MovieDbApi> logger)
    {
        _logger = logger;
    }

    public async Task<string> GetMovies()
    {
        var API_KEY = "81041f1501b21c327e1c6644a030c201"; //appsettings.json  //Environment.GetVariable
        var API_URL = $"https://api.themoviedb.org/3/movie/now_playing?api_key={API_KEY}&language=en-US&page=1";
        
        using var client = new HttpClient();
        
        _logger.LogInformation("Calling Movie DB API with url: {API_URL}", API_URL);
        var response = await client.GetAsync(API_URL);
        return await response.Content.ReadAsStringAsync();
    }
}
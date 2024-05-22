using System.Net;

namespace El_Proyecte_Grande.Services;

public class MovieDbApi : IMovieDbApi
{
    private readonly ILogger<MovieDbApi> _logger;
    private readonly IConfiguration _configuration;

    public MovieDbApi(ILogger<MovieDbApi> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<string> GetMovies()
    {
        var API_KEY = Environment.GetEnvironmentVariable("MOVIEDB_API_KEY") ?? _configuration["MovieDbApiKey"];
        var API_URL = $"https://api.themoviedb.org/3/movie/now_playing?api_key={API_KEY}&language=en-US&page=1";
        
        using var client = new HttpClient();
        
        _logger.LogInformation("Calling Movie DB API with url: {API_URL}", API_URL);
        var response = await client.GetAsync(API_URL);
        return await response.Content.ReadAsStringAsync();
    }
}
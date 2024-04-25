using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public class OmdbApi : IOmdbApi
{
    private readonly ILogger<OmdbApi> _logger;
    private readonly IJsonProcessor _jsonProcessor;
    private readonly IMovieDbApi _movieDbApi;

    public OmdbApi(ILogger<OmdbApi> logger, IJsonProcessor jsonProcessor, IMovieDbApi movieDbApi)
    {
        _logger = logger;
        _jsonProcessor = jsonProcessor;
        _movieDbApi = movieDbApi;
    }

    public async Task<List<string>> GetMovies()
    {
        var API_KEY = "de5ff22f";
        var moviesData = await _movieDbApi.GetMovies();
        var movies = _jsonProcessor.ProcessTitles(moviesData);
        var movieDownloadString = new List<string>();
        foreach (var title in movies)
        {
            var API_URL = $"http://www.omdbapi.com/?apikey=de5ff22f&t={title}";
            using var client = new HttpClient();

            _logger.LogInformation("Calling Movie DB API with url: {API_URL}", API_URL);
            var response = await client.GetAsync(API_URL);
            var data = await response.Content.ReadAsStringAsync();
            movieDownloadString.Add(data);
        }

        return movieDownloadString;
    }
}
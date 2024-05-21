using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public class OmdbApi : IOmdbApi
{
    private readonly ILogger<OmdbApi> _logger;
    private readonly IJsonProcessor _jsonProcessor;
    private readonly IMovieDbApi _movieDbApi;
    private IConfiguration _config;

    public OmdbApi(ILogger<OmdbApi> logger, IJsonProcessor jsonProcessor, IMovieDbApi movieDbApi, IConfiguration config)
    {
        _logger = logger;
        _jsonProcessor = jsonProcessor;
        _movieDbApi = movieDbApi;
        _config = config;
    }

    public async Task<List<string>> GetMovies()
    {
        var API_KEY = _config["OmdbApiKey"];
        var moviesData = await _movieDbApi.GetMovies();
        var movies = _jsonProcessor.ProcessTitles(moviesData);
        var movieDownloadString = new List<string>();
        foreach (var title in movies)
        {
            try
            {
                var API_URL = $"http://www.omdbapi.com/?apikey={API_KEY}&t={title}";
                using var client = new HttpClient();

                _logger.LogInformation("Calling Movie DB API with url: {API_URL}", API_URL);
                var response = await client.GetAsync(API_URL);
                var data = await response.Content.ReadAsStringAsync();

                if (!data.Contains("Error"))
                {
                    movieDownloadString.Add(data);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
        return movieDownloadString;
    }
}
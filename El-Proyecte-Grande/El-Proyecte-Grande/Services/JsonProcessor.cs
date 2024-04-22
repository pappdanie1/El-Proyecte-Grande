using System.Text.Json;
using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public class JsonProcessor : IJsonProcessor
{
    public List<Movie> ProcessMovies(string data)
    {
        
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement main = json.RootElement.GetProperty("results");
        var movies = new List<Movie>();

        int movieCount = 0;
        
        foreach (var movie in main.EnumerateArray())
        {
            if (movieCount >= 5)
            {
                break;
            }
            var item = new Movie
                {
                Title = movie.GetProperty("original_title").GetString(),
                Director = null,
                Cast = null,
                Description = movie.GetProperty("overview").GetString(),
                DurationInSec = null,
                Poster = movie.GetProperty("poster_path").GetString()
            };
            movies.Add(item);
            movieCount++;
        }

        return movies;
    }
}
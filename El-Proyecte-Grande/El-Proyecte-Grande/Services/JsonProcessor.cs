using System.Text.Json;
using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public class JsonProcessor : IJsonProcessor
{
    public List<string> ProcessTitles(string data)
    {
        
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement main = json.RootElement.GetProperty("results");
        var movies = new List<string>();

        int movieCount = 0;
        
        foreach (var movie in main.EnumerateArray())
        {
            if (movieCount >= 5)
            {
                break;
            }

            var title = movie.GetProperty("original_title").GetString();
            /* var item = new Movie
                {
                Title = movie.GetProperty("original_title").GetString(),
                Director = null,
                Cast = null,
                Description = movie.GetProperty("overview").GetString(),
                DurationInSec = null,
                Poster = movie.GetProperty("poster_path").GetString()
            }; */
            movies.Add(title);
            movieCount++;
        }

        return movies;
    }

    public List<Movie> ProcessMovies(List<string> data)
    {
        var movies = new List<Movie>();
        foreach (var movie in data)
        {
            JsonDocument json = JsonDocument.Parse(movie);
            JsonElement main = json.RootElement;

            var item = new Movie
            {
                Title = main.GetProperty("Title").GetString(),
                Director = main.GetProperty("Director").GetString(),
                Cast = main.GetProperty("Actors").GetString(),
                Description = main.GetProperty("Plot").GetString(),
                DurationInSec = main.GetProperty("Runtime").GetString(),
                Poster = main.GetProperty("Poster").GetString(),
                Genres = main.GetProperty("Genre").GetString()
            };
            movies.Add(item);
        }

        return movies;
    }
}
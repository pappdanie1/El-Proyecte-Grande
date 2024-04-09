using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public class MovieRepository : IMovieRepository
{
    private List<Movie> _movies;

    public MovieRepository()
    {
        _movies = new List<Movie>
        {
            new Movie(1, "Test", "Test", new List<string>{"1", "2"}, "desc", 230),
            new Movie(2, "Test2", "Test", new List<string>{"1", "2"}, "desc", 230),
            new Movie(3, "Test3", "Test", new List<string>{"1", "2"}, "desc", 230),
        };
    }

    public IList<Movie> GetAll()
    {
        return _movies;
    }

    public Movie AddMovie(Movie movie)
    {
        _movies.Add(movie);
        return movie;
    }

    public Movie DeleteById(int id)
    {
        foreach (var movie in _movies)
        {
            if (movie.Id == id)
            {
                _movies.Remove(movie);
                return movie;
            }
        }

        return null;
    }
    
    public Movie UpdateMovie(int id, Movie updatedMovie)
    {
        var movieToUpdate = _movies.FirstOrDefault(m => m.Id == id);
        if (movieToUpdate != null)
        {
            movieToUpdate.Title = updatedMovie.Title;
            movieToUpdate.Director = updatedMovie.Director;
            movieToUpdate.Cast = updatedMovie.Cast;
            movieToUpdate.Description = updatedMovie.Description;
            movieToUpdate.DurationInSec = updatedMovie.DurationInSec;
        }

        return movieToUpdate;
    }
}
using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public class MovieRepository : IMovieRepository
{
    private List<Movie> _movies;
    private readonly IMovieDbApi _movieDbApi;
    private readonly IJsonProcessor _jsonProcessor;

    public MovieRepository(IMovieDbApi movieDbApi, IJsonProcessor jsonProcessor)
    {
        _movieDbApi = movieDbApi;
        _jsonProcessor = jsonProcessor;
        var response = _movieDbApi.GetMovies();
        var data = _jsonProcessor.ProcessMovies(response);
        _movies = data;
    }

    public IList<Movie> GetAll()
    {
        return _movies;
    }

    public Movie GetById(int id)
    {
        var movie = _movies.Find(movie => movie.Id == id);

        if (movie == null)
        {
            return null;
        }

        return movie;
    }

    public Movie AddMovie(Movie movie)
    {
        var id = _movies.Count + 1;
        movie.Id = id;
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
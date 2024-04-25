using AspCinema.Models;
using El_Proyecte_Grande.Data;

namespace El_Proyecte_Grande.Services;

public class MovieRepository : IMovieRepository
{
    private readonly ElProyecteGrandeContext _movieDbContext;

    public MovieRepository(ElProyecteGrandeContext context)
    {
        _movieDbContext = context;
    }

    public IList<Movie> GetAll()
    {
        return _movieDbContext.Movies.ToList();
    }

    public Movie? GetById(int id)
    {
        return _movieDbContext.Movies.FirstOrDefault(movie => movie.Id == id);
    }

    public void AddMovie(Movie movie)
    {
        _movieDbContext.Add(movie);
        _movieDbContext.SaveChanges();
    }

    public void DeleteById(int id)
    {
        
        _movieDbContext.Remove(_movieDbContext.Movies.FirstOrDefault(movie => movie.Id == id));
        _movieDbContext.SaveChanges();
    }
    
    public void UpdateMovie(Movie movie)
    {
        _movieDbContext.Update(movie);
        _movieDbContext.SaveChanges();
    }
}
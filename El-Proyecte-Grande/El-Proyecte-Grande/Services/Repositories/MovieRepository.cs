using AspCinema.Models;
using El_Proyecte_Grande.Data;
using Microsoft.EntityFrameworkCore;

namespace El_Proyecte_Grande.Services.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly AspCinemaContext _context;

    public MovieRepository(AspCinemaContext context)
    {
        _context = context;
    }

    public IList<Movie> GetAll()
    {
        return _context.Movies.Include(m => m.Screenings).ToList();
    }

    public Movie? GetById(int id)
    {
        return _context.Movies.FirstOrDefault(movie => movie.Id == id);
    }

    public void AddMovie(Movie movie)
    {
        _context.Add(movie);
        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        
        _context.Remove(_context.Movies.FirstOrDefault(movie => movie.Id == id));
        _context.SaveChanges();
    }
    
    public void UpdateMovie(Movie movie)
    {
        _context.Update(movie);
        _context.SaveChanges();
    }
}
using System.Runtime.InteropServices.JavaScript;
using AspCinema.Models;
using El_Proyecte_Grande.Data;
using Microsoft.EntityFrameworkCore;

namespace El_Proyecte_Grande.Services;

public class ScreeningRepository : IScreeningRepository
{
    private readonly AspCinemaContext _movieDbContext;

    public ScreeningRepository(AspCinemaContext context)
    {
        _movieDbContext = context;
    }

    public IList<Screening> GetAll()
    {
        return _movieDbContext.Screenings
            .Include(s => s.Movie)
            .Include(s => s.Auditorium)
            .ToList();
    }

    public Screening? GetById(int id)
    {
        return _movieDbContext.Screenings.FirstOrDefault(s => s.Id == id);
    }

    public void AddScreening(Screening screening)
    {
        _movieDbContext.Add(screening);
        _movieDbContext.SaveChanges();
    }

    public void DeleteById(int id)
    {
        
        _movieDbContext.Remove(_movieDbContext.Screenings.FirstOrDefault(s => s.Id == id));
        _movieDbContext.SaveChanges();
    }
    
    public void UpdateScreening(Screening screening)
    {
        _movieDbContext.Update(screening);
        _movieDbContext.SaveChanges();
    }
    
}
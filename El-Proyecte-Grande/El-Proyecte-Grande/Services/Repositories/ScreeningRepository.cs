using El_Proyecte_Grande.Data;
using El_Proyecte_Grande.Models;
using Microsoft.EntityFrameworkCore;

namespace El_Proyecte_Grande.Services.Repositories;

public class ScreeningRepository : IScreeningRepository
{
    private readonly AspCinemaContext _context;

    public ScreeningRepository(AspCinemaContext context)
    {
        _context = context;
    }

    public IList<Screening> GetAll()
    {
        return _context.Screenings
            .Include(s => s.Movie)
            .Include(s => s.Auditorium)
            .ToList();
    }

    public Screening? GetById(int id)
    {
        return _context.Screenings.Include(s => s.Auditorium.Seats).Include(s => s.Auditorium).Include(s => s.Movie).FirstOrDefault(s => s.Id == id);
    }

    public void AddScreening(Screening screening)
    {
        _context.Add(screening);
        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        
        _context.Remove(_context.Screenings.FirstOrDefault(s => s.Id == id));
        _context.SaveChanges();
    }
    
    public void UpdateScreening(Screening screening)
    {
        _context.Update(screening);
        _context.SaveChanges();
    }
    
}
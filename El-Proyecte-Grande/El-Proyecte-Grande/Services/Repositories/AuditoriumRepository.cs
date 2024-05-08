using AspCinema.Models;
using El_Proyecte_Grande.Data;
using El_Proyecte_Grande.Models;
using Microsoft.EntityFrameworkCore;

namespace El_Proyecte_Grande.Services.Repositories;

public class AuditoriumRepository : IAuditoriumRepository
{
    private readonly AspCinemaContext _context;

    public AuditoriumRepository(AspCinemaContext context)
    {
        _context = context;
    }

    public Auditorium GetById(int id)
    {
        var auditorium = _context.Auditoriums
            .Include(a => a.Seats.Where(s => s.Auditorium.Id == id))
            .FirstOrDefault(a => a.Id == id);

        return auditorium;
    }

    public void AddAuditorium(Auditorium auditorium)
    {
        _context.Add(auditorium);
        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        _context.Remove(_context.Auditoriums.FirstOrDefault(a => a.Id == id));
        _context.SaveChanges();
    }
}
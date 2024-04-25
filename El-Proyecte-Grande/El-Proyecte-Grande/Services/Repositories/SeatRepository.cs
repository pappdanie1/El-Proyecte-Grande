using AspCinema.Models;
using El_Proyecte_Grande.Data;
using Microsoft.EntityFrameworkCore;

namespace El_Proyecte_Grande.Services;

public class SeatRepository : ISeatRepository
{
    private readonly ElProyecteGrandeContext _context;

    public SeatRepository(ElProyecteGrandeContext context)
    {
        _context = context;
    }

    public IList<Seat> GetAll()
    {
        return _context.Seats.Include(s => s.Auditorium).ToList();
    }

    public void AddSeat(Seat seat)
    {
        _context.Add(seat);
        _context.SaveChanges();
    }
}
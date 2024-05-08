using AspCinema.Models;
using El_Proyecte_Grande.Data;
using Microsoft.EntityFrameworkCore;

namespace El_Proyecte_Grande.Services.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly AspCinemaContext _context;

    public SeatRepository(AspCinemaContext context)
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
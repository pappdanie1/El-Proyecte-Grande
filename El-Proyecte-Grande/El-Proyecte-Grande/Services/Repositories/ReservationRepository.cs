using AspCinema.Models;
using El_Proyecte_Grande.Data;
using Microsoft.EntityFrameworkCore;

namespace El_Proyecte_Grande.Services.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AspCinemaContext _context;

    public ReservationRepository(AspCinemaContext context)
    {
        _context = context;
    }
    
    //All reservations for a given user
    public IList<Reservation> GetAllByUser(string userName)
    {
        return _context.Reservations.Where(r => r.Customer.UserName == userName).ToList();
    }

    //All reservations for a given screening
    public IList<Reservation> GetAllByScreening(int screeningId)
    {
        return _context.Reservations.Where(r => r.Screening.Id == screeningId).ToList();
    }

    //for deleting a reservation for a user
    public Reservation? GetById(int id, string userName)
    {
        return _context.Reservations.Include(r => r.Customer)
            .FirstOrDefault(r => r.Id == id && r.Customer.UserName == userName);
    }

    public void AddReservation(Reservation reservation)
    {
        _context.Add(reservation);
        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        _context.Reservations.Remove(_context.Reservations.FirstOrDefault(r => r.Id == id));
        _context.SaveChanges();
    }
}
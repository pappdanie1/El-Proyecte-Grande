using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public interface ISeatRepository
{
    IList<Seat> GetAll();
    void AddSeat(Seat seat);
}
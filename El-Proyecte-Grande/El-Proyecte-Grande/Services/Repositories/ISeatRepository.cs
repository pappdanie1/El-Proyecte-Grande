using AspCinema.Models;
using El_Proyecte_Grande.Models;

namespace El_Proyecte_Grande.Services.Repositories;

public interface ISeatRepository
{
    Seat? GetById(int id);
    List<SeatReserved> GetReservedByScreening(int screeningId);
    void AddReservedSeat(SeatReserved reservedSeat);
}

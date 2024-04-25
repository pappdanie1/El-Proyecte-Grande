using AspCinema.Models;

namespace El_Proyecte_Grande.Services.DbSeed;

public interface ISeedSeats
{
    Task<List<Seat>> Seed();
}
using AspCinema.Models;
using El_Proyecte_Grande.Models;

namespace El_Proyecte_Grande.Services.DbSeed;

public interface ISeedScreenings
{
    Task<List<Screening>> Seed();
}
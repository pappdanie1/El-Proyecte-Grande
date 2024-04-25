using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public interface IOmdbApi
{
    Task<List<string>> GetMovies();
}
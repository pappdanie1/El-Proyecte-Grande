using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public interface IJsonProcessor
{
    List<Movie> ProcessMovies(string data);
}
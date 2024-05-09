using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public interface IJsonProcessor
{
    List<string> ProcessTitles(string data);
    List<Movie> ProcessMovies(List<string> data);
}
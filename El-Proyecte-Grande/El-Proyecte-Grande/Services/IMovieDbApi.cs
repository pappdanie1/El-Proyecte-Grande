namespace El_Proyecte_Grande.Services;

public interface IMovieDbApi
{
    Task<string> GetMovies();
}
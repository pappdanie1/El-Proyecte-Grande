using AspCinema.Models;
using El_Proyecte_Grande.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace El_Proyecte_Grande.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;
    private readonly IOmdbApi _omdbApi;
    private readonly IJsonProcessor _jsonProcessor;

    public MovieController(IMovieRepository movieRepository, IJsonProcessor jsonProcessor, IOmdbApi omdbApi)
    {
        _movieRepository = movieRepository;
        _jsonProcessor = jsonProcessor;
        _omdbApi = omdbApi;
    }

    [HttpGet]
    public async Task<ActionResult<List<Movie>>> GetAll()
    {
        try
        {
            var movieDb = _movieRepository.GetAll();
            if (movieDb.Count != 0)
            {
                return Ok(movieDb);
            }
            else
            {
                var movies = await _omdbApi.GetMovies();
                var processedMovies = _jsonProcessor.ProcessMovies(movies);
                foreach (var movie in processedMovies)
                {
                    _movieRepository.AddMovie(movie);
                }
                return Ok(processedMovies);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpGet("{movieId}")]
    public ActionResult<Movie> GetMovieById([FromRoute]int movieId)
    {
        try
        {
            var movie = _movieRepository.GetById(movieId);

            if (movie == null)
            {
                return NotFound("Movie not found");
            }

            return Ok(movie);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public ActionResult AddMovie(Movie movie)
    {
        try
        {
            _movieRepository.AddMovie(movie);
            return Ok(movie);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{movieId}")]
    public ActionResult<Movie> DeleteMovie([FromRoute] int movieId)
    {
        try
        {
            var movie = _movieRepository.GetById(movieId);
            _movieRepository.DeleteById(movieId);
            if (movie == null)
            {
                return NotFound("Movie not found");
            }

            return Ok(movie);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPatch]
    public ActionResult<Movie> UpdateMovie(Movie movie)
    {
        try
        {
            var existingMovie = _movieRepository.GetAll().FirstOrDefault(m => m.Id == movie.Id);
            if (existingMovie == null)
            {
                return NotFound("Movie not found");
            }

            _movieRepository.UpdateMovie(movie);
            var updated = _movieRepository.GetById(movie.Id);
            ;
            if (updated == null)
            {
                return BadRequest("Failed to update movie");
            }

            return Ok(updated);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
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

    public MovieController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpGet]
    public ActionResult<List<Movie>> GetAll()
    {
        try
        {
            var movies = _movieRepository.GetAll();
            return Ok(movies);
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
    public ActionResult<Movie> AddMovie(Movie movie)
    {
        try
        {
            return Ok(_movieRepository.AddMovie(movie));
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
            var movie = _movieRepository.DeleteById(movieId);
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
    
    [HttpPatch("{movieId}")]
    public ActionResult<Movie> UpdateMovie([FromRoute] int movieId, [FromBody] Movie updatedMovie)
    {
        try
        {
            var existingMovie = _movieRepository.GetAll().FirstOrDefault(m => m.Id == movieId);
            if (existingMovie == null)
            {
                return NotFound("Movie not found");
            }

            var updated = _movieRepository.UpdateMovie(movieId, updatedMovie);
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
using AspCinema.Models;
using El_Proyecte_Grande.Models;
using El_Proyecte_Grande.Services;
using El_Proyecte_Grande.Services.DbSeed;
using El_Proyecte_Grande.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace El_Proyecte_Grande.Controllers;


[ApiController]
[Route("[controller]")]

public class ScreeningController : ControllerBase
{

    private readonly IScreeningRepository _screeningRepository;
    private readonly ISeedScreenings _seedScreenings;


    public ScreeningController(IScreeningRepository screeningRepository, ISeedScreenings seedScreenings)
    {
        _screeningRepository = screeningRepository;
        _seedScreenings = seedScreenings;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllScreenings()
    {
        try
        {
            var screenings = _screeningRepository.GetAll();
            if (screenings.Count != 0)
            {
                return Ok(screenings);
            }
            else
            {
                var seeded = await _seedScreenings.Seed();
                foreach (var screening in seeded)
                {
                    _screeningRepository.AddScreening(screening);
                }
                return Ok(seeded);
            }
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("{screeningId}")]
    public ActionResult<Screening> GetScreeningById([FromRoute]int screeningId)
    {
        try
        {
            Screening screening = _screeningRepository.GetById(screeningId);

            if (screening == null)
            {
                return NotFound("Screening not found");
            }

            return Ok(screening);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public ActionResult PostScreening(Screening screening)
    {
        try
        {
            _screeningRepository.AddScreening(screening);
            return Ok(screening);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{screeningId}")]
    public ActionResult<Screening> DeleteScreening([FromRoute]int screeningId)
    {
        try
        {
            var screening = _screeningRepository.GetById(screeningId);
            _screeningRepository.DeleteById(screeningId);
            
            if (screening == null)
            {
                return NotFound("Screening not found");
            }

            return Ok(screening);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPatch]
    public ActionResult<Screening> UpdateScreening(Screening screening)
    {
        try
        {
            _screeningRepository.UpdateScreening(screening);
            var updatedScreening = _screeningRepository.GetById(screening.Id);
            return Ok(updatedScreening);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

}
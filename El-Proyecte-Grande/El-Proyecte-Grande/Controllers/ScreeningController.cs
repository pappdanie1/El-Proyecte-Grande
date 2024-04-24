using AspCinema.Models;
using El_Proyecte_Grande.Services;
using Microsoft.AspNetCore.Mvc;

namespace El_Proyecte_Grande.Controllers;


[ApiController]
[Route("[controller]")]

public class ScreeningController : ControllerBase
{

    private readonly IScreeningRepository _screeningRepository;


    public ScreeningController(IScreeningRepository screeningRepository)
    {
        _screeningRepository = screeningRepository;
    }

    [HttpGet]
    public ActionResult GetAllScreenings()
    {
        try
        {
            return Ok(_screeningRepository.GetAll());
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
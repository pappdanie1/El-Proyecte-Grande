using AspCinema.Models;
using El_Proyecte_Grande.Models;
using El_Proyecte_Grande.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace El_Proyecte_Grande.Controllers;

[ApiController]
[Route("[controller]")]
public class AuditoriumController : ControllerBase
{
    private readonly IAuditoriumRepository _auditoriumRepository;

    public AuditoriumController(IAuditoriumRepository auditoriumRepository)
    {
        _auditoriumRepository = auditoriumRepository;
    }

    [HttpGet, Authorize (Roles = "Admin, User")]
    public ActionResult GetAuditoriumById(int id)
    {
        try
        {
            return Ok(_auditoriumRepository.GetById(id));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost, Authorize (Roles = "Admin")]
    public ActionResult AddAuditorium(Auditorium auditorium)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            _auditoriumRepository.AddAuditorium(auditorium);
            return Ok(auditorium);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete, Authorize (Roles = "Admin")]
    public ActionResult DeleteAuditorium(int id)
    {
        try
        {
            var auditoriumToDelete = _auditoriumRepository.GetById(id);
            _auditoriumRepository.DeleteById(id);

            return Ok(auditoriumToDelete);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
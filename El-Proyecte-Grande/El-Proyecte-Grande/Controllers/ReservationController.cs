using System.Security.Claims;
using AspCinema.Models;
using El_Proyecte_Grande.Data;
using El_Proyecte_Grande.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace El_Proyecte_Grande.Controllers;

[ApiController]
[Route("[controller]"), Authorize(Roles = "User")]
public class ReservationController : ControllerBase
{
    private readonly IReservationRepository _reservationRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReservationController(IReservationRepository reservationRepository, UserManager<ApplicationUser> userManager)
    {
        _reservationRepository = reservationRepository;
        _userManager = userManager;
    }


    [HttpGet("AllByUser")]
    public ActionResult GetAllReservationByUser(string username)
    {
        try
        {
            return Ok(_reservationRepository.GetAllByUser(username));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("AllByScreening")]
    public ActionResult GetAllReservationByScreening(int id)
    {
        try
        {
            return Ok(_reservationRepository.GetAllByScreening(id));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("OneById")]
    public ActionResult GetReservationById(int id, string username)
    {
        try
        {
            return Ok(_reservationRepository.GetById(id, username));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost("AddReservation")]
    public async Task<ActionResult> AddReservation(Reservation reservation)
    {
        try
        {
            var userId = User.FindAll(ClaimTypes.NameIdentifier).Skip(1).FirstOrDefault().Value;
            Console.WriteLine(userId);
            var user = await _userManager.FindByIdAsync(userId);
            reservation.CustomerId = userId;
            reservation.Customer = user;
            
            _reservationRepository.AddReservation(reservation);
            return Ok(reservation);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete]
    public ActionResult DeleteReservation(int id)
    {
        try
        {
            var userName = User.FindAll(ClaimTypes.Name).FirstOrDefault().Value;
            var reservation = _reservationRepository.GetById(id, userName);
            _reservationRepository.DeleteById(id);
            
            return Ok(reservation);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
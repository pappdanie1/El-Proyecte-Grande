using System.Security.Claims;
using AspCinema.Models;
using El_Proyecte_Grande.Data;
using El_Proyecte_Grande.Models;
using El_Proyecte_Grande.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace El_Proyecte_Grande.Controllers;

[ApiController]
[Route("[controller]"),Authorize(Roles = "User, Admin")]
public class ReservationController : ControllerBase
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IScreeningRepository _screeningRepository;
    private readonly ISeatRepository _seatRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReservationController(IReservationRepository reservationRepository, UserManager<ApplicationUser> userManager, IScreeningRepository screeningRepository, ISeatRepository seatRepository)
    {
        _reservationRepository = reservationRepository;
        _userManager = userManager;
        _screeningRepository = screeningRepository;
        _seatRepository = seatRepository;
    }


    [HttpGet("AllByUser")]
    public ActionResult GetAllReservationByUser(string username)
    {
        try
        {
            IList<Reservation> reservations = _reservationRepository.GetAllByUser(username);

            foreach (var reserve in reservations)
            {
                foreach (var rs in reserve.ReservedSeats)
                {
                    rs.Screening = null;
                    rs.Seat.Auditorium = null;
                }
            }
            
            return Ok(reservations);
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
    public async Task<ActionResult> AddReservation([FromBody] Reservation reservation, [FromQuery] List<int> seatIds, int screeningId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindAll(ClaimTypes.NameIdentifier).Skip(1).FirstOrDefault().Value;
            var user = await _userManager.FindByIdAsync(userId);
            var screening = _screeningRepository.GetById(screeningId);
            reservation.Customer = user;
            reservation.Screening = screening;
            _reservationRepository.AddReservation(reservation);

            foreach (var seatId in seatIds)
            {
                var seat = _seatRepository.GetById(seatId);
                var seatReserved = new SeatReserved
                {
                    Seat = seat,
                    Reservation = reservation,
                    Screening = screening
                };
                _seatRepository.AddReservedSeat(seatReserved);
            }
            
            
            return Ok(reservation.ReservedSeats);
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
using AspCinema.Models;
using Microsoft.AspNetCore.Identity;

namespace El_Proyecte_Grande.Data;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
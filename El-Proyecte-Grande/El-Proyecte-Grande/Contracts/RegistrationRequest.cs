using System.ComponentModel.DataAnnotations;

namespace El_Proyecte_Grande.Contracts;

public record RegistrationRequest(
    [Required]string Email,
    [Required]string Name,
    [Required]string PhoneNumber,
    [Required]string Username,
    [Required]string Password);
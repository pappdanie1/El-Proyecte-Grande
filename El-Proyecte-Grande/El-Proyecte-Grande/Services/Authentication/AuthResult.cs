namespace El_Proyecte_Grande.Services.Authentication;

public record AuthResult(
    bool Success,
    string Name,
    string PhoneNumber,
    string Email,
    string UserName,
    string Token,
    string Role)
{
    public readonly Dictionary<string, string> ErrorMessages = new();
}
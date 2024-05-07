namespace El_Proyecte_Grande.Services.Authentication;

public record AuthResult(
    bool Success,
    string Name,
    string PhoneNumber,
    string Email,
    string UserName,
    string Token)
{
    public readonly Dictionary<string, string> ErrorMessages = new();
}
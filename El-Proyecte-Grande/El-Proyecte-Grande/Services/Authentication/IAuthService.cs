namespace El_Proyecte_Grande.Services.Authentication;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(string email, string name, string phoneNumber, string username, string password, string role);
    Task<AuthResult> LoginAsync(string email, string password);
}
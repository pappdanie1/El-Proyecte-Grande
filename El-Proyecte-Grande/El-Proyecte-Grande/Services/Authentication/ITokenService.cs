using Microsoft.AspNetCore.Identity;

namespace El_Proyecte_Grande.Services.Authentication;

public interface ITokenService
{
    public string CreateToken(IdentityUser user, string role);
}
using El_Proyecte_Grande.Data;
using Microsoft.AspNetCore.Identity;

namespace El_Proyecte_Grande.Services.Authentication;

public interface ITokenService
{
    public string CreateToken(ApplicationUser user, string role);
}
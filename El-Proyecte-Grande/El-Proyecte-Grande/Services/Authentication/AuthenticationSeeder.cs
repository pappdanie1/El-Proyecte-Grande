using Microsoft.AspNetCore.Identity;
using IConfiguration = Castle.Core.Configuration.IConfiguration;

namespace El_Proyecte_Grande.Services.Authentication;

public class AuthenticationSeeder
{
    private RoleManager<IdentityRole> _roleManager;
    private UserManager<IdentityUser> _userManager;
    private IConfigurationRoot _config;

    public AuthenticationSeeder(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
    }
    
    public void AddRoles()
    {
        var tAdmin = CreateAdminRole(_roleManager);
        tAdmin.Wait();

        var tUser = CreateUserRole(_roleManager);
        tUser.Wait();
    }
    
    public void AddAdmin()
    {
        var tAdmin = CreateAdminIfNotExists();
        tAdmin.Wait();
    }

    private async Task CreateAdminIfNotExists()
    {
        var adminInDb = await _userManager.FindByEmailAsync("admin@admin.com");
        if (adminInDb == null)
        {
            var admin = new IdentityUser { UserName = "admin", Email = "admin@admin.com" };
            var adminCreated = await _userManager.CreateAsync(admin, "admin123");

            if (adminCreated.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }

    private async Task CreateAdminRole(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(_config["Roles:1"])); 
    }

    async Task CreateUserRole(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(_config["Roles:2"]));
    }
}
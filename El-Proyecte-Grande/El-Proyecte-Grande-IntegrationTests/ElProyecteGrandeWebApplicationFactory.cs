using El_Proyecte_Grande.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace El_Proyecte_Grande_IntegrationTests;

public class ElProyecteGrandeWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = Guid.NewGuid().ToString();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var AspCinemaContextDescriptor =
                services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AspCinemaContext>));

            services.Remove(AspCinemaContextDescriptor);

            services.AddDbContext<AspCinemaContext>(options => options.UseInMemoryDatabase(_dbName));
            

            using var scope = services.BuildServiceProvider().CreateScope();

            var AspCinemaContext = scope.ServiceProvider.GetRequiredService<AspCinemaContext>();
            AspCinemaContext.Database.EnsureDeleted();
            AspCinemaContext.Database.EnsureCreated();
        });
    }
}
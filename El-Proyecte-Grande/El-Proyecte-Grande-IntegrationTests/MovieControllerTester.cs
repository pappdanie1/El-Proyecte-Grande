using System.Net;
using System.Net.Http.Json;
using AspCinema.Models;

namespace El_Proyecte_Grande_IntegrationTests;

[Collection("IntegrationTests")] 
public class MovieControllerTester
{
    private readonly ElProyecteGrandeWebApplicationFactory _app;
    private readonly HttpClient _client;

    public MovieControllerTester()
    {
        _app = new ElProyecteGrandeWebApplicationFactory();
        _client = _app.CreateClient();
    }

    [Fact]
    public async Task TestGetMovies()
    {
        var response = await _client.GetAsync("Movie");

        response.EnsureSuccessStatusCode();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
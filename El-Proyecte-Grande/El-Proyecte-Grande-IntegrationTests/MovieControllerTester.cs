using System.Net;
using System.Net.Http.Json;
using AspCinema.Models;
using El_Proyecte_Grande.Models;

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
    
    [Fact]
    public async Task TestGetByValidId()
    {
        var movie = new Movie
        {
            Title = "asd"
        };
        var AddResponse = await _client.PostAsJsonAsync("Movie", movie);
        AddResponse.EnsureSuccessStatusCode();

        var response = await _client.GetAsync("Movie/1");
        response.EnsureSuccessStatusCode();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task TestGetByInvalidId()
    {
        var response = await _client.GetAsync("Movie/1");
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task TestAddValidData()
    {
        var movie = new Movie
        {
            Title = "asd"
        };
        var AddResponse = await _client.PostAsJsonAsync("Movie", movie);
        AddResponse.EnsureSuccessStatusCode();
        
        Assert.Equal(HttpStatusCode.OK, AddResponse.StatusCode);
    }
    
    [Fact]
    public async Task TestAddInvalidData()
    {
        var movie = new Movie
        {
        };
        var AddResponse = await _client.PostAsJsonAsync("Movie", movie);
        
        Assert.Equal(HttpStatusCode.BadRequest, AddResponse.StatusCode);
    }
    
    [Fact]
    public async Task TestDeleteValidId()
    {
        var movie = new Movie
        {
            Title = "asd"
        };
        var AddResponse = await _client.PostAsJsonAsync("Movie", movie);
        AddResponse.EnsureSuccessStatusCode();

        var response = await _client.DeleteAsync("Movie/1");
        response.EnsureSuccessStatusCode();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task TestDeleteInvalidId()
    {
        var response = await _client.DeleteAsync("Movie/1");
        
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
    
}
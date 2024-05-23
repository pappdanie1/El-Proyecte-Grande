using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using El_Proyecte_Grande.Contracts;

namespace El_Proyecte_Grande_IntegrationTests;

[Collection("IntegrationTests")] 
public class SeatControllerTester
{
    private readonly ElProyecteGrandeWebApplicationFactory _app;
    private readonly HttpClient _client;

    public SeatControllerTester()
    {
        _app = new ElProyecteGrandeWebApplicationFactory();
        _client = _app.CreateClient();
    }

    [Fact]
    public async Task GetReservedSeatOk()
    {
        var loginResponse = await _client.PostAsJsonAsync("Auth/Login", new {Email = "admin@admin.com", Password = "admin123"});
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);
        
        var response = await _client.GetAsync("Seat/ReservedByScreening");
        response.EnsureSuccessStatusCode();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GetReservedSeatInvalidId()
    {
        var loginResponse = await _client.PostAsJsonAsync("Auth/Login", new {Email = "admin@admin.com", Password = "admin123"});
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);

        string invalidId = "invalid_id";
        
        var response = await _client.GetAsync($"Seat/ReservedByScreening?screeningId={invalidId}");
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task GetReservedSeatUnauthorized()
    {
        var response = await _client.GetAsync("Seat/ReservedByScreening");
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
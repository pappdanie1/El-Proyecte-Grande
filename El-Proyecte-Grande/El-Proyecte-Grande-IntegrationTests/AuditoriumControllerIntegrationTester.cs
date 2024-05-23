using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using El_Proyecte_Grande.Contracts;
using El_Proyecte_Grande.Models;
using Xunit.Abstractions;

namespace El_Proyecte_Grande_IntegrationTests;

[Collection("IntegrationTests")]
public class AuditoriumControllerIntegrationTester
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ElProyecteGrandeWebApplicationFactory _app;
    private readonly HttpClient _client;

    public AuditoriumControllerIntegrationTester(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _app = new ElProyecteGrandeWebApplicationFactory();
        _client = _app.CreateClient();
    }
    
    [Fact]
    public async Task GetAuditoriumById_ReturnsOk()
    {
        var loginResponse =
            await _client.PostAsJsonAsync("Auth/Login", new { Email = "admin@admin.com", Password = "admin123" });
        loginResponse.EnsureSuccessStatusCode();
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();

        // Set token in authorization header
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);
        var response = await _client.GetAsync("/Auditorium?id=1");

        response.EnsureSuccessStatusCode();

        var auditorium = await response.Content.ReadFromJsonAsync<Auditorium>();
        
        Assert.NotNull(auditorium);
        Assert.Equal(1, auditorium.Id);
    }

    [Fact]
    public async Task GetAuditoriumById_WithoutLogin_ReturnsUnauthorized()
    {
        var response = await _client.GetAsync("/Auditorium?id=1");
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetAuditoriumById_ReturnsNoContent_WhenIdIsMissing()
    {
        // Log in and set the token
        var loginResponse = await _client.PostAsJsonAsync("Auth/Login", new { Email = "admin@admin.com", Password = "admin123" });
        loginResponse.EnsureSuccessStatusCode();
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);
    
        // Request without id parameter
        var response = await _client.GetAsync("/Auditorium");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    
    [Fact]
    public async Task AddAuditorium_ReturnsOk()
    {
        // Log in and set the token
        var loginResponse = await _client.PostAsJsonAsync("Auth/Login", new { Email = "admin@admin.com", Password = "admin123" });
        loginResponse.EnsureSuccessStatusCode();
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);
    
        // Create a new auditorium
        var newAuditorium = new Auditorium { Name = "New Auditorium" };
    
        // Send the POST request
        var response = await _client.PostAsJsonAsync("/Auditorium", newAuditorium);

        response.EnsureSuccessStatusCode();

        var returnedAuditorium = await response.Content.ReadFromJsonAsync<Auditorium>();
        Assert.NotNull(returnedAuditorium);
        Assert.Equal("New Auditorium", returnedAuditorium.Name);
    }
    
    [Fact]
    public async Task GetAuditoriumById_WithoutAdminRole_ReturnsForbidden()
    {
        // Register test
        var email = "test@test.com";
        var name = "test test";
        var phone = "123";
        var username = "test";
        var password = "password123";
        var role = "User";
        
        var registrationResponse = await _client.PostAsJsonAsync("Auth/Register",
            new { Email = email, Name = name, PhoneNumber = phone, Username = username, Password = password, Role = role });
        
        registrationResponse.EnsureSuccessStatusCode();
        
        // Login 
        var loginResponse =
            await _client.PostAsJsonAsync("Auth/Login", new { Email = "test@test.com", Password = "password123" });
        loginResponse.EnsureSuccessStatusCode();
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();

        // Set token in authorization header
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);
        
        // Create a new auditorium
        var newAuditorium = new Auditorium { Name = "New Auditorium" };
    
        // Send the POST request
        var response = await _client.PostAsJsonAsync("/Auditorium", newAuditorium);
        
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
    
    [Fact]
    public async Task AddAuditorium_ReturnsBadRequest_WhenModelIsInvalid()
    {
        // Log in and set the token
        var loginResponse = await _client.PostAsJsonAsync("Auth/Login", new { Email = "admin@admin.com", Password = "admin123" });
        loginResponse.EnsureSuccessStatusCode();
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);
    
        // Create an invalid auditorium (missing Name)
        var invalidAuditorium = new Auditorium{};

        var response = await _client.PostAsJsonAsync("/Auditorium", invalidAuditorium);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    // TO DO on delete cascade at Seat entity ??
    /*[Fact]
    public async Task DeleteAuditorium_ReturnsOk()
    {
        var loginResponse =
            await _client.PostAsJsonAsync("Auth/Login", new { Email = "admin@admin.com", Password = "admin123" });
        loginResponse.EnsureSuccessStatusCode();
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();

        // Set token in authorization header
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);
        
        var response = await _client.DeleteAsync("/Auditorium?id=1");
        // Log response for debugging
        var responseContent = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine($"Response Content: {responseContent}");

        response.EnsureSuccessStatusCode();

        var deletedAuditorium = await response.Content.ReadFromJsonAsync<Auditorium>();
        Assert.NotNull(deletedAuditorium);
        Assert.Equal(1, deletedAuditorium.Id);
    }*/
}
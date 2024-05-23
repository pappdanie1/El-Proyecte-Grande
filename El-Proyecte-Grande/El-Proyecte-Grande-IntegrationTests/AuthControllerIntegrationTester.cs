using System.Net;
using System.Net.Http.Json;
using El_Proyecte_Grande.Contracts;
using Xunit.Abstractions;

namespace El_Proyecte_Grande_IntegrationTests;

[Collection("IntegrationTests")]
public class AuthControllerIntegrationTester
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ElProyecteGrandeWebApplicationFactory _app;
    private readonly HttpClient _client;

    public AuthControllerIntegrationTester(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _app = new ElProyecteGrandeWebApplicationFactory();
        _client = _app.CreateClient();
    }

    [Fact]
    public async Task Register_ReturnsCreated()
    {
        var email = "test@test.com";
        var name = "test test";
        var phone = "123";
        var username = "test";
        var password = "password123";
        var role = "User";
        
        var registrationResponse = await _client.PostAsJsonAsync("Auth/Register",
            new { Email = email, Name = name, PhoneNumber = phone, Username = username, Password = password, Role = role });
        
        registrationResponse.EnsureSuccessStatusCode();

        var responseContent = await registrationResponse.Content.ReadFromJsonAsync<AuthResponse>();

        Assert.Equal(responseContent.Email, email);
        Assert.Equal(responseContent.Name, name);
        Assert.Equal(responseContent.UserName, username);
        Assert.Equal(HttpStatusCode.Created, registrationResponse.StatusCode);
    }

    [Fact]
    public async Task Register_WithInvalidData_ReturnsBadRequest()
    {
        var email = "";
        var name = "";
        var phone = "";
        var username = "";
        var password = "";
        var role = "User";
    
        var registrationResponse = await _client.PostAsJsonAsync("Auth/Register",
            new { Email = email, Name = name, PhoneNumber = phone, Username = username, Password = password, Role = role });
     
        Assert.Equal(HttpStatusCode.BadRequest, registrationResponse.StatusCode);
    }
    
    [Fact]
    public async Task Register_WithInvalidData_ReturnsBadRequestWithErrors()
    {
        var email = "";
        var name = "";
        var phone = "";
        var username = "";
        var password = "";
        var role = "User";
    
        var registrationResponse = await _client.PostAsJsonAsync("Auth/Register",
            new { Email = email, Name = name, PhoneNumber = phone, Username = username, Password = password, Role = role });

        var responseContent = await registrationResponse.Content.ReadAsStringAsync();
        
        Assert.Equal(HttpStatusCode.BadRequest, registrationResponse.StatusCode);
        Assert.Contains("The Email field is required.", responseContent);
        Assert.Contains("The Name field is required.", responseContent);
    }
    
    [Fact]
    public async Task Register_WithExistingEmail_Returns_BadRequest()
    {
        var email = "test@test.com";
        var name = "test test";
        var phone = "123";
        var username = "test";
        var password = "password123";
        var role = "User";
        
        var registrationResponse1 = await _client.PostAsJsonAsync("Auth/Register",
            new { Email = email, Name = name, PhoneNumber = phone, Username = username, Password = password, Role = role });
        registrationResponse1.EnsureSuccessStatusCode();
        
        var registrationResponse2= await _client.PostAsJsonAsync("Auth/Register",
            new { Email = email, Name = "test", Phone = "123", Username = "test1", Password = "test123", Role = role });
        
        Assert.Equal(HttpStatusCode.BadRequest, registrationResponse2.StatusCode);
    }
    
    [Fact]
    public async Task Login_Returns_SuccessStatusWithToken()
    {
        var email = "admin@admin.com";
        var password = "admin123";
        
        var loginResponse = await _client.PostAsJsonAsync("Auth/Login",
            new { Email = email, Password = password});
        
        loginResponse.EnsureSuccessStatusCode();

        var responseContent = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();

        Assert.NotNull(responseContent);
        Assert.Equal((HttpStatusCode.OK), loginResponse.StatusCode);
        Assert.False(string.IsNullOrEmpty(responseContent.Token));
    }
    
    [Fact]
    public async Task Login_WithInvalidData_Returns_BadRequest()
    {
        var email = " ";
        var password = " ";
     
        var loginResponse = await _client.PostAsJsonAsync("Auth/Login",
            new { Email = email, Password = password});
       
        Assert.Equal(HttpStatusCode.BadRequest, loginResponse.StatusCode);
    }
    
    [Fact]
    public async Task Login_WithIncorrectData_Returns_BadRequest()
    {
        var email = "admin@admin.com";
        var password = "123";
        
        var loginResponse = await _client.PostAsJsonAsync("Auth/Login",
            new { Email = email, Password = password});
       
        Assert.Equal(HttpStatusCode.BadRequest, loginResponse.StatusCode);
    }
}
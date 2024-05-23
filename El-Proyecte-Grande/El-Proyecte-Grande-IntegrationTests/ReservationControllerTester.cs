using System.Net;
using System.Net.Http.Json;
using AspCinema.Models;
using El_Proyecte_Grande.Contracts;
using El_Proyecte_Grande.Data;
using El_Proyecte_Grande.Models;

namespace El_Proyecte_Grande_IntegrationTests;

public class ReservationControllerTester
{
    private readonly ElProyecteGrandeWebApplicationFactory _app;
    private readonly HttpClient _client;

    public ReservationControllerTester()
    {
        _app = new ElProyecteGrandeWebApplicationFactory();
        _client = _app.CreateClient();
    }
    
    [Fact]
    public async Task TestGetAllReservationsByUser()
    {
        var loginResponse = await _client.PostAsJsonAsync("Auth/Login", new {Email = "admin@admin.com", Password = "admin123"});
        loginResponse.EnsureSuccessStatusCode();
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();
            
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResult.Token);
            
        var response = await _client.GetAsync("Reservation/AllByUser?username=admin");
            
        response.EnsureSuccessStatusCode();
            
        var data = await response.Content.ReadFromJsonAsync<IEnumerable<List<Reservation>>>();
        Assert.NotNull(data);
    }
    
    [Fact]
    public async Task TestGetAllReservationsByUserNoLogin()
    {
        var response = await _client.GetAsync("Reservation/AllByUser?username=admin");
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task TestGetAllReservationsByScreening()
    {
        var loginResponse = await _client.PostAsJsonAsync("Auth/Login", new {Email = "admin@admin.com", Password = "admin123"});
        loginResponse.EnsureSuccessStatusCode();
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();
            
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResult.Token);
            
        var response = await _client.GetAsync("/Reservation/AllByScreening?id=1");
            
        response.EnsureSuccessStatusCode();
            
        var data = await response.Content.ReadFromJsonAsync<IEnumerable<List<Reservation>>>();
        Assert.NotNull(data);
    }
    
    /*
    [Fact]
    public async Task AddReservation()
    {
        var loginResponse = await _client.PostAsJsonAsync("Auth/Login", new {Email = "admin@admin.com", Password = "admin123"});
        loginResponse.EnsureSuccessStatusCode();
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();
            
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResult.Token);
        
        var movie = new Movie
        {
            Title = "asd"
        };
        var AddMovieResponse = await _client.PostAsJsonAsync("Movie", movie);
        AddMovieResponse.EnsureSuccessStatusCode();

        var existingAuditorium = new Auditorium { Id = 1, Name = "Audit1" };
        existingAuditorium.Seats.Add(new Seat { Number = 1, Row = 1 });
        existingAuditorium.Seats.Add(new Seat { Number = 2, Row = 2 });
        
        var AddAuditoriumResponse = await _client.PostAsJsonAsync("Auditorium", existingAuditorium);
        AddAuditoriumResponse.EnsureSuccessStatusCode();

        var screening = new Screening
        {
            Movie = movie,
            Auditorium = existingAuditorium,
            Start = DateTime.UtcNow.AddDays(1)
        };
        
        var ScreeningResponse = await _client.PostAsJsonAsync("Screening", screening);
        ScreeningResponse.EnsureSuccessStatusCode();

        var reservation = new Reservation
        {
            Id = 0,
            Screening = new Screening
            {
                Id = 0,
                Start = DateTime.UtcNow.AddDays(1),
                Auditorium = new Auditorium
                {
                    Id = 0,
                    Name = "string",
                },
                Movie = new Movie
                {
                    Id = 0,
                    Title = "string",
                    Director = "string",
                    Cast = "string",
                    Description = "string",
                    DurationInSec = "string",
                    Poster = "string",
                    Genres = "string",
                },
            },
            Customer = new ApplicationUser
            {
                Id = "string",
                UserName = "string",
                NormalizedUserName = "string",
                Email = "string",
                NormalizedEmail = "string",
                EmailConfirmed = true,
                PasswordHash = "string",
                SecurityStamp = "string",
                ConcurrencyStamp = "string",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnd = DateTime.UtcNow.AddDays(1),
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Name = "string",
                PhoneNumber = "string",
            },
        };
            
        var response = await _client.PostAsJsonAsync("Reservation/AddReservation?seatIds=1&screeningId=1", reservation);
            
        response.EnsureSuccessStatusCode();
            
        var data = await response.Content.ReadFromJsonAsync<IEnumerable<List<Reservation>>>();
        Assert.NotNull(data);
    }
    */
}
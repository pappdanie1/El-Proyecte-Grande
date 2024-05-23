using System.Net;
using System.Net.Http.Json;
using AspCinema.Models;
using El_Proyecte_Grande.Models;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace El_Proyecte_Grande_IntegrationTests;

[Collection("IntegrationTests")] 
public class ScreeningControllerTester
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ElProyecteGrandeWebApplicationFactory _app;
    private readonly HttpClient _client;

    public ScreeningControllerTester(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _app = new ElProyecteGrandeWebApplicationFactory();
        _client = _app.CreateClient();
    }

    [Fact]
    public async Task GetAllScreeningOk()
    {
        var response = await _client.GetAsync("Screening");
        response.EnsureSuccessStatusCode();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var screenings = await response.Content.ReadFromJsonAsync<List<Screening>>();
        
        Assert.NotNull(screenings);

        foreach (var screening in screenings)
        {
            Assert.False(screening.Movie == null);
        }
    }

    /*[Fact]
    public async Task GetScreeningByIdOk()
    {
        int id = 1;
        Screening screening = new Screening
        {
            Id = 1,
            Reservations = new List<Reservation>(),
            ReservedSeats = new List<SeatReserved>(),
            Auditorium = new Auditorium{ Id = 1, Name = "Audit1", Screenings = new List<Screening>(), Seats = new List<Seat>()},
            Movie = new Movie{Id = 1},
            Start = new DateTime(2024, 05,23)
        };
        
        var addScreeningResp = await _client.PostAsJsonAsync("Screening", screening);
        _testOutputHelper.WriteLine(addScreeningResp.ToString());
        addScreeningResp.EnsureSuccessStatusCode();
        
        var response = await _client.GetAsync($"Screening?screeningId={id}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Screening>();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
    }*/

    [Fact]
    public async Task GetScreeningByIdNotFound()
    {
        var response = await _client.GetAsync($"Screening/1");
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    /*[Fact]
    public async Task PostScreeningSuccess()
    {
        Screening screening = new Screening
        {
            Id = 0,
            Reservations = new List<Reservation>(),
            ReservedSeats = new List<SeatReserved>(),
            Start = new DateTime(2024, 05, 05, 10, 00, 00),
            Auditorium = new Auditorium{ Id = 1, Name = "Audit1", Screenings = new List<Screening>(), Seats = new List<Seat>()},
            Movie = new Movie{Id = 1},
        };

        var response = await _client.PostAsJsonAsync("Screening", screening);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }*/
}
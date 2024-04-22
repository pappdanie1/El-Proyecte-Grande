using El_Proyecte_Grande.Data;
using El_Proyecte_Grande.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddSingleton<IMovieDbApi, MovieDbApi>();
builder.Services.AddSingleton<IJsonProcessor, JsonProcessor>();
builder.Services.AddScoped<IScreeningRepository, ScreeningRepository>();
builder.Services.AddDbContext<ElProyecteGrandeContext>();


//add cors
/*builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:5000", "http://localhost:5229")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseRouting();

//app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
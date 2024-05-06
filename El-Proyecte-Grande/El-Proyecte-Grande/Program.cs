using System.Text;
using El_Proyecte_Grande.Data;
using El_Proyecte_Grande.Services;
using El_Proyecte_Grande.Services.Authentication;
using El_Proyecte_Grande.Services.DbSeed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


AddServices();
ConfigureSwagger();
AddDbContext();
AddAuthentication();
AddIdentity();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(options => options.AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void AddServices()
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IMovieRepository, MovieRepository>();
    builder.Services.AddScoped<ISeatRepository, SeatRepository>();
    builder.Services.AddSingleton<IMovieDbApi, MovieDbApi>();
    builder.Services.AddSingleton<IJsonProcessor, JsonProcessor>();
    builder.Services.AddSingleton<IOmdbApi, OmdbApi>();
    builder.Services.AddScoped<ISeedScreenings, SeedScreenings>();
    builder.Services.AddScoped<ISeedSeats, SeedSeats>();
    builder.Services.AddScoped<IScreeningRepository, ScreeningRepository>();
    builder.Services.AddScoped<IAuthService, AuthService>();
}

void ConfigureSwagger()
{
    
}

void AddDbContext()
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ElProyecteGrandeContext>(options => 
        options.UseSqlServer(connectionString, sqlOption =>
        {
            sqlOption.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        }));
    builder.Services.AddDbContext<UsersContext>(options =>
        options.UseSqlServer(connectionString, sqlOption =>
        {
            sqlOption.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        }));
}

void AddAuthentication()
{
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JwtSettings:ValidIssuer"],
                ValidAudience = builder.Configuration["JwtSettings:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["SigningKey:IssuerSigningKey"])
                ),
            };
        });
}

void AddIdentity()
{
    builder.Services
        .AddIdentityCore<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddEntityFrameworkStores<UsersContext>();
}
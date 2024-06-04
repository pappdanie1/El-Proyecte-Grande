using System.Text;
using System.Text.Json.Serialization;
using El_Proyecte_Grande.Data;
using El_Proyecte_Grande.Services;
using El_Proyecte_Grande.Services.Authentication;
using El_Proyecte_Grande.Services.DbSeed;
using El_Proyecte_Grande.Services.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


AddServices();
ConfigureSwagger();
AddDbContext();
AddAuthentication();
AddIdentity();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    
    var services = scope.ServiceProvider;
    var dbContext = services.GetService<AspCinemaContext>();
    
    var authenticationSeeder = scope.ServiceProvider.GetRequiredService<AuthenticationSeeder>();
    if (dbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
    {
        scope.ServiceProvider.GetService<AspCinemaContext>().Database.Migrate();
    }
    authenticationSeeder.AddRoles();
    authenticationSeeder.AddAdmin();
    
    dbContext.Seed();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void AddServices()
{
    builder.Services.AddControllers();
    builder.Services.AddControllers().AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler =  ReferenceHandler.IgnoreCycles);
    builder.Services.AddScoped<IMovieRepository, MovieRepository>();
    builder.Services.AddScoped<ISeatRepository, SeatRepository>();
    builder.Services.AddSingleton<IMovieDbApi, MovieDbApi>();
    builder.Services.AddSingleton<IJsonProcessor, JsonProcessor>();
    builder.Services.AddSingleton<IOmdbApi, OmdbApi>();
    builder.Services.AddScoped<ISeedScreenings, SeedScreenings>();
    builder.Services.AddScoped<IScreeningRepository, ScreeningRepository>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<AuthenticationSeeder>();
    builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
    builder.Services.AddScoped<IAuditoriumRepository, AuditoriumRepository>();
}

void ConfigureSwagger()
{
    builder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
        });
    });
}

void AddDbContext()
{
    var connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__DEFAULTCONNECTION") ?? 
                           builder.Configuration.GetConnectionString("DefaultConnection");
    
    builder.Services.AddDbContext<AspCinemaContext>(options =>
        options.UseNpgsql(connectionString)
    );
}

void AddAuthentication()
{
    var validIssuer = Environment.GetEnvironmentVariable("VALIDISSUER") ??
                      builder.Configuration["JwtSettings:ValidIssuer"];
    var validAudience = Environment.GetEnvironmentVariable("VALIDAUDIENCE") ??
                        builder.Configuration["JwtSettings:ValidAudience"];
    var issuerSigningKey = Environment.GetEnvironmentVariable("ISSUERSIGNINGKEY") ??
                          builder.Configuration["SigningKey:IssuerSigningKey"];
    
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
                                      ValidIssuer = validIssuer,
                                      ValidAudience = validAudience,
                                      IssuerSigningKey = new SymmetricSecurityKey(
                                          Encoding.UTF8.GetBytes(issuerSigningKey)
                                      ),
                                  };
                              });
}

void AddIdentity()
{
    builder.Services
        .AddIdentityCore<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AspCinemaContext>();
}

public partial class Program { }
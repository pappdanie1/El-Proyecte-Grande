using AspCinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace El_Proyecte_Grande.Data;

public class AspCinemaContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public AspCinemaContext(DbContextOptions<AspCinemaContext> options) : base(options)
    {
    }
    
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Screening> Screenings { get; set; }
    public DbSet<Auditorium> Auditoriums { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Seat> Seats { get; set; }
    
    public DbSet<SeatReserved> SeatReserveds { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        List<Auditorium> auditoria = new List<Auditorium>()
        {
            new Auditorium { Id = 1, Name = "Audit1", SeatNo = 30 },
            new Auditorium { Id = 2, Name = "Audit2", SeatNo = 30 },
            new Auditorium { Id = 3, Name = "Audit3", SeatNo = 30 },
            new Auditorium { Id = 4, Name = "Audit4", SeatNo = 30 },
            new Auditorium { Id = 5, Name = "Audit5", SeatNo = 30 }
        };


        modelBuilder.Entity<Movie>().HasIndex(u => u.Title).IsUnique();
    
        modelBuilder.Entity<Auditorium>().HasData(auditoria);
        
        modelBuilder.Entity<SeatReserved>()
            .HasOne(sr => sr.Screening)
            .WithMany(s => s.ReservedSeats)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<SeatReserved>()
            .HasOne(sr => sr.Seat)
            .WithMany(s => s.Reserveds)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<SeatReserved>()
            .HasOne(sr => sr.Reservation)
            .WithMany(s => s.Seats)
            .OnDelete(DeleteBehavior.NoAction);
    }
    
    //initialdb created
}
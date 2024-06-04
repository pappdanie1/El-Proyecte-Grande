using AspCinema.Models;
using El_Proyecte_Grande.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;

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
    public DbSet<SeatReserved> ReservedSeats { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        List<Auditorium> auditoria = new List<Auditorium>()
        {
            new Auditorium { Id = 1, Name = "Audit1", Seats = new List<Seat>() },
            new Auditorium { Id = 2, Name = "Audit2", Seats = new List<Seat>() },
            new Auditorium { Id = 3, Name = "Audit3", Seats = new List<Seat>() },
            new Auditorium { Id = 4, Name = "Audit4", Seats = new List<Seat>() },
            new Auditorium { Id = 5, Name = "Audit5", Seats = new List<Seat>() }
        };


        modelBuilder.Entity<Movie>().HasIndex(u => u.Title).IsUnique();
    
        modelBuilder.Entity<Auditorium>().HasData(auditoria);
        
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Reservations)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Screening)
            .WithMany(s => s.Reservations)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<SeatReserved>()
            .HasOne(s => s.Reservation)
            .WithMany(r => r.ReservedSeats)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<SeatReserved>()
            .HasOne(s => s.Screening)
            .WithMany(s => s.ReservedSeats)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<SeatReserved>()
            .HasOne(s => s.Seat)
            .WithMany(s => s.ReservedSeats)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Seat>()
            .HasOne(s => s.Auditorium)
            .WithMany(a => a.Seats)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Screening>()
            .HasOne(s => s.Auditorium)
            .WithMany(a => a.Screenings)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Screening>()
            
            .HasOne(s => s.Movie)
            .WithMany(m => m.Screenings)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Screening>(entity =>
        {
            entity.Property(e => e.Start).HasColumnType("timestamptz");
        });

    }
    
    public void Seed()
    {
        if (!Seats.Any())
        {
            foreach (var auditorium in Auditoriums)
            {
                var rows = 5;
                var seatsInRow = 6;
                for (int j = 1; j <= rows; j++)
                {
                    for (int k = 1; k <= seatsInRow; k++)
                    {
                        var seat = new Seat
                        {
                            Auditorium = auditorium,
                            Number = k,
                            Row = j
                        };
                        Seats.Add(seat); 
                        auditorium.Seats.Add(seat);
                    }
                }
            }
            SaveChanges(); 
        }
    }
}
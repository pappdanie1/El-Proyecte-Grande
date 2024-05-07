using AspCinema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace El_Proyecte_Grande.Data;

public class ElProyecteGrandeContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Screening> Screenings { get; set; }
    public DbSet<Auditorium> Auditoriums { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Seat> Seats { get; set; }
    
    public DbSet<SeatReserved> SeatReserveds { get; set; }

    public ElProyecteGrandeContext(DbContextOptions<ElProyecteGrandeContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
        /*modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Customer)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.CustomerId)
            .HasForeignKey(r => r.ScreeningId);*/
        
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Customer)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Screening)
            .WithMany()
            .HasForeignKey(r => r.ScreeningId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SeatReserved>()
            .HasOne(sr => sr.Reservation)
            .WithMany(r => r.Seats)
            .HasForeignKey(sr => sr.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SeatReserved>()
            .HasOne(sr => sr.Screening)
            .WithMany() // Assuming no navigation property from Screening to SeatReserved
            .HasForeignKey(sr => sr.ScreeningId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SeatReserved>()
            .HasOne(sr => sr.Seat)
            .WithMany()
            .HasForeignKey(sr => sr.SeatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
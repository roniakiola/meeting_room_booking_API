using Microsoft.EntityFrameworkCore;
using MeetingRoomBookingAPI.Models;

namespace MeetingRoomBookingAPI.Data;

public class BookingDbContext : DbContext
{
  public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
  {
  }

  public DbSet<Room> Rooms => Set<Room>();
  public DbSet<Booking> Bookings => Set<Booking>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // Configure Room entity
    modelBuilder.Entity<Room>(entity =>
    {
      entity.HasKey(r => r.Id);
      entity.Property(r => r.Name).IsRequired().HasMaxLength(100);

      entity.HasMany(r => r.Bookings)
              .WithOne(b => b.Room)
              .HasForeignKey(b => b.RoomId)
              .OnDelete(DeleteBehavior.Cascade);
    });

    // Configure Booking entity
    modelBuilder.Entity<Booking>(entity =>
    {
      entity.HasKey(b => b.Id);
      entity.Property(b => b.RoomId).IsRequired();
      entity.Property(b => b.StartTime).IsRequired();
      entity.Property(b => b.EndTime).IsRequired();
    });

    // Seed 5 predefined rooms
    modelBuilder.Entity<Room>().HasData(
        new Room { Id = 1, Name = "Conference Room A" },
        new Room { Id = 2, Name = "Conference Room B" },
        new Room { Id = 3, Name = "Meeting Room C" },
        new Room { Id = 4, Name = "Board Room" },
        new Room { Id = 5, Name = "Training Room" }
    );
  }
}

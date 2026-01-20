using Microsoft.EntityFrameworkCore;
using MeetingRoomBookingAPI.Data;
using MeetingRoomBookingAPI.DTOs;
using MeetingRoomBookingAPI.Models;

namespace MeetingRoomBookingAPI.Services;

public class BookingService : IBookingService
{
  private readonly BookingDbContext _context;

  public BookingService(BookingDbContext context)
  {
    _context = context;
  }

  public async Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request)
  {
    // Validate: Start time must be before end time
    if (request.StartTime >= request.EndTime)
    {
      throw new InvalidOperationException("Start time must be before end time.");
    }

    // Validate: Bookings must not be in the past
    if (request.StartTime < DateTime.UtcNow)
    {
      throw new InvalidOperationException("Cannot create bookings in the past.");
    }

    // Validate: Room must exist
    var room = await _context.Rooms.FindAsync(request.RoomId);
    if (room == null)
    {
      throw new InvalidOperationException($"Room with ID {request.RoomId} does not exist.");
    }

    // Validate: Bookings must not overlap
    var hasOverlap = await _context.Bookings
        .Where(b => b.RoomId == request.RoomId)
        .AnyAsync(b =>
            // Check for overlap: existing booking overlaps if its start is before new end AND its end is after new start
            b.StartTime < request.EndTime && b.EndTime > request.StartTime
        );

    if (hasOverlap)
    {
      throw new InvalidOperationException("The requested time slot overlaps with an existing booking.");
    }

    // Create the booking
    var booking = new Booking
    {
      RoomId = request.RoomId,
      StartTime = request.StartTime,
      EndTime = request.EndTime
    };

    _context.Bookings.Add(booking);
    await _context.SaveChangesAsync();

    // Return the response
    return new BookingResponse
    {
      Id = booking.Id,
      RoomId = booking.RoomId,
      RoomName = room.Name,
      StartTime = booking.StartTime,
      EndTime = booking.EndTime
    };
  }

  public async Task<bool> CancelBookingAsync(int id)
  {
    var booking = await _context.Bookings.FindAsync(id);
    if (booking == null)
    {
      return false;
    }

    _context.Bookings.Remove(booking);
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<List<BookingResponse>> GetBookingsByRoomAsync(int roomId)
  {
    var bookings = await _context.Bookings
        .Include(b => b.Room)
        .Where(b => b.RoomId == roomId)
        .OrderBy(b => b.StartTime)
        .Select(b => new BookingResponse
        {
          Id = b.Id,
          RoomId = b.RoomId,
          RoomName = b.Room.Name,
          StartTime = b.StartTime,
          EndTime = b.EndTime
        })
        .ToListAsync();

    return bookings;
  }
}

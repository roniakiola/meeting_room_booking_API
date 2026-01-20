namespace MeetingRoomBookingAPI.Models;

public class Booking
{
  public int Id { get; set; }
  public int RoomId { get; set; }
  public DateTime StartTime { get; set; }
  public DateTime EndTime { get; set; }

  // Navigation property
  public Room Room { get; set; } = null!;
}

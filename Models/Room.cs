namespace MeetingRoomBookingAPI.Models;

public class Room
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;

  // Navigation property
  public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}

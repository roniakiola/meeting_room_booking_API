namespace MeetingRoomBookingAPI.DTOs;

public class BookingResponse
{
  public int Id { get; set; }
  public int RoomId { get; set; }
  public string RoomName { get; set; } = string.Empty;
  public DateTime StartTime { get; set; }
  public DateTime EndTime { get; set; }
}

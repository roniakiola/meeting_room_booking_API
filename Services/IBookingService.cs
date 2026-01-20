using MeetingRoomBookingAPI.DTOs;

namespace MeetingRoomBookingAPI.Services;

public interface IBookingService
{
  Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request);
  Task<bool> CancelBookingAsync(int id);
  Task<List<BookingResponse>> GetBookingsByRoomAsync(int roomId);
}
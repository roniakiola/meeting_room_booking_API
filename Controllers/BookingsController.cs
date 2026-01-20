using Microsoft.AspNetCore.Mvc;
using MeetingRoomBookingAPI.DTOs;
using MeetingRoomBookingAPI.Services;

namespace MeetingRoomBookingAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
  private readonly IBookingService _bookingService;

  public BookingsController(IBookingService bookingService)
  {
    _bookingService = bookingService;
  }

  /// <summary>
  /// Creates a new booking for a meeting room.
  /// </summary>
  /// <param name="request">The booking details</param>
  /// <returns>The created booking</returns>
  [HttpPost]
  [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
  public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
  {
    try
    {
      var booking = await _bookingService.CreateBookingAsync(request);
      return CreatedAtAction(nameof(GetBookings), new { roomId = booking.RoomId }, booking);
    }
    catch (ArgumentException ex)
    {
      return BadRequest(new ProblemDetails
      {
        Status = StatusCodes.Status400BadRequest,
        Title = "Invalid request",
        Detail = ex.Message
      });
    }
    catch (KeyNotFoundException ex)
    {
      return NotFound(new ProblemDetails
      {
        Status = StatusCodes.Status404NotFound,
        Title = "Room not found",
        Detail = ex.Message
      });
    }
    catch (InvalidOperationException ex)
    {
      return Conflict(new ProblemDetails
      {
        Status = StatusCodes.Status409Conflict,
        Title = "Booking conflict",
        Detail = ex.Message
      });
    }
  }

  /// <summary>
  /// Cancels (deletes) an existing booking.
  /// </summary>
  /// <param name="id">The booking ID</param>
  /// <returns>No content if successful</returns>
  [HttpDelete("{id:int}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> CancelBooking(int id)
  {
    var result = await _bookingService.CancelBookingAsync(id);
    if (!result)
    {
      return NotFound(new ProblemDetails
      {
        Status = StatusCodes.Status404NotFound,
        Title = "Booking not found",
        Detail = $"Booking with ID {id} does not exist."
      });
    }

    return NoContent();
  }

  /// <summary>
  /// Lists all bookings for a specific room.
  /// </summary>
  /// <param name="roomId">The room ID</param>
  /// <returns>A list of bookings</returns>
  [HttpGet]
  [ProducesResponseType(typeof(List<BookingResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetBookings([FromQuery] int roomId)
  {
    try
    {
      var bookings = await _bookingService.GetBookingsByRoomAsync(roomId);
      return Ok(bookings);
    }
    catch (KeyNotFoundException ex)
    {
      return NotFound(new ProblemDetails
      {
        Status = StatusCodes.Status404NotFound,
        Title = "Room not found",
        Detail = ex.Message
      });
    }
  }
}

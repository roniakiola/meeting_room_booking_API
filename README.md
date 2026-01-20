# Meeting Room Booking API

A simple ASP.NET Core Web API for booking meeting rooms.

## Prerequisites

- .NET 10.0 SDK

## Running the API

```bash
dotnet run
```

The API will start on `https://localhost` (check console output for the exact port).

## Available Rooms

The API comes pre-seeded with 5 meeting rooms (IDs 1-5):

| Room ID | Room Name          |
|---------|--------------------|
| 1       | Conference Room A  |
| 2       | Conference Room B  |
| 3       | Meeting Room C     |
| 4       | Board Room         |
| 5       | Training Room      |

## API Endpoints

### Create a Booking
`POST /api/bookings`

```json
{
  "roomId": 1,
  "startTime": "2026-01-20T09:00:00Z",
  "endTime": "2026-01-20T10:00:00Z"
}
```

### Cancel a Booking
`DELETE /api/bookings/{id}`

### List Bookings for a Room
`GET /api/bookings?roomId={roomId}`

## Business Rules

- Bookings cannot overlap for the same room (adjacent bookings are allowed)
- Bookings cannot be in the past
- Start time must be before end time

## Notes

- All timestamps use UTC format (ISO 8601)
- In-memory database (data resets on restart)

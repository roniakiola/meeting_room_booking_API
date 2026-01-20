# Meeting Room Booking API

A simple ASP.NET Core Web API for booking meeting rooms.

## Prerequisites

- .NET 10.0 SDK

## Setup and Running

1. **Clone or download the project**

2. **Run the API**
   ```bash
   dotnet run
   ```

3. **Access the API**
   - The API runs on: `http://localhost:5193`
   - Swagger UI: `http://localhost:5193/`

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

All endpoints are accessible at `http://localhost:5193/api/bookings`

### Create a Booking
`POST /api/bookings`

```json
{
  "roomId": 1,
  "startTime": "2026-01-20T09:00:00Z",
  "endTime": "2026-01-20T10:00:00Z"
}
```

**Example:**
```bash
curl -X POST http://localhost:5193/api/bookings \
  -H "Content-Type: application/json" \
  -d "{\"roomId\":1,\"startTime\":\"2026-01-20T09:00:00Z\",\"endTime\":\"2026-01-20T10:00:00Z\"}"
```

### Cancel a Booking
`DELETE /api/bookings/{id}`

**Example:**
```bash
curl -X DELETE http://localhost:5193/api/bookings/1
```

### List Bookings for a Room
`GET /api/bookings?roomId={roomId}`

**Example:**
```bash
curl http://localhost:5193/api/bookings?roomId=1
```

## Business Rules

- Bookings cannot overlap for the same room (adjacent bookings are allowed)
- Bookings cannot be in the past
- Start time must be before end time

## Notes

- All timestamps use UTC format (ISO 8601)
- In-memory database (data resets on restart)
- Swagger UI provides interactive API documentation at `http://localhost:5193/`

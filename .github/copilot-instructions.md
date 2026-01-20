# General instructions for Meeting Room Booking API

You are acting as a backend developer.

Goal of this project is to produce a simple ASP.NET Core Web API (C#) for booking meeting rooms.
The API is backend-only (no frontend).

## Functional requirements:

- Create a booking for a given room and time range
- Cancel (delete) a booking
- List all bookings for a given room

## Business rules:

- Bookings must not overlap for the same room
- Bookings must not be in the past
- Start time must be before end time

## Technical constraints:

- Use ASP.NET Core Web API with controllers (not minimal APIs)
  - Use .NET 10.0.2
- Use an in-memory data structure (no real database)
  - Use EF Core with InMemory provider
- No authentication or authorization
- Keep the solution simple and readable

## Design expectations:

- Controllers should be thin
- Business logic should live in a service class
- Use basic DTOs for requests and responses
- Use reasonable HTTP status codes

## Output:

- Provide the full code needed to run the API
- Briefly explain the project structure and key design choices

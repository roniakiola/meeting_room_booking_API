using Microsoft.EntityFrameworkCore;
using MeetingRoomBookingAPI.Data;
using MeetingRoomBookingAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure EF Core with InMemory database
builder.Services.AddDbContext<BookingDbContext>(options =>
    options.UseInMemoryDatabase("BookingDb"));

// Register the booking service
builder.Services.AddScoped<IBookingService, BookingService>();

// Add problem details for standardized error responses
builder.Services.AddProblemDetails();

var app = builder.Build();

// Seed the database with rooms
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookingDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

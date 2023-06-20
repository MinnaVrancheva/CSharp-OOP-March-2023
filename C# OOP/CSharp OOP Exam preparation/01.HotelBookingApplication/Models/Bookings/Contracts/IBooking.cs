using BookingApp.Models.Rooms.Contracts;

namespace BookingApp.Models.Bookings.Contracts;

public interface IBooking
{
    IRoom Room { get; }
    int ResidenceDuration { get; }
    int AdultsCount { get; }
    int ChildrenCount { get; }
    int BookingNumber { get; }

    public string BookingSummary();
}

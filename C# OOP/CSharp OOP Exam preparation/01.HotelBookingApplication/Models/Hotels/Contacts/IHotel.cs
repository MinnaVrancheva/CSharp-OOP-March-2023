using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories.Contracts;

namespace BookingApp.Models.Hotels.Contacts;

public interface IHotel
{
    string FullName { get; }
    int Category { get; }
    double Turnover { get; }

    public IRepository<IRoom> Rooms { get; set; }

    public IRepository<IBooking> Bookings { get; set; }
}

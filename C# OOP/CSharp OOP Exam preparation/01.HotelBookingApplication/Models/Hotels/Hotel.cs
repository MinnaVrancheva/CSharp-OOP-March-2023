using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;

namespace BookingApp.Models.Hotels;

public class Hotel : IHotel
{
    private string fullName;
    private int category;

    public Hotel(string fullName, int category)
    {
        this.fullName = fullName;
        this.category = category;
        this.Rooms = new RoomRepository();
        this.Bookings = new BookingRepository();
    }

    public string FullName
    {
        get => this.fullName;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(ExceptionMessages.HotelNameNullOrEmpty);
            }
            this.fullName = value;
        }
    }

    public int Category
    {
        get => this.category;
        private set
        {
            if (value < 1 || value > 5)
            {
                throw new ArgumentException(ExceptionMessages.InvalidCategory);
            }
            category = value;
        }
    }

    public double Turnover => Math.Round(Bookings.All().Sum(x => x.ResidenceDuration * x.Room.PricePerNight), 2);

    public IRepository<IRoom> Rooms { get; set; }
    public IRepository<IBooking> Bookings { get; set; }
}

using BookingApp.Core.Contracts;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Bookings;
using System.Text;

namespace BookingApp.Core;

public class Controller : IController
{
    private readonly IRepository<IHotel> hotels;
    public Controller()
    {
        this.hotels = new HotelRepository();
    }
    public string AddHotel(string hotelName, int category)
    {
        IHotel hotel;
        if (hotels.Select(hotelName) != null)
        {
            return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
        }
        hotel = new Hotel(hotelName, category);
        this.hotels.AddNew(hotel);
        string result = string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
        return result;
    }

    public string BookAvailableRoom(int adultsCount, int childrenCount, int duration, int category)
    {
        if (hotels.All().FirstOrDefault(x => x.Category == category) == default)
        {
            return String.Format(OutputMessages.CategoryInvalid, category);
        }

        var orderedHotels = this.hotels.All().Where(x => x.Category == category).OrderBy(x => x.FullName);//.ThenBy(x => x.Turnover);

        foreach (var hotel in orderedHotels)
        {
            var selectedRoom = hotel.Rooms.All()
                .Where(x => x.PricePerNight > 0)
                .Where(y => y.BedCapacity >= adultsCount + childrenCount)
                .OrderBy(z => z.BedCapacity).FirstOrDefault();

            if (selectedRoom != null)
            {
                int bookingNumber = this.hotels.All().Sum(x => x.Bookings.All().Count) + 1;
                IBooking booking = new Booking(selectedRoom, duration, adultsCount, childrenCount, bookingNumber);
                hotel.Bookings.AddNew(booking);
                return String.Format(OutputMessages.BookingSuccessful, bookingNumber, hotel.FullName);
            }
        }

        return string.Format(OutputMessages.RoomNotAppropriate);
    }

    public string HotelReport(string hotelName)
    {
        IHotel hotel = hotels.Select(hotelName);

        if (hotel == null)
        {
            return string.Format(OutputMessages.HotelNameInvalid, hotelName);
        }

        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Hotel name: {hotelName}");
        sb.AppendLine($"--{hotel.Category} star hotel");
        sb.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
        sb.AppendLine($"--Bookings:");
        sb.AppendLine();

        if (hotel.Bookings.All().Count == 0)
        {
            sb.AppendLine($"none");
        }
        else
        {
            foreach (var booking in hotel.Bookings.All())
            {
                sb.AppendLine($"{booking.BookingSummary()}");
                sb.AppendLine();
            }
        }
        return sb.ToString().TrimEnd();
    }

    public string SetRoomPrices(string hotelName, string roomType, double price)
    {
        if (hotels.Select(hotelName) == null)
        {
            return string.Format(OutputMessages.HotelNameInvalid, hotelName);
        }

        IHotel hotel = hotels.Select(hotelName);
        if (roomType != nameof(DoubleBed) && roomType != nameof(Studio) && roomType != nameof(Apartment))
        {
            throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
        }

        if (hotel.Rooms.Select(roomType) == null)
        {
            return string.Format(OutputMessages.RoomTypeNotCreated);
        }

        IRoom room = hotel.Rooms.Select(roomType);
        if (room.PricePerNight > 0)
        {
            throw new InvalidOperationException(ExceptionMessages.CannotResetInitialPrice);
        }

        room.SetPrice(price);

        return string.Format(OutputMessages.PriceSetSuccessfully, roomType, hotelName);
    }

    public string UploadRoomTypes(string hotelName, string roomType)
    {
        
        if(hotels.Select(hotelName) == null)
        {
            return string.Format(OutputMessages.HotelNameInvalid, hotelName);
        }

        IHotel hotel = hotels.Select(hotelName);
        if(hotel.Rooms.Select(roomType) != default)
        {
            return string.Format(OutputMessages.RoomTypeAlreadyCreated);
        }

        IRoom room;
        if (roomType == nameof(DoubleBed))
        {
            room = new DoubleBed();
        }
        else if (roomType == nameof(Studio))
        {
            room = new Studio();
        }
        else if (roomType == nameof(Apartment))
        {
            room = new Apartment();
        }
        else
        {
            throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
        }

        hotel.Rooms.AddNew(room);
        return string.Format(OutputMessages.RoomTypeAdded, roomType, hotelName);
    }
}

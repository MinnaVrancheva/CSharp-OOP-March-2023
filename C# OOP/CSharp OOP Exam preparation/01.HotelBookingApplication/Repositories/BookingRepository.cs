using BookingApp.Models.Bookings.Contracts;
using BookingApp.Repositories.Contracts;

namespace BookingApp.Repositories;

public class BookingRepository : IRepository<IBooking>
{
    private List<IBooking> bookings;

    public BookingRepository()
    {
        this.bookings = new List<IBooking>();
    }
    public void AddNew(IBooking model)
    {
        bookings.Add(model);
    }

    public IReadOnlyCollection<IBooking> All() => this.bookings.AsReadOnly();

    public IBooking Select(string criteria)
    {
        return this.bookings.FirstOrDefault(b => b.BookingNumber.ToString() == criteria);
    }
}

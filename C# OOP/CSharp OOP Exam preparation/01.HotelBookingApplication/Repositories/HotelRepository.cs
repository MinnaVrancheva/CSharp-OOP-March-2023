using BookingApp.Models.Hotels.Contacts;
using BookingApp.Repositories.Contracts;

namespace BookingApp.Repositories;

public class HotelRepository : IRepository<IHotel>
{
    private List<IHotel> hotels;

    public HotelRepository() => this.hotels = new List<IHotel>();
    public void AddNew(IHotel model)
    {
        this.hotels.Add(model);
    }

    public IReadOnlyCollection<IHotel> All() => this.hotels.AsReadOnly();
         

    public IHotel Select(string criteria)
    {
        return this.hotels.FirstOrDefault(x => x.FullName == criteria);
    }
}

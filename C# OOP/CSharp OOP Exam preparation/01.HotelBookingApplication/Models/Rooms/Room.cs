using BookingApp.Models.Rooms.Contracts;
using BookingApp.Utilities.Messages;

namespace BookingApp.Models.Rooms;

public abstract class Room : IRoom
{
    private int bedCapacity;
    private double pricePerNight = 0;
    protected Room(int bedCapacity)
    {
        this.bedCapacity = bedCapacity;
    }
    public int BedCapacity => this.bedCapacity;
    public double PricePerNight
    {
        get { return pricePerNight; }
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentException(ExceptionMessages.PricePerNightNegative);
            }

            this.pricePerNight = value;
        }
    }

    public void SetPrice(double price)
    {
        this.PricePerNight = price;
    }
}

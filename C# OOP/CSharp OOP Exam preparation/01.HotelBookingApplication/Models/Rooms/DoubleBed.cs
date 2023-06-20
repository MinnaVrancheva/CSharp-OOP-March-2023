namespace BookingApp.Models.Rooms;

public class DoubleBed : Room
{
    private const int DoubleRoomBedCapacity = 2;

    public DoubleBed()
        : base(DoubleRoomBedCapacity) { }
}

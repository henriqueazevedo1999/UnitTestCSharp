namespace TestNinja.Mocking;

public interface IBookingRepository
{
    IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
}

public class BookingRepository : IBookingRepository
{
    private readonly IUnitOfWork _unityOfWork;

    public BookingRepository(IUnitOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
    {
        var bookings = _unityOfWork.Query<Booking>().Where(b => b.Status != "Cancelled");

        if (excludedBookingId.HasValue)
            bookings = bookings.Where(b => b.Id != excludedBookingId.Value);

        return bookings;
    }
}

using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class BookingHelper_OverlappingBookingExistTests
{
    private Mock<IBookingRepository> _repository;
    private Booking _existingBooking;

    [SetUp]
    public void SetUp()
    {
        _existingBooking = new Booking
        {
            Id = 2,
            ArrivalDate = ArriveOn(2022, 03, 26),
            DepartureDate = DepartOn(2022, 03, 30),
            Reference = "Booking 2"
        };

        _repository = new Mock<IBookingRepository>();
        _repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>()
        {
            _existingBooking
        }.AsQueryable());
    }

    [Test]
    public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
    {
        // Act
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = _existingBooking.ArrivalDate.AddDays(-6),
            DepartureDate = _existingBooking.ArrivalDate.AddDays(-1),
            Reference = "Booking 1"
        }, _repository.Object);

        // Assert 
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnTheOverlappingBookingReference()
    {
        // Act
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = _existingBooking.ArrivalDate.AddDays(-6),
            DepartureDate = _existingBooking.DepartureDate.AddDays(-1),
            Reference = "Booking 1"
        }, _repository.Object);

        // Assert 
        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }

    [Test]
    public void BookingStartsInTheMiddleAndFinishesAfterAnExistingBooking_ReturnTheOverlappingBookingReference()
    {
        // Act
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = _existingBooking.ArrivalDate.AddDays(1),
            DepartureDate = _existingBooking.DepartureDate.AddDays(1),
            Reference = "Booking 1"
        }, _repository.Object);

        // Assert 
        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }

    [Test]
    public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
    {
        // Act
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = _existingBooking.DepartureDate.AddDays(1),
            DepartureDate = _existingBooking.DepartureDate.AddDays(6),
            Reference = "Booking 1"
        }, _repository.Object);

        // Assert 
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnTheOverlappingBookingReference()
    {
        // Act
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = _existingBooking.ArrivalDate.AddDays(-1),
            DepartureDate = _existingBooking.DepartureDate.AddDays(1),
            Reference = "Booking 1"
        }, _repository.Object);

        // Assert 
        Assert.That(result, Is.EqualTo(_existingBooking.Reference)) ;
    }

    [Test]
    public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnTheOverlappingBookingReference()
    {
        // Act
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = _existingBooking.ArrivalDate.AddDays(1),
            DepartureDate = _existingBooking.DepartureDate.AddDays(-1),
            Reference = "Booking 1"
        }, _repository.Object);

        // Assert 
        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }

    [Test]
    public void BookingsOverlapButNewBookingHasCancelledStatus_ReturnEmptyString()
    {
        // Act
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = _existingBooking.ArrivalDate.AddDays(-1),
            DepartureDate = _existingBooking.ArrivalDate.AddDays(1),
            Reference = "Booking 1",
            Status = "Cancelled"
        }, _repository.Object);

        // Assert 
        Assert.That(result, Is.Empty);
    }

    private DateTime ArriveOn(int year, int month, int day)
    {
        return new DateTime(year, month, day, 14, 0, 0);
    }

    private DateTime DepartOn(int year, int month, int day)
    {
        return new DateTime(year, month, day, 10, 0, 0);
    }
}

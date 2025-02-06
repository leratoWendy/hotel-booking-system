using HotelRoomBookingAPI.AuthModels;
using HotelRoomBookingAPI.Controllers;
using HotelRoomBookingAPI.Models;
using HotelRoomBookingAPI.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTApi.NunitTests
{
    [TestFixture]
    public class GuestBookingsTests
    {
        private object _hotelContext;
        private GuestsBookingsController _controllerUnderTest;
        private List<Booking> _bookingList;
        private List<GuestsBookingsVM> _bookingsList;
        private Booking _booking;
        private GuestsBookingsVM _bookings;

        private Mock<LP_HotelRoomBooking_EFDBContext> _contextMock;

        [SetUp]
        public void Initialiser()
        {
            _hotelContext = InMemoryContext.GeneratedGuests();
            _contextMock = new Mock<LP_HotelRoomBooking_EFDBContext>();
            _bookingList = new List<Booking>();
            _bookingsList = new List<GuestsBookingsVM>();
            _booking = new Booking();

            DateTime checkinDate = new DateTime(2024, 5, 20);
            DateTime checkoutDate = new DateTime(2024, 5, 23);
            _bookings = new GuestsBookingsVM()
            {
                BookingId = 1,
                GuestId = 1,
                RoomId = 1,
                CheckinDate = checkinDate,
                CheckoutDate = checkoutDate,
                TotalPrice = 4000.00
            };
        }

        [TearDown]
        public void CleanUpObject()
        {
            _hotelContext = null;
            _controllerUnderTest = null;
            _bookingList = null;
            _bookingsList = null;
            _booking = null;
            _bookings = null;
        }

        [Test]
        public async Task _01Test_GetAllGuests_ReturnsListWithValidCount0()
        {
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new GuestsBookingsController(_localHotelContext, null, null, null, null);

            var result = await _controllerUnderTest.Get();

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(0, ((IEnumerable<GuestsBookingsVM>)okResult.Value).Count());
        }

        [Test]
        public async Task _02Test_GetMyBookings_ReturnsNotFoundForInvalidUsername()
        {
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _controllerUnderTest = new GuestsBookingsController(_localHotelContext, null, null, null, null);

            var result = await _controllerUnderTest.GetMyBookings("InvalidUsername");

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task _03Test_PutBookings_ReturnsBadRequestForMismatchedIds()
        {
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _controllerUnderTest = new GuestsBookingsController(_localHotelContext, null, null, null, null);

            var result = await _controllerUnderTest.PutBookings(1, new GuestsBookingsVM { BookingId = 2 });

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("The Booking ID in the request body (2) does not match the ID in the URL (1).", badRequestResult.Value);
        }

        [Test]
        public async Task _04Test_GetMyBookings_ReturnsNotFoundForInvalidUsername()
        {
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _controllerUnderTest = new GuestsBookingsController(_localHotelContext, null, null, null, null);

            var result = await _controllerUnderTest.GetMyBookings("InvalidUsername");

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task _05Test_PutBookings_ReturnsBadRequestForMismatchedIds()
        {
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _controllerUnderTest = new GuestsBookingsController(_localHotelContext, null, null, null, null);

            var result = await _controllerUnderTest.PutBookings(1, new GuestsBookingsVM { BookingId = 2 });

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("The Booking ID in the request body (2) does not match the ID in the URL (1).", badRequestResult.Value);
        }

       
        
      
    }
}

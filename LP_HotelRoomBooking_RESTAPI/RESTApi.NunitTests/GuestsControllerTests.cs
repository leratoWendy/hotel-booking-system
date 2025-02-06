using HotelRoomBookingAPI.Controllers;
using HotelRoomBookingAPI.Models;
using HotelRoomBookingAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RESTApi.NunitTests
{
    [TestFixture]
    public class GuestsControllerTests
    {
        private object _hotelContext;
        private GuestsController _controllerUnderTest;
        private List<Guest> _guestList;
        private List<GuestVM> _guestsList;

        Guest _guest;
        GuestVM _guests;

        public object? ExpectedFirstName { get; private set; }
        public object? ExpectedNumberOfGuests { get; private set; }

        [SetUp]
        public void Initialiser()
        {
            _hotelContext = InMemoryContext.GeneratedGuests();
            _guestList = new List<Guest>();
            _guestsList = new List<GuestVM>();
            _guest = new Guest();
            _guests = new GuestVM()
            {
                GuestId = 1,
                FirstName = "Thando",
                LastName = "May",
                IdentityUserName = "ThandoMay",
                Email = "THando@gmail.com",
                Phone = "0795565856"
            };

        }

        [TearDown]
        public void CleanUpObject()
        {
            _hotelContext = null;
            _controllerUnderTest = null;
            _guestList = null;
            _guestsList = null;
            _guest = null;
            _guests = null;
        }

        [Test]
        public async Task _01Test_GetAllGuests_ReturnsListWithValidCount0()
        {
            // Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new GuestsController(_localHotelContext);

            

            //Assert
           
            Assert.AreEqual(0, _localHotelContext.Guests.Count());
        }

        [Test]
        public async Task _02Test_GetAllGuests_ReturnsaListwithValidCount1()
        {
            // Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new GuestsController(_localHotelContext);

            // Create a new GuestVM object to post
            var newGuest = new GuestVM()
            {
                GuestId = 1,
                FirstName = "John",
                LastName = "Lewis",
                IdentityUserName = "JohnLewis",
                Email = "John@gmail.com",
                Phone = "0794567890"
            };

            // Act
            await _controllerUnderTest.PostGuests(newGuest);

            // Assert
            Assert.NotNull(_localHotelContext.Guests);
            Assert.AreEqual(1, _localHotelContext.Guests.Count());
        }


        [Test]
        public async Task _03Test_PutGuests_UpdateExistingGuest_ReturnsNoContent()
        {
            // Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new GuestsController(_localHotelContext);

            // Create an existing guest in the database
            var existingGuest = new Guest()
            {
                GuestId = 1,
                FirstName = "Maria",
                LastName = "Jimmy",
                UserName = "MariaJimmy",
                Email = "Maria@gmail.com",
                Phone = "0791234567"
            };
            _localHotelContext.Guests.Add(existingGuest);
            await _localHotelContext.SaveChangesAsync();

            // Create an updated Guest object
            var updatedGuest = new Guest()
            {
                GuestId = 1,
                FirstName = "Kganya Updated",
                LastName = "Selala Updated",
                UserName = "KganyaSelalaUpdated",
                Email = "KganyaUpdated@gmail.com",
                Phone = "0799876543"
            };

            // Act
            var actionResult = await _controllerUnderTest.PutGuests(existingGuest.GuestId, updatedGuest);

            // Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<NoContentResult>(actionResult);
        }



        

       


       
       
        [Test]
        public async Task _07Test_GetGuest_InvalidId_ReturnsNotFound()
        {
            // Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new GuestsController(_localHotelContext);

            // Act
            var actionResult = await _controllerUnderTest.GetGuest(999);

            // Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<Guest>>(actionResult);
            var result = actionResult.Result as NotFoundResult;
            Assert.NotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        

        [Test]
        public async Task _09Test_GetGuestByName_InvalidName_ReturnsNotFound()
        {
            // Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new GuestsController(_localHotelContext);

            // Act
            var actionResult = await _controllerUnderTest.GetGuestByName("NonExistingName");

            // Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<Guest>>(actionResult);
            var result = actionResult.Result as NotFoundResult;
            Assert.NotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }


        

        [Test]
        public async Task _10Test_DeleteGuest_InvalidId_ReturnsNotFound()
        {
            // Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new GuestsController(_localHotelContext);

            // Act
            var actionResult = await _controllerUnderTest.DeleteGuests(999); // Invalid ID

            // Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<NotFoundResult>(actionResult.Result);
        }


        [Test]
        public async Task _23Test_GetGuests_ReturnsNotNull()
        {
            // Arrange
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new GuestsController(_localHotelContext);
          
            // Act
            var result = await _controllerUnderTest.GetGuests();

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task _24Test_GetGuests_ReturnsCorrectType()
        {
            // Arrange
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new GuestsController(_localHotelContext);

            // Act
            var result = await _controllerUnderTest.GetGuests();

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<GuestVM>>>(result);
        }

       

        [Test]
        public async Task _26Test_GetGuests_ReturnsGuestsWithCorrectData()
        {
            // Arrange
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new GuestsController(_localHotelContext);
            // Act
            var result = await _controllerUnderTest.GetGuests();
            var guestVMs = result.Value;

            // Assert
            foreach (var guest in guestVMs)
            {
                Assert.AreEqual(ExpectedFirstName, guest.FirstName); 
                                                                     
            }
        }

        [Test]
        public async Task _27Test_GetGuests_ReturnsEmptyListWhenNoGuestsExist()
        {
            // Arrange
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new GuestsController(_localHotelContext);
           

            // Act
            var result = await _controllerUnderTest.GetGuests();
            var guestVMs = result.Value;

            // Assert
            Assert.IsEmpty(guestVMs);
        }
    
        

        [Test]
        public async Task _26Test_GetGuests_ReturnsGuestsWithCorrectDataWHenCalled()
        {
            // Arrange
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _controllerUnderTest = new GuestsController(_localHotelContext);

            // Act
            var result = await _controllerUnderTest.GetGuests();
            var guestVMs = result.Value;

            // Assert
            foreach (var guest in guestVMs)
            {
                Assert.AreEqual(ExpectedFirstName, guest.FirstName); 
                                                                     
            }
        }

      

       

       






    }
}

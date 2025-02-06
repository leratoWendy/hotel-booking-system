using HotelRoomBookingAPI.Controllers;
using HotelRoomBookingAPI.Models;
using HotelRoomBookingAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.NunitTests
{
    [TestFixture]
    public class RoomsControllerTests
    {
        private Object _hotelContext;
        private RoomsController _controllerUnderTest;
        private List<Room> _roomList;
        private List<RoomsVM> _roomsList;

        Room _room;
        RoomsVM _rooms;

        [SetUp]
        public void Initialiser()
        {
            _hotelContext = InMemoryContext.GeneratedGuests();
            _roomList = new List<Room>();
            _room = new Room();
            _rooms = new RoomsVM()
            {
                RoomId = 1,
                RoomNumber = 2,
                RoomType = "STANDARD",
                NightPrice = 600.00
            };

        }



        [TearDown]
        public void CleanUpObject()
        {
            _hotelContext = null;
            _controllerUnderTest = null;
            _roomList = null;
            _room = null;
        }



        [Test]
        public async Task _01Test_GetAllRooms_ReturnsaListwithValidCount0()
        {
            //Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new RoomsController(_localHotelContext);


            //Act            
            //await _controllerUnderTest.PostCurrencies(new Currency());

            //Assert
            //Assert.NotNull(_localTradeContext.Currencies);
            Assert.AreEqual(0, _localHotelContext.Rooms.Count());

        }

        [Test]
        public async Task _02Test_GetAllRoom_ReturnsaListwithValidCount1()
        {
            //Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new RoomsController(_localHotelContext);


            //Act            
            await _controllerUnderTest.PostRooms(new RoomsVM());
          

            //Assert
            Assert.NotNull(_localHotelContext.Rooms);
            Assert.AreEqual(1, _localHotelContext.Rooms.Count());

        }


        [Test]
        public async Task _03Test_GetAllRoom_ReturnsaListwithValidCount()
        {
            //Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new RoomsController(_localHotelContext);


            //Act            
            await _controllerUnderTest.PostRooms(new RoomsVM());
            await _controllerUnderTest.PostRooms(new RoomsVM());
            await _controllerUnderTest.PostRooms(new RoomsVM());

            //Assert
            Assert.NotNull(_localHotelContext.Rooms);
            Assert.AreEqual(3, _localHotelContext.Rooms.Count());

        }


        [Test]
        public async Task _04Test_GetAllRoom_ReturnsWithCorrectType()
        {
            //Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new RoomsController(_localHotelContext);


            await _controllerUnderTest.PostRooms(_rooms);
            await _controllerUnderTest.PostRooms(new RoomsVM());

            //Act            
            var actionResult = await _controllerUnderTest.GetRooms(1);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<RoomsVM>>(actionResult);
        }



        [Test]
        public async Task _05Test_GetAllRoom_ReturnsWithCorrectTypeAndCount()
        {
            //Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new RoomsController(_localHotelContext);


            await _controllerUnderTest.PostRooms(new RoomsVM());
            await _controllerUnderTest.PostRooms(new RoomsVM());
            await _controllerUnderTest.PostRooms(new RoomsVM());
            await _controllerUnderTest.PostRooms(new RoomsVM());

            //Act            
            var actionResult = await _controllerUnderTest.GetRooms();

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<IEnumerable<RoomsVM>>>(actionResult);
            var value = actionResult.Value;
            Assert.AreEqual(4, value.Count());
        }





        [Test]
        public async Task _06Test_GetRoomById_ReturnsWithCorrectType()
        {
            //Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new RoomsController(_localHotelContext);

            await _controllerUnderTest.PostRooms(_rooms);
            await _controllerUnderTest.PostRooms(new RoomsVM());
           
          

            //Act            
            var actionResult = await _controllerUnderTest.GetRooms(1);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<RoomsVM>>(actionResult);
        }


        [Test]
        public async Task _07Test_PostRooms_AddedSuccessfullyAndShowsInContextCount()
        {
            //Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new RoomsController(_localHotelContext);

            

            //Act            
            var actionResult = await _controllerUnderTest.PostRooms(_rooms);

            //Assert
            Assert.NotNull(actionResult);
            Assert.AreEqual(1, _localHotelContext.Rooms.Count());

        }


        [Test]
        public async Task _08Test_PostRooms_AddedSuccessfullyAndReturnsWithCorrectType()
        {
            //Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new RoomsController(_localHotelContext);



            //Act            
            var actionResult = await _controllerUnderTest.PostRooms(_rooms);
            //Act            
           

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<RoomsVM>>(actionResult);
        }


        [Test]
        public async Task _09Test_PostRooms_AddedSuccessfullyReturnsWithCorrectTypeAndShowsInContextCount()
        {
            //Arrange 
             var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new RoomsController(_localHotelContext);


            

           
            RoomsVM _room2 = new RoomsVM()
            {
                RoomId = 5,
                RoomNumber = 6,
                RoomType = "STANDARD",
                NightPrice = 700.00
            };

            //Act            
            var actionResult = await _controllerUnderTest.PostRooms(_room2);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<RoomsVM>>(actionResult);
            Assert.AreEqual(1, _localHotelContext.Rooms.Count());
        }


        [Test]
        public async Task _10Test_DeleteRooms_DeleteSuccessfullyReturnsWithCorrectTypeAndShowsInContextCount()
        {
            // Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new RoomsController(_localHotelContext);

            // Create a new room
            RoomsVM _room = new RoomsVM()
            {
                RoomId = 5,
                RoomNumber = 6,
                RoomType = "STANDARD",
                NightPrice = 650.00
            };

            // Act            
            var actionResult = await _controllerUnderTest.PostRooms(_room);
            var addedRoom = (actionResult.Result as ObjectResult)?.Value as RoomsVM; 

            await _controllerUnderTest.DeleteRooms(addedRoom.RoomId); 

            // Assert
            Assert.AreEqual(1, _localHotelContext.Rooms.Count());
        }





        [Test]
        public async Task _11Test_DeleteRooms_AddMultipleDeleteOne_DeleteSuccessfullyReturnsWithCorrectTypeAndShowsInContextCount()
        {
            // Arrange
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new RoomsController(_localHotelContext);

         
            RoomsVM _room2 = new RoomsVM()
            {
                RoomId = 5,
                RoomNumber = 6,
                RoomType = "STANDARD",
                NightPrice = 700.00
            };

            RoomsVM _room3 = new RoomsVM()
            {
                RoomId = 6,
                RoomNumber = 7,
                RoomType = "DELUXE",
                NightPrice = 1500.00
            };


            // Act
            await _controllerUnderTest.PostRooms(_rooms);
            await _controllerUnderTest.PostRooms(_room2);
            await _controllerUnderTest.PostRooms(_room3);

            var actionResultDeleted = await _controllerUnderTest.DeleteRooms(_room2.RoomId);

            // Assert
            Assert.NotNull(actionResultDeleted);
            Assert.IsInstanceOf<ActionResult<Room>>(actionResultDeleted);

           
            var deletedRoom = await _localHotelContext.Rooms.FindAsync(_room2.RoomId);
            Assert.Null(deletedRoom); 

            // Check the count of rooms in the context after deletion
            Assert.AreEqual(3, _localHotelContext.Rooms.Count());
        }


        

        [Test]
        public async Task _13Test_PutRooms_ReturnsBadRequestForMismatchedId()
        {
            // Arrange 
            var _localHotelContext = (LP_HotelRoomBooking_EFDBContext)_hotelContext;
            _localHotelContext.Database.EnsureDeleted();
            _controllerUnderTest = new RoomsController(_localHotelContext);

            // Create a new room
            RoomsVM room = new RoomsVM()
            {
                RoomId = 1,
                RoomNumber = 101,
                RoomType = "DELUXE",
                NightPrice = 1500.00
            };

            // Add the room to the context
            await _controllerUnderTest.PostRooms(room);

            // Modify the room's details
            room.RoomId = 2; // Change the ID to simulate a mismatch

            // Act            
            var actionResult = await _controllerUnderTest.PutRooms(room.RoomId, _room);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
        }

       

    }
}

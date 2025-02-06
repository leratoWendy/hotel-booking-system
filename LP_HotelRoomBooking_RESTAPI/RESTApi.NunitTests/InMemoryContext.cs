using HotelRoomBookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace RESTApi.NunitTests
{
    public static class InMemoryContext
    {
        public static LP_HotelRoomBooking_EFDBContext GeneratedGuests()
        {
            var _contextOptions = new DbContextOptionsBuilder<LP_HotelRoomBooking_EFDBContext>()
                .UseInMemoryDatabase("ControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            return new LP_HotelRoomBooking_EFDBContext(_contextOptions);
        }
    }
}
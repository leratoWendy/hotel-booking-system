using HotelRoomBookingAPI.Models;
using HotelRoomBookingAPI.ViewModel;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelRoomBookingAPI.Repository
{
    /// <summary>
    /// Repository class for handling guest bookings.
    /// </summary>
    public class GuestsBookingsRepo
    {
        private readonly LP_HotelRoomBooking_EFDBContext _context;
        private static readonly ILog logger = LogManager.GetLogger("GuestsBookingsController");

        /// <summary>
        /// Initializes a new instance of the <see cref="GuestsBookingsRepo"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GuestsBookingsRepo(LP_HotelRoomBooking_EFDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all bookings for a specific guest.
        /// </summary>
        /// <param name="guestId">The guest ID.</param>
        /// <returns>A list of <see cref="GuestsBookingsVM"/>.</returns>
        public virtual List<GuestsBookingsVM> GetGuestsBookings(int guestId)
        {
            try
            {
                var bookings = _context.Bookings
                    .Where(b => b.GuestId == guestId)
                    .Select(b => new GuestsBookingsVM
                    {
                        BookingId = b.BookingId,
                        GuestId = b.GuestId,
                        RoomId = b.RoomId,
                        CheckinDate = b.CheckinDate,
                        CheckoutDate = b.CheckoutDate,
                        TotalPrice = b.TotalPrice
                    })
                    .ToList();

                if (bookings == null || bookings.Count == 0)
                {
                    return new List<GuestsBookingsVM>(); 
                }

                return bookings;
            }
            catch (Exception ex)
            {
              
                logger.Warn($"Error in GetGuestsBookings: {ex.Message}");
               
                throw; 
            }
        }

        /// <summary>
        /// Get details of a specific booking.
        /// </summary>
        /// <param name="bookingId">The booking ID.</param>
        /// <returns>A <see cref="GuestsBookingsVM"/> object.</returns>
        public virtual GuestsBookingsVM GetBookingDetails(int bookingId)
        {
            var booking = _context.Bookings
                .Where(b => b.BookingId == bookingId)
                .Select(b => new GuestsBookingsVM
                {
                    BookingId = b.BookingId,
                    GuestId = b.GuestId,
                    RoomId = b.RoomId,
                    CheckinDate = b.CheckinDate,
                    CheckoutDate = b.CheckoutDate,
                    TotalPrice = b.TotalPrice
                })
                .FirstOrDefault();

            return booking;
        }

        /// <summary>
        /// Get all bookings for a specific room.
        /// </summary>
        /// <param name="roomId">The room ID.</param>
        /// <returns>A list of <see cref="GuestsBookingsVM"/>.</returns>
        public virtual List<GuestsBookingsVM> GetBookingsByRoomId(int roomId)
        {
            var bookings = _context.Bookings
                .Where(b => b.RoomId == roomId)
                .Select(b => new GuestsBookingsVM
                {
                    BookingId = b.BookingId,
                    GuestId = b.GuestId,
                    RoomId = b.RoomId,
                    CheckinDate = b.CheckinDate,
                    CheckoutDate = b.CheckoutDate,
                    TotalPrice = b.TotalPrice
                })
                .ToList();

            return bookings;
        }
    }
}

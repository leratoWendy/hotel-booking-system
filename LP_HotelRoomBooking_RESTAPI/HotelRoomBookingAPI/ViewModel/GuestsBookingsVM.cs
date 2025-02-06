namespace HotelRoomBookingAPI.ViewModel
{
    /// <summary>
    /// Represents a view model for guest bookings.
    /// </summary>
    public class GuestsBookingsVM
    {
        /// <summary>
        /// Gets or sets the unique identifier for the booking.
        /// </summary>
        public int BookingId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the guest making the booking.
        /// </summary>
        public int GuestId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the room booked.
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Gets or sets the check-in date for the booking.
        /// </summary>
        public DateTime CheckinDate { get; set; }

        /// <summary>
        /// Gets or sets the check-out date for the booking.
        /// </summary>
        public DateTime CheckoutDate { get; set; }

        /// <summary>
        /// Gets or sets the total price for the booking.
        /// </summary>
        public double TotalPrice { get; set; }
    }
}

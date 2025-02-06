namespace HotelRoomBookingAPI.ViewModel
{
    /// <summary>
    /// Represents a view model for room information.
    /// </summary>
    public class RoomsVM
    {
        /// <summary>
        /// Gets or sets the unique identifier for the room.
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Gets or sets the room number.
        /// </summary>
        public int RoomNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        public string? RoomType { get; set; }

        /// <summary>
        /// Gets or sets the price per night for the room.
        /// </summary>
        public double NightPrice { get; set; }
    }
}

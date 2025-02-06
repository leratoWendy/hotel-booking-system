namespace HotelRoomBookingAPI.ViewModel
{
    /// <summary>
    /// Represents a view model for guest information.
    /// </summary>
    public class GuestVM
    {
        /// <summary>
        /// Gets or sets the unique identifier for the guest.
        /// </summary>
        public int GuestId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the guest.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the guest.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the identity user name associated with the guest.
        /// </summary>
        public string? IdentityUserName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the guest.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the guest.
        /// </summary>
        public string Phone { get; set; }
    }
}

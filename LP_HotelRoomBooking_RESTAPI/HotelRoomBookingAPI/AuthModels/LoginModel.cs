namespace HotelRoomBookingAPI.AuthModels
{
    /// <summary>
    /// Represents a model for user login information.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the user name for login.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password for login.
        /// </summary>
        public string Password { get; set; }
    }
}

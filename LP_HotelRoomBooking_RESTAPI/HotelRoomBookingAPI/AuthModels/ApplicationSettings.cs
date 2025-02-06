namespace HotelRoomBookingAPI.AuthModels
{
    /// <summary>
    /// Represents application settings related to JWT authentication.
    /// </summary>
    public class ApplicationSettings
    {
        /// <summary>
        /// Gets or sets the JWT secret key.
        /// </summary>
        public string? JWT_Secret { get; set; }

        /// <summary>
        /// Gets or sets the JWT cookie name.
        /// </summary>
        public string? JwtCookieName { get; set; }

        /// <summary>
        /// Gets or sets the signing key for JWT.
        /// </summary>
        public string? SigningKey { get; set; }

        /// <summary>
        /// Gets or sets the expiration time for JWT in minutes.
        /// </summary>
        public string? ExpiryInMinutes { get; set; }

        /// <summary>
        /// Gets or sets the site URL for JWT.
        /// </summary>
        public string? JWT_Site_URL { get; set; }

        /// <summary>
        /// Gets or sets the client URL for JWT.
        /// </summary>
        public string? Client_URL { get; set; }
    }
}

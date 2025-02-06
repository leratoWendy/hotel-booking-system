using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelRoomBookingAPI.AuthModels
{
    /// <summary>
    /// Represents a user in the application with extended properties.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        [Column(TypeName = "nvarchar(150)")]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        [Column(TypeName = "nvarchar(150)")]
        public string? LastName { get; set; }
    }
}

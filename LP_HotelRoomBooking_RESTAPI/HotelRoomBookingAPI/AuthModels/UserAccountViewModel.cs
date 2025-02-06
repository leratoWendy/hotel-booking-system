using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace HotelRoomBookingAPI.AuthModels
{
    /// <summary>
    /// Represents a view model for user account information.
    /// </summary>
    public class UserAccountViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccountViewModel"/> class.
        /// </summary>
        public UserAccountViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccountViewModel"/> class with specified user and roles.
        /// </summary>
        /// <param name="aus">The IdentityUser instance.</param>
        /// <param name="userRoles">The list of roles held by the user.</param>
        public UserAccountViewModel(IdentityUser aus, List<string> userRoles)
        {
            UserName = aus.UserName;
            Email = aus.Email;
            RolesHeld = userRoles;
        }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the list of roles held by the user.
        /// </summary>
        public List<string> RolesHeld { get; set; }
    }
}

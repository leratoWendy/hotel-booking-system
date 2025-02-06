using HotelRoomBookingAPI.AuthModels;
using Microsoft.AspNetCore.Identity;

namespace HotelRoomBookingAPI.Services
{
    /// <summary>
    /// Helper class for identity-related operations, such as checking user roles.
    /// </summary>
    public class IdentityHelper
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityHelper"/> class.
        /// </summary>
        /// <param name="userManager">The UserManager instance for managing users.</param>
        /// <param name="context">The AuthenticationContext instance for authentication operations.</param>
        /// <param name="roleManager">The RoleManager instance for managing roles.</param>
        public IdentityHelper(UserManager<ApplicationUser> userManager,
            AuthenticationContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Checks if a user is in a specific role.
        /// </summary>
        /// <param name="userId">The ID of the user to check.</param>
        /// <param name="roleName">The name of the role to check.</param>
        /// <returns>True if the user is in the specified role; otherwise, false.</returns>
        public async Task<bool> IsUserInRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            List<string> userRoles = new List<string>(await _userManager.GetRolesAsync(user));

            if (user != null)
            {
                return userRoles.Any(r => r.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a user is in a super user role.
        /// </summary>
        /// <param name="userId">The ID of the user to check.</param>
        /// <returns>True if the user is in a super user role; otherwise, false.</returns>
        public async Task<bool> IsSuperUserRole(string userId)
        {
            string superUserRole1 = "Administrator";
            string superUserRole2 = "Receptionist";

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                List<string> userRoles = new List<string>(await _userManager.GetRolesAsync(user));
                return (userRoles.Contains(superUserRole1) || userRoles.Contains(superUserRole2));
            }
            else
            {
                return false;
            }
        }
    }
}

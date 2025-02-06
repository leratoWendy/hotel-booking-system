using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomBookingAPI.AuthModels
{
    /// <summary>
    /// Represents the database context for authentication-related operations.
    /// </summary>
    public class AuthenticationContext : IdentityDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the context.</param>
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for ApplicationUsers.
        /// </summary>
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        /// <summary>
        /// Configures the model used by the entity framework core.
        /// </summary>
        /// <param name="builder">The model builder instance.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Calls base OnModelCreating from IdentityDbContext
            base.OnModelCreating(builder);

            // Generate GUIDs for role and user IDs
            var roleId_1 = Guid.NewGuid().ToString();
            var userId_1 = Guid.NewGuid().ToString();

            var roleId_2 = Guid.NewGuid().ToString();
            var userId_2 = Guid.NewGuid().ToString();

            var roleId_3 = Guid.NewGuid().ToString();
            var userId_3 = Guid.NewGuid().ToString();

            // Seeds the database with initial roles and users
            #region "Seed Data"
            builder.Entity<IdentityRole>().HasData(
                new { Id = roleId_1, Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
                new { Id = roleId_2, Name = "Receptionist", NormalizedName = "RECEPTIONIST" },
                new { Id = roleId_3, Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            // Create Administrator user
            var AdminUser = new ApplicationUser
            {
                Id = userId_1,
                Email = "Admin1@gmail.com",
                EmailConfirmed = true,
                FirstName = "Peter",
                LastName = "Cox",
                UserName = "PeterCoxAdmin",
                NormalizedUserName = "PETERCOXADMIN"
            };

            // Set user password
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            AdminUser.PasswordHash = ph.HashPassword(AdminUser, "Peter*@2024");

            // Seed user
            builder.Entity<ApplicationUser>().HasData(AdminUser);

            // Set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_1,
                UserId = userId_1
            });

            // Create Receptionist user
            var ReceptionistUser = new ApplicationUser
            {
                Id = userId_2,
                Email = "Receptionist@gmail.com",
                EmailConfirmed = true,
                FirstName = "Maria",
                LastName = "Bell",
                UserName = "MariaBellRecept",
                NormalizedUserName = "MARIABELLRECEPT"
            };

            // Set user password
            PasswordHasher<ApplicationUser> cstph = new PasswordHasher<ApplicationUser>();
            ReceptionistUser.PasswordHash = cstph.HashPassword(ReceptionistUser, "MariaBel*@2024");

            // Seed user
            builder.Entity<ApplicationUser>().HasData(ReceptionistUser);

            // Set user role to receptionist
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_2,
                UserId = userId_2
            });

            // Create Customer user
            var CustomerUser = new ApplicationUser
            {
                Id = userId_3,
                Email = "John@gmail.com",
                EmailConfirmed = true,
                FirstName = "John",
                LastName = "Smith",
                UserName = "JohnSmith",
                NormalizedUserName = "JOHNSMITH"
            };

            // Set user password
            PasswordHasher<ApplicationUser> lgcph = new PasswordHasher<ApplicationUser>();
            CustomerUser.PasswordHash = lgcph.HashPassword(CustomerUser, "John*@2024");

            // Seed user
            builder.Entity<ApplicationUser>().HasData(CustomerUser);

            // Set user role to customer
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId_3,
                UserId = userId_3
            });
            #endregion
        }
    }
}

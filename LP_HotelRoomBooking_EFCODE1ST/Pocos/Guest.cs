using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pocos
{
    [Table("guests")]
    public class Guest
    {
        [Key]
        [Column("guest_id")]
        public int GuestId { get; set; }

        [Column("first_name")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Column("last_name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Column("user_name")]
        [Required(ErrorMessage = "username is required")]
        public string Username { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Column("phone")]
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }

        // One guest can have many bookings
        public virtual ICollection<Booking> Bookings { get; set; }

        // One guest can make multiple payments
        public virtual ICollection<Payment> Payments { get; set; }
    }
}

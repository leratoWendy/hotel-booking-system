using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using System.Diagnostics;

namespace Pocos
{

    [Table("bookings")]
    public class Booking
    {
        [Key]
        [Column("booking_id")]
        public int BookingId { get; set; }

        [Column("guest_id")]
        [Required]
        public int GuestId { get; set; }
        public virtual Guest Guest { get; set; }

        [Column("room_id")]
        [Required]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        [Column("checkin_date")]
        public DateTime CheckInDate { get; set; }

        [Column("checkout_date")]
        public DateTime CheckOutDate { get; set; }

        [Column("total_price")]
        public double TotalPrice { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}

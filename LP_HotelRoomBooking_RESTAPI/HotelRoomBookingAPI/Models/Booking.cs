using System;
using System.Collections.Generic;

namespace HotelRoomBookingAPI.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Payments = new HashSet<Payment>();
        }

        public int BookingId { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public double TotalPrice { get; set; }

        public virtual Guest Guest { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
    }
}

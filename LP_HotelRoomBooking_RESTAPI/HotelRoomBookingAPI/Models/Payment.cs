﻿using System;
using System.Collections.Generic;

namespace HotelRoomBookingAPI.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int GuestId { get; set; }
        public int BookingId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }

        public virtual Booking Booking { get; set; } = null!;
        public virtual Guest Guest { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos
{

    [Table("payments")]
    public class Payment
    {

        [Key, Column("payment_id"),]
        public int PaymentId { get; set; }


        //each payment is made by one guest

        [Column("guest_id")]
        [Required]
        public int GuestId { get; set; }
        public virtual Guest Guest { get; set; }



        //One payment corresponds to one booking
        [Column("booking_id")]
        [Required]
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }


        [Column("payment_date"),]
        public DateTime PaymentDate { get; set; }

        [Column("amount"),]
        public double Amount { get; set; }
    }
}

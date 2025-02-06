using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Pocos
{

    [Table("rooms")]
    public class Room
    {
        [Key, Column("room_id"),]
        public int RoomId { get; set; }

        [Column("room_number"),]
        public int RoomNumber { get; set; }

        [Column("room_type"),]
        public string RoomType { get; set; }

        [Column("night_price"),]
        public double NightPrice { get; set; }


        //One room can be booked multiple times
        public virtual ICollection<Booking> Bookings { get; set; }

    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos
{
    [Table("booking_status")]

    public class BookingStatus
    {
        [Key, Column("status_id"),]
        public int StatusId { get; set; }

        [Column("status_name"),]
        public string StatusName { get; set; }
    }
}

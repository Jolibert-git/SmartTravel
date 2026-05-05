using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core;

namespace Travel.Domain.Entities
{
    [Table("seat_class")]
    public class SeatClass: HasId
    {
        

        [Required]
        [MaxLength(30)]
        [Column("class_name")]
        public string ClassName { get; set; } = string.Empty;

        // Navigation

        public ICollection<FlightSeat> FlightSeats { get; set; } = new List<FlightSeat>();
    }
}

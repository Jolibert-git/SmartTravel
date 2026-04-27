using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("hotel")]
    public class Hotel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("id_destination")]
        public long IdDestination { get; set; }

        [Required]
        [MaxLength(45)]
        [Column("hotel_name")]
        public string HotelName { get; set; } = string.Empty;

        [Column("stars")]
        public int? Stars { get; set; }

        [MaxLength(60)]
        [Column("email")]
        public string? Email { get; set; }

        // Navigation
        public Destination Destination { get; set; } = null!;
        public ICollection<PhoneHotel> PhoneHotels { get; set; } = new List<PhoneHotel>();
        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        // => Para el frontend: estrellas como texto (★★★★☆)
        [NotMapped]
        public string StarDisplay => Stars.HasValue
            ? new string('★', Stars.Value) + new string('☆', 5 - Stars.Value)
            : "Sin clasificar";
    }
}

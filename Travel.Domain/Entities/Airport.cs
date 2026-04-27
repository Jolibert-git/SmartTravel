using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("airport")]
    public class Airport
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("id_destination")]
        public long IdDestination { get; set; }

        [Required]
        [MaxLength(45)]
        [Column("airport_name")]
        public string AirportName { get; set; } = string.Empty;

        [Required]
        [MaxLength(5)]
        [Column("code_iata")]
        public string CodeIata { get; set; } = string.Empty;

        // Navigation
        public Destination Destination { get; set; } = null!;
        public ICollection<PhoneAirport> PhoneAirports { get; set; } = new List<PhoneAirport>();
        public ICollection<Flight> DepartureFlights { get; set; } = new List<Flight>();
        public ICollection<Flight> ArrivalFlights { get; set; } = new List<Flight>();

        // => Para el frontend: nombre con código IATA
        [NotMapped]
        public string DisplayName => $"{AirportName} ({CodeIata})";
    }
}

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
    [Table("destination")]
    public class Destination: HasId
    {
        

        [Column("id_country")]
        public long IdCountry { get; set; }

        [Required]
        [MaxLength(45)]
        [Column("city")]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(45)]
        [Column("street")]
        public string Street { get; set; } = string.Empty;

        // Navigation
        [ForeignKey("IdCountry")]
        public Country Country { get; set; } = null!;
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
        public ICollection<Airport> Airports { get; set; } = new List<Airport>();

        // => Para el frontend: dirección completa
        [NotMapped]
        public string FullAddress => $"{Street}, {City}, {Country?.CountryName}";
    }
}

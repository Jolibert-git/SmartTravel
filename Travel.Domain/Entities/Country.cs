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
    [Table("country")]
    public class Country : HasId
    {
        

        [Required]
        [MaxLength(60)]
        [Column("country_name")]
        public string CountryName { get; set; } = string.Empty;

        [Required]
        [StringLength(2)]
        [Column("iso_code")]
        public string IsoCode { get; set; } = string.Empty;

        // Navigation
        public ICollection<Destination> Destinations { get; set; } = new List<Destination>();
        public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();

        // => Para el frontend: URL de la bandera via iso_code
        [NotMapped]
        public string FlagUrl => $"https://flagcdn.com/w40/{IsoCode.ToLower()}.png";
    }
}

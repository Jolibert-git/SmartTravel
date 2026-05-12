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
    [Table("passenger")]
    public class Passenger: HasId
    {
        

        [Required]
        [MaxLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("document_number")]
        public string DocumentNumber { get; set; } = string.Empty;

        [Column("id_document_type")]
        public long IdDocumentType { get; set; }

        [Column("id_country")]
        public long IdCountry { get; set; }

        // Navigation
        [ForeignKey("IdDocumentType")]
        public DocumentType DocumentType { get; set; } = null!;
        [ForeignKey("IdCountry")]
        public Country Country { get; set; } = null!;
        public ICollection<ReservationPassenger> ReservationPassengers { get; set; } = new List<ReservationPassenger>();
        public ICollection<FlightSeatReservation> FlightSeatReservations { get; set; } = new List<FlightSeatReservation>();

        // => Para el frontend: nombre completo
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        // => Para el frontend: edad calculada
        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - BirthDate.Year;
                if (BirthDate.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        // => Para el frontend: si es menor de edad
        [NotMapped]
        public bool IsMinor => Age < 18;
    }
}

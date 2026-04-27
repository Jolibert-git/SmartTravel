using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("systems_user")]
    public class SystemsUser
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        [Column("phone_number")]
        public string? PhoneNumber { get; set; }

        [Required]
        [Column("password_hash")]
        public string PasswordHash { get; set; } = string.Empty;

        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        // Navigation
        public ICollection<RolUser> RolUsers { get; set; } = new List<RolUser>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        // => Para el frontend: nombre completo
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        // => Para el frontend: iniciales del avatar
        [NotMapped]
        public string Initials =>
            $"{(FirstName.Length > 0 ? FirstName[0] : ' ')}{(LastName.Length > 0 ? LastName[0] : ' ')}".ToUpper();

    }
}

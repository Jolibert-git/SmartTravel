using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Entities
{
    [Table("promotion")]
    public class Promotion
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [MaxLength(60)]
        [Column("promotion_name")]
        public string PromotionName { get; set; } = string.Empty;

        [Column("id_discount_type")]
        public long IdDiscountType { get; set; }

        [Required]
        [Column("discount_value")]
        public decimal DiscountValue { get; set; }

        [Required]
        [Column("date_from")]
        public DateTime DateFrom { get; set; }

        [Required]
        [Column("date_to")]
        public DateTime DateTo { get; set; }

        [Column("min_persons")]
        public int MinPersons { get; set; } = 1;

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        // Navigation
        public DiscountType DiscountType { get; set; } = null!;
        public ICollection<PromotionDetail> PromotionDetails { get; set; } = new List<PromotionDetail>();
        public ICollection<ReservationPromotion> ReservationPromotions { get; set; } = new List<ReservationPromotion>();

        // => Para el frontend: si la promoción está vigente hoy
        [NotMapped]
        public bool IsCurrentlyValid =>
            IsActive && DateTime.Now >= DateFrom && DateTime.Now <= DateTo;

        // => Para el frontend: descuento formateado
        [NotMapped]
        public string DiscountDisplay => $"{DiscountValue:N2}";

        // => Para el frontend: días restantes de la promo
        [NotMapped]
        public int DaysRemaining => IsCurrentlyValid ? (DateTo - DateTime.Now).Days : 0;
    }
}

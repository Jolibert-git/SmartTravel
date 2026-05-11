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
    [Table("room")]
    public class Room: HasId
    {
        

        [Column("id_hotel")]
        public long IdHotel { get; set; }

        [Column("id_type_room")]
        public long IdTypeRoom { get; set; }

        [Column("id_service")]
        public long IdService { get; set; }

        // Navigation
        //[ForeignKey("IdHotel")]
        [ForeignKey(nameof(IdHotel))]
        public Hotel Hotel { get; set; } = null!;
        [ForeignKey("IdTypeRoom")]
        public TypeRoom TypeRoom { get; set; } = null!;
        [ForeignKey("IdService")]
        public OfferedService OfferedService { get; set; } = null!;
        public ICollection<DetailReservation> DetailReservations { get; set; } = new List<DetailReservation>();

        // => Para el frontend: etiqueta completa de la habitación
        [NotMapped]
        public string RoomLabel => $"{TypeRoom?.TypeDescription} – {Hotel?.HotelName}";
    }
}

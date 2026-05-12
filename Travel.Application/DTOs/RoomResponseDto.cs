using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class RoomResponseDto
    {
        public long Id { get; set; }
        public string RoomLabel { get; set; } = string.Empty;
        public TypeRoomDto TypeRoom { get; set; } = null!;
        public HotelResponseDto Hotel { get; set; } = null!;
        public ServiceResponseDto Service { get; set; } = null!;
    }
}

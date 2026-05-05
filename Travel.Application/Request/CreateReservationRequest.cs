using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class CreateReservationRequest
    {
        public long? IdPackage { get; set; }
        public List<ReservationDetailRequest> Details { get; set; } = new();
        public List<long> PassengerIds { get; set; } = new();
        public List<long>? PromotionIds { get; set; }
    }
}

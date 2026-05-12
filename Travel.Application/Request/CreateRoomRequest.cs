using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Request
{
    public class CreateRoomRequest
    {
        public long IdHotel { get; set; }
        public long IdTypeRoom { get; set; }
        public long IdService { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.DTOs
{
    public class PromotionDetailDto
    {
        public long Id { get; set; }
        public string AppliesTo { get; set; } = string.Empty;
        public long? IdService { get; set; }
        public long? IdPackage { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class TouristClubJoinRequestDto
    {
        public long TouristId { get; set; }
        public long ClubId { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}

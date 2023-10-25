using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubJoinRequestByTouristDto
    {
        public long Id { get; set; }
        public long ClubId { get; set; }
        public string ClubName { get; set; }
        public DateTime RequestedAt { get; set; }
        public string Status { get; set; }
    }
}

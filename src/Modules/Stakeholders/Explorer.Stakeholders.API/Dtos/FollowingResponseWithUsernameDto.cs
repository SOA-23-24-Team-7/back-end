using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class FollowingResponseWithUsernameDto
    {
        public long Id { get; set; }
        public long FollowingId { get; set; }
        public string FollowingUsername { get; set; }
    }
}

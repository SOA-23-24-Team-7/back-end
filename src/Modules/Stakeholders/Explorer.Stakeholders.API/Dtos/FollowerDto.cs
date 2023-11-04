using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class FollowerDto
    {
        public long Id {  get; set; }
        public long UserId { get; set; }
        public long FollowedById { get; set; }
        public string FollowedByUserName { get; set; }
    }
}

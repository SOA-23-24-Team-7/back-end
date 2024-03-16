using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class VoteCreateDto
    {
        public long UserId { get; set; }
        public long BlogId { get; set; }
        public string VoteType { get; set; }
    }
}

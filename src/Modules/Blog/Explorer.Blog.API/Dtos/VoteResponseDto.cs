using Explorer.Blog.API.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public enum VoteType { DOWNVOTE, UPVOTE }
    public class VoteResponseDto
    {
        public long UserId { get; set; }
        public long BlogId { get; set; }
        public VoteType VoteType { get; set; }
    }
}

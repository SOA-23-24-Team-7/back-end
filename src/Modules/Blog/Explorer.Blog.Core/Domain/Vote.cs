using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public enum VoteType { DOWNVOTE, UPVOTE }
    public class Vote : Entity
    {
        public long UserId { get; init; }
        public long BlogId { get; init; }
        public Blog? Blog { get; init; }
        public VoteType VoteType { get; private set; }

        public Vote(long userId, long blogId, VoteType voteType)
        {
            UserId = userId;
            BlogId = blogId;
            VoteType = voteType;
        }

        public void SetToVoteType(VoteType voteType)
        {
            this.VoteType = voteType;
        }
    }
}

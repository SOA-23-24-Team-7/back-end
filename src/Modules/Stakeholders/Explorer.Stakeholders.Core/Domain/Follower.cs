using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Follower : Entity
    {
        public long UserId { get; private set; }
        
        public User User { get; private set; }
        public long FollowedById { get; private set; }
        public User FollowedBy { get; private set; }

        public Follower(long userId, long followedById) 
        {
            UserId = userId;
            FollowedById = followedById;
        }
    }
}

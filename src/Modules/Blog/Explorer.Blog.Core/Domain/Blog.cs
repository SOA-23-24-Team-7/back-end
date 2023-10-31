using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain
{
    public enum BlogStatus { Draft, Published, Closed };
    public class Blog : Entity
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public DateTime Date { get; init; }
        public List<string>? Pictures { get; init; }
        public BlogStatus Status { get; init; }

        [InverseProperty("Blog")]
        public ICollection<Comment> Comments { get; } = new List<Comment>();

        [InverseProperty("Blog")]
        public ICollection<Vote> Votes { get; } = new List<Vote>();

        public long VoteCount => Votes.Sum(x =>
        {
            if (x.VoteType == VoteType.UPVOTE) return 1;
            return -1;
        });

        public long UpvoteCount => Votes.Sum(x => x.VoteType == VoteType.UPVOTE ? 1 : 0);
        public long DownvoteCount => Votes.Sum(x => x.VoteType == VoteType.DOWNVOTE ? 1 : 0);

        public Blog(string title, string description, DateTime date, List<string>? pictures, BlogStatus status)
        {

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title ne sme biti prazan ili null.\n");
            }


            Title = title;
            Description = description;
            Date = date;
            Pictures = pictures;
            Status = status;
        }
    }
}

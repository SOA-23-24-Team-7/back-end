using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Blog.Core.Domain
{
    public enum BlogStatus { Draft, Published, Closed, Active, Famous };
    public class Blog : Entity
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public DateTime Date { get; init; }
        public BlogStatus Status { get; private set; }
        public int AuthorId{ get; init; }

        [InverseProperty("Blog")]
        public ICollection<Comment> Comments { get; } = new List<Comment>();

        public ICollection<Vote> Votes { get; } = new List<Vote>();

        public long VoteCount => Votes.Sum(x =>
        {
            if (x.VoteType == VoteType.UPVOTE) return 1;
            return -1;
        });

        public long UpvoteCount => Votes.Sum(x => x.VoteType == VoteType.UPVOTE ? 1 : 0);
        public long DownvoteCount => Votes.Sum(x => x.VoteType == VoteType.DOWNVOTE ? 1 : 0);

        public Blog(string title, string description, DateTime date, BlogStatus status, int authorId)
        {

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title ne sme biti prazan ili null.\n");
            }


            Title = title;
            Description = description;
            Date = date;
            Status = status;
            AuthorId = authorId;
        }

        public void SetVote(long userId, VoteType voteType)
        {
            Vote existingVote = Votes.FirstOrDefault(r => r.UserId == userId);
            if (existingVote != null)
            {
                Votes.Remove(existingVote);

                if (existingVote.VoteType == voteType)
                {
                    UpdateBlogStatus();
                    return;
                }
            }

            Votes.Add(new Vote(userId, voteType));
            UpdateBlogStatus();

        }

        private void UpdateBlogStatus()
        {
            if (VoteCount < -10)
            {
                Status = BlogStatus.Closed;
            }
            else if (VoteCount > 100 || Comments.Count > 10)
            {
                Status = BlogStatus.Active;
            }
            else if (VoteCount > 500 && Comments.Count > 30)
            {
                Status = BlogStatus.Famous;
            }
        }
    }
}

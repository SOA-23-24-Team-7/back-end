using System.ComponentModel.DataAnnotations.Schema;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain
{
    public enum BlogStatus { Draft, Published, Closed, Active, Famous };
    public enum BlogVisibilityPolicy { Public, Private };
    public class Blog : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; init; }
        public BlogStatus Status { get; private set; }
        public int AuthorId { get; init; }
        public long? ClubId { get; init; }

        [InverseProperty("Blog")]
        public ICollection<Comment> Comments { get; } = new List<Comment>();

        public ICollection<Vote> Votes { get; } = new List<Vote>();
        public BlogVisibilityPolicy VisibilityPolicy { get; private set; }

        public long VoteCount => Votes.Sum(x =>
        {
            if (x.VoteType == VoteType.UPVOTE) return 1;
            return -1;
        });

        public long UpvoteCount => Votes.Sum(x => x.VoteType == VoteType.UPVOTE ? 1 : 0);
        public long DownvoteCount => Votes.Sum(x => x.VoteType == VoteType.DOWNVOTE ? 1 : 0);

        public Blog(string title, string description, DateTime date, BlogStatus status, int authorId, BlogVisibilityPolicy visibilityPolicy)
        {

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title ne sme biti prazan ili null.\n");
            }

            AuthorId = authorId;
            Title = title;
            Description = description;
            Date = DateTime.UtcNow.Date;
            Status = status;
            VisibilityPolicy = visibilityPolicy;
            ClubId = null;
        }

        public void UpdateBlog(string title, string description, BlogStatus status)
        {

            Title = title;
            Description = description;
            Status = status;
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
            if (VoteCount < -2)
            {
                Status = BlogStatus.Closed;
            }
            else if (VoteCount >= 3 && Comments.Count >= 3)
            {
                Status = BlogStatus.Famous;
            }
            else if (VoteCount >= 2 && Comments.Count >= 2)
            {
                Status = BlogStatus.Active;
            }
            else
            {
                Status = BlogStatus.Published;
            }
        }
    }
}

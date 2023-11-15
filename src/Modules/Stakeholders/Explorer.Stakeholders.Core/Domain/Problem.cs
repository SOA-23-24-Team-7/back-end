using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Problems;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Problem : Entity
    {
        public string Category { get; init; }
        public string Priority { get; init; }
        public string Description { get; init; }
        public DateTime ReportedTime { get; init; }
        public long TouristId { get; init; }
        public User Tourist { get; init; }
        public int TourId { get; init; }
        public ProblemAnswer Answer { get; set; }
        public ICollection<ProblemComment> Comments { get; set; } = new List<ProblemComment>();
        public bool IsResolved { get; set; } = false;
        public bool IsAnswered { get; set; } = false;
        public DateTime Deadline { get; private set; } = DateTime.MaxValue;

        public Problem(string category, string priority, string description, DateTime reportedTime, long touristId, int tourId)
        {
            Validate(category, priority, description);
            Category = category;
            Priority = priority;
            Description = description;
            ReportedTime = reportedTime;
            TouristId = touristId;
            TourId = tourId;
        }

        public void ResolveProblem()
        {
            IsResolved = true;
        }

        public void CreateAnswer(string text, long authorId)
        {
            Answer = new ProblemAnswer(authorId, text);
            IsAnswered = true;
        }

        public ProblemComment CreateComment(string text, User commenter, long commenterId)
        {
            var comment = new ProblemComment(commenterId, commenter, text);
            Comments.Add(comment);
            return comment;
        }

        public void SetDeadline(DateTime deadline)
        {
            Deadline = deadline;
        }

        public void Validate(string category, string priority, string description)
        {
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Invalid Category.");
            if (string.IsNullOrWhiteSpace(priority)) throw new ArgumentException("Invalid Priority.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
        }
    }
}

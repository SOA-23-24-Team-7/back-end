using Explorer.BuildingBlocks.Core.Domain;
using System.Xml.Linq;

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

        //public void UpdateAnswerId(long answerId)
        //{
        //    AnswerId = answerId;
        //}

        public void UpdateIsAnswered(bool isAnswered)
        {
            IsAnswered = isAnswered;
        }

        public void UpdateDeadline(DateTime deadline)
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

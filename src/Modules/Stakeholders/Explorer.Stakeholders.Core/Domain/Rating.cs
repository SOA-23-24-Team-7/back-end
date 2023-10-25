using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Rating : Entity
    {
        public int Grade { get; private set; }
        public string? Comment { get; private set; }
        public DateTime DateTime { get; private set; }
        public long UserId { get; private set; }
        public User? User { get; init; }
        public Rating(int grade, string? comment, DateTime dateTime, long userId)
        {
            Grade = grade;
            Comment = comment;
            DateTime = dateTime;
            UserId = userId;
            Validate();
        }
        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (Grade < 1 || Grade > 5) throw new ArgumentException("Invalid Grade");
        }
    }
}

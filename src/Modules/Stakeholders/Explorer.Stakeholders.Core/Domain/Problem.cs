using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Problem : Entity
    {
        public string Category { get; init; }
        public string Priority { get; init; }
        public string Description { get; init; }
        public DateTime DateTime { get; init; }
        public long TouristId { get; init; }
        public int TourId { get; init; }

        public Problem(string category, string priority, string description, DateTime dateTime, long touristId, int tourId)
        {
            Validate(category, priority, description);
            Category = category;
            Priority = priority;
            Description = description;
            DateTime = dateTime;
            TouristId = touristId;
            TourId = tourId;
        }
        public void Validate(string category, string priority, string description)
        {
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentException("Invalid Category.");
            if (string.IsNullOrWhiteSpace(priority)) throw new ArgumentException("Invalid Priority.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
        }
    }
}

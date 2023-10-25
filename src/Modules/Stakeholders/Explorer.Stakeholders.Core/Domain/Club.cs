using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Club : Entity
    {
        public long OwnerId { get; init; }
        public User? Owner { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Image { get; init; }
        public Club(long ownerId, string name, string description, string image)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
            if (string.IsNullOrWhiteSpace(image)) throw new ArgumentException("Invalid Image.");
            Name = name;
            Description = description;
            Image = image;
            OwnerId = ownerId;
        }
    }
}

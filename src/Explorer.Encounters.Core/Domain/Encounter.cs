using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounter.Core.Domain
{
    public class Encounter : Entity
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string Location { get; init; }
        public long Xp { get; init; }
        public EncounterStatus Status { get; init; }
        public EncounterType Type { get; init; }
        public Encounter(string title, string description, string location, long xp, EncounterStatus status, EncounterType type)
        {
            Title = title;
            Description = description;
            Location = location;
            Xp = xp;
            Status = status;
            Type = type;
            Validate();
        }
        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title)) throw new ArgumentException("Invalid Title");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
            if (string.IsNullOrWhiteSpace(Location)) throw new ArgumentException("Invalid Location");
            if (Xp < 0) throw new ArgumentException("XP cannot be negative");
        }
    }
    public enum EncounterStatus { Active, Draft, Archieved };
    public enum EncounterType { Social, Location, Misc };

}

using System.Text.Json.Serialization;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Encounters.Core.Domain.Encounter
{
    public class EncounterInstance : ValueObject
    {
        public long UserId { get; set; }
        public EncounterInstanceStatus Status { get; private set; }
        public DateTime? CompletionTime { get; private set; }

        [JsonConstructor]
        public EncounterInstance(long userId)
        {
            UserId = userId;
            Status = EncounterInstanceStatus.Active;
        }

        public void Complete()
        {
            Status = EncounterInstanceStatus.Completed;
            CompletionTime = DateTime.UtcNow;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
        }

    }
    public enum EncounterInstanceStatus { Active, Completed }
}

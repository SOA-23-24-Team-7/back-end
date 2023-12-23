using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;


namespace Explorer.Encounters.Core.Domain.Encounter
{
    public class EncounterInstance : ValueObject
    {
        public long UserId { get; set; }
        public EncounterInstanceStatus Status { get; private set; }
        public DateTime? CompletionTime { get; private set; }

        public EncounterInstance(long userId)
        {
            UserId = userId;
            Status = EncounterInstanceStatus.Active;
        }

        [JsonConstructor]
        public EncounterInstance(long userId, EncounterInstanceStatus status, DateTime? completionTime) : this(userId)
        {
            Status = status;
            CompletionTime = completionTime;
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

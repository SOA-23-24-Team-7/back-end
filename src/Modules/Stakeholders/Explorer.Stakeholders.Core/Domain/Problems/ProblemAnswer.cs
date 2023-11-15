using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Stakeholders.Core.Domain.Problems
{
    public class ProblemAnswer : ValueObject
    {
        public long AuthorId { get; private set; }
        public string Answer { get; private set; }

        [JsonConstructor]
        public ProblemAnswer(long authorId, string answer)
        {
            AuthorId = authorId;
            Answer = answer;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Answer)) throw new ArgumentException("Invalid Answer");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AuthorId;
        }
    }
}

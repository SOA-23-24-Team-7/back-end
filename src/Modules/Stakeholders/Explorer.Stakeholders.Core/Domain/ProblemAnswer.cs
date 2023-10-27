using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ProblemAnswer : Entity
    {
        public long ProblemId { get; private set; }
        public string Answer { get; private set; }
        public ICollection<ProblemComment> Comments { get; private set; }

        public ProblemAnswer(long problemId, string answer)
        {
            ProblemId = problemId;
            Answer = answer;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Answer)) throw new ArgumentException("Invalid Answer");
        }
    }
}

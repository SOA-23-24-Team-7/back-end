using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ProblemAnswer : Entity
    {
        public long AuthorId { get; private set; }
        public long ProblemId { get; private set; }
        public string Answer { get; private set; }

        public ProblemAnswer(long authorId, long problemId, string answer)
        {
            AuthorId = authorId;
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

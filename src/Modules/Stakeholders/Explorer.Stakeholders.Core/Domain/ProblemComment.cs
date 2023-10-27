using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ProblemComment : Entity
    {
        public long ProblemAnswerId { get; private set; }
        public string Text { get; private set; }

        public ProblemComment(long problemAnswerId, string text)
        {
            ProblemAnswerId = problemAnswerId;
            Text = text;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Text)) throw new ArgumentException("Invalid Text");
        }
    }
}

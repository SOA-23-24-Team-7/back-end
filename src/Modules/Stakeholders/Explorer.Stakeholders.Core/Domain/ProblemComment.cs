using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ProblemComment : Entity
    {
        public long CommenterId { get; private set; }
        public User Commenter { get; private set; }
        public long ProblemAnswerId { get; private set; }
        public string Text { get; private set; }

        public ProblemComment(long commenterId, long problemAnswerId, string text)
        {
            CommenterId = commenterId;
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

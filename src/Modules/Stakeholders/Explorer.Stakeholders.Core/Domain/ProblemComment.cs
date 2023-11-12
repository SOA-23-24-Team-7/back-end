using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ProblemComment : ValueObject
    {
        public long CommenterId { get; private set; }
        public User Commenter { get; private set; }
        public string Text { get; private set; }

        [JsonConstructor]
        public ProblemComment(long commenterId, string text)
        {
            CommenterId = commenterId;
            Text = text;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Text)) throw new ArgumentException("Invalid Text");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CommenterId;
            yield return Text;
        }
    }
}

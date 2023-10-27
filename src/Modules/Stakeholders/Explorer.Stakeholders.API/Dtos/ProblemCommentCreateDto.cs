namespace Explorer.Stakeholders.API.Dtos
{
    public class ProblemCommentCreateDto
    {
        public long ProblemAnswerId { get; set; }
        public string Text { get; set; }
    }
}


namespace Explorer.Stakeholders.API.Dtos
{
    public class ProblemAnswerCreateDto
    {
        public long AuthorId { get; set; }
        public long ProblemId { get; set; }
        public string Answer { get; set; }
    }
}

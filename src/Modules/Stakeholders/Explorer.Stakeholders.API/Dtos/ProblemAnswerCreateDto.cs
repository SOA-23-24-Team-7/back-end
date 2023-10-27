
namespace Explorer.Stakeholders.API.Dtos
{
    public class ProblemAnswerCreateDto
    {
        public long ProblemId { get; set; }
        public string Answer { get; set; }
        public ICollection<ProblemAnswerResponseDto> Comments { get; set; }
    }
}

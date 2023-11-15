namespace Explorer.Stakeholders.API.Dtos
{
    public class ProblemResolvingNotificationCreateDto
    {
        public long ProblemId { get; set; }
        public long ReceiverId { get; set; }
        public long SenderId { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }

    }
}

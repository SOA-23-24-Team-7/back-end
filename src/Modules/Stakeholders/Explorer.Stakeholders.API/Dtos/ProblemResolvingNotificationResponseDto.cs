namespace Explorer.Stakeholders.API.Dtos
{
    public class ProblemResolvingNotificationResponseDto
    {
        public long Id { get; set; }
        public long ProblemId { get; set; }
        public long ReceiverId { get; set; }
        public long SenderId { get; set; }
        public string Header { get; set; }
        public UserResponseDto Sender { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public bool HasSeen { get; set; }
    }
}

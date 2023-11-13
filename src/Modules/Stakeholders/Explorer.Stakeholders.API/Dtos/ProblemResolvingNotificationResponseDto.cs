namespace Explorer.Stakeholders.API.Dtos
{
    public class ProblemResolvingNotificationResponseDto
    {
        public long ProblemId { get; set; }
        public long ReceiverId { get; set; }
        public long SenderId { get; set; }
        public UserResponseDto Sender { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public bool HasSeen { get; set; }
    }
}

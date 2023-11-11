namespace Explorer.Stakeholders.API.Dtos
{
    public class PersonResponseDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public UserResponseDto User { get; set; }
        public string? Bio { get; set; }
        public string? Motto { get; set; }
    }
}

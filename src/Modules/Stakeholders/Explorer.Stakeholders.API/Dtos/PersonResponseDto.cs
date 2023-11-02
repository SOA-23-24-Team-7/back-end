namespace Explorer.Stakeholders.API.Dtos
{
    public class PersonResponseDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public string? Motto { get; set; }
        public bool IsActive { get; set; }
    }
}

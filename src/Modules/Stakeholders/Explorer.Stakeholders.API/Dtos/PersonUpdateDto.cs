namespace Explorer.Stakeholders.API.Dtos
{
    public class PersonUpdateDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public string? Motto { get; set; }
    }
}

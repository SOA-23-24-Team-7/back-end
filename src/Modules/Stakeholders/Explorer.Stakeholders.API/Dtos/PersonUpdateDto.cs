namespace Explorer.Stakeholders.API.Dtos
{
    public class PersonUpdateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public string? Moto { get; set; }
    }
}

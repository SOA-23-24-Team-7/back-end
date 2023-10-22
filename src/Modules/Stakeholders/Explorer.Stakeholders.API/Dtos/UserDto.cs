namespace Explorer.Stakeholders.API.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get;  set; }
        public int Role { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}

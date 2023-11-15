namespace Explorer.Stakeholders.API.Dtos
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }
        public int Role { get; set; }
        public bool IsActive { get; set; }
        public ICollection<FollowerCreateDto> Followers { get; set; }
    }
}

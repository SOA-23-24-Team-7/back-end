using Explorer.Stakeholders.API.Dtos;

namespace Explorer.Encounters.API.Dtos
{
    public class TouristProgressResponseDto
    {
        public long UserId { get; set; }
        public UserResponseDto User { get; set; }
        public int Xp { get; set; }
        public int Level { get; set; }
    }
}

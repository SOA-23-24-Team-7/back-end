namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubCreateDto
    {
        public long OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}

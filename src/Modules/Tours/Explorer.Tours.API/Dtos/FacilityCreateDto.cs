namespace Explorer.Tours.API.Dtos
{
    public class FacilityCreateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int AuthorId { get; set; }
        public FacilityCategory Category { get; set; }
    }
}

namespace Explorer.Tours.API.Dtos
{
    public class FacilityUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public int AuthorId { get; set; }
        public FacilityCategory Category { get; set; }
        public double Longitude { get; init; }
        public double Latitude { get; init; }
    }
}

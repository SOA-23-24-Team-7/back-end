namespace Explorer.Tours.API.Dtos
{
    public class TourUpdateDto
    {
        public int Id { get; set; }
        public long AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public List<string> Tags { get; set; }
        public TourStatus Status { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; }
        public double Distance { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ArchiveDate { get; set; }
        public List<TourDurationUpdateDto> Durations { get; set; }
        public TourCategory Category { get; set; }
    }
}

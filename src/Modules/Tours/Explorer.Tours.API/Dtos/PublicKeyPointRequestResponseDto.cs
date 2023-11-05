using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.API.Dtos
{
    public class PublicKeyPointRequestResponseDto
    {
        public long Id { get; set; }
        public long KeyPointId { get; set; }
        public PublicStatus Status { get; set; }
        public string? Comment { get; set; }
        public DateTime Created { get; set; }

    }
}

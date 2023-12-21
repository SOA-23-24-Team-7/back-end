using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.API.Dtos;

namespace Explorer.Tours.API.Dtos
{
    public class PublicKeyPointRequestResponseDto
    {
        public long Id { get; set; }
        public long KeyPointId { get; set; }
        public PublicStatus Status { get; set; }
        public string? Comment { get; set; }
        public DateTime Created { get; set; }
        public string AuthorName { get; set; }
        public string? KeyPointName { get; set; }

        public UserResponseDto Author { get; set; }

    }
}

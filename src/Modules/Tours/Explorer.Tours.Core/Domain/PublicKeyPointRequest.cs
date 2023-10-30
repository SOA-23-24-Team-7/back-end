using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class PublicKeyPointRequest : Entity
    {
        public long KeyPointId { get; init; }
        public KeyPoint? KeyPoint { get; init; } 
        public PublicStatus Status { get; init; }
        public string? Comment { get; init; }

        public PublicKeyPointRequest(long keyPointId,PublicStatus status) 
        { 
            KeyPointId = keyPointId;
            Status = status;
            //Comment = comment;
        }  
    }
}
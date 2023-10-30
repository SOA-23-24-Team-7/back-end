using Explorer.BuildingBlocks.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Explorer.Tours.Core.Domain
{
    public class PublicKeyPointRequest : Entity
    {
        public long KeyPointId { get; init; }
        //[InverseProperty("PublicKeyPointRequest")]
        //public KeyPoint? KeyPoint { get; init; }
        public PublicStatus Status { get; init; }
        public string? Comment { get; init; }

        public PublicKeyPointRequest(long keyPointId,PublicStatus status) 
        { 
            KeyPointId = keyPointId;
            Status = status;
            //Comment = comment;
        }
        public PublicKeyPointRequest(long keyPointId, PublicStatus status,string? comment)
        {
            KeyPointId = keyPointId;
            Status = status;
            Comment = comment;
        }
    }
}
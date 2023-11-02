using Explorer.BuildingBlocks.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Explorer.Tours.Core.Domain
{
    public class PublicKeyPointRequest : Entity
    {
        public long KeyPointId { get; init; }
        //public KeyPoint? KeyPoint { get; init; }
        public long AuthorId { get; init; }
        public PublicStatus Status { get; set; }
        public string? Comment { get; set; }

        public PublicKeyPointRequest(long keyPointId,PublicStatus status,long authorId) 
        { 
            KeyPointId = keyPointId;
            Status = status;
            AuthorId = authorId;
            //Comment = comment;

        }  

        
        public PublicKeyPointRequest(long keyPointId, PublicStatus status,string? comment,long authorId)
        {
            KeyPointId = keyPointId;
            Status = status;
            Comment = comment;
            AuthorId = authorId;
        }
    }
}
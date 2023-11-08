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
        public DateTime Created { get; set; }
        public string AuthorName { get; init; }

        public PublicKeyPointRequest(long keyPointId,PublicStatus status,long authorId, DateTime created, string authorName) 
        { 
            KeyPointId = keyPointId;
            Status = status;
            AuthorId = authorId;
            Created = created;
            //Comment = comment;
            AuthorName = authorName;

        }  

        
        public PublicKeyPointRequest(long keyPointId, PublicStatus status,string? comment,long authorId, DateTime created, string authorname)
        {
            KeyPointId = keyPointId;
            Status = status;
            Comment = comment;
            AuthorId = authorId;
            Created = created;
            AuthorName = authorname;
        }
    }
}
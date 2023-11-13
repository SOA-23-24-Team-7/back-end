using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class PublicFacilityRequest : Entity
    {
        public long FacilityId { get; init; }
        public PublicStatus Status { get; set; }
        public string? Comment { get; set; }
        public long AuthorId { get; init; }
        public DateTime Created { get; set; }
        public string AuthorName { get; init; }

        public PublicFacilityRequest(long facilityId, PublicStatus status,long authorId, DateTime created, string authorName)
        {
            FacilityId = facilityId;
            Status = status;
            AuthorId = authorId;
            Created = created;
            AuthorName = authorName;
            //Comment = comment;

        }


        public PublicFacilityRequest(long facilityId, PublicStatus status, string? comment,long authorId, DateTime created, string authorname)
        {
            FacilityId = facilityId;
            Status = status;
            Comment = comment;
            AuthorId = authorId;
            Created = created;
            AuthorName = authorname;
        }
    }
}

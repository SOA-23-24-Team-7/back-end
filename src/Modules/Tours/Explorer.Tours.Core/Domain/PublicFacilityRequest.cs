using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class PublicFacilityRequest : Entity
    {
        public long FacilityId { get; init; }
        public PublicStatus Status { get; init; }
        public string? Comment { get; init; }

        public PublicFacilityRequest(long facilityId, PublicStatus status)
        {
            FacilityId = facilityId;
            Status = status;
            //Comment = comment;

        }


        public PublicFacilityRequest(long facilityId, PublicStatus status, string? comment)
        {
            FacilityId = facilityId;
            Status = status;
            Comment = comment;
        }
    }
}

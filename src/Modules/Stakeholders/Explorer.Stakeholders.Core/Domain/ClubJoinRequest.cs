﻿using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ClubJoinRequest : Entity
    {
        public long TouristId { get; init; }
        public long ClubId { get; init; }
        public DateTime RequestedAt { get; init; }
        public TouristClubJoinRequestStatus Status { get; init; }

        public ClubJoinRequest(long touristId, long clubId, DateTime requestedAt, TouristClubJoinRequestStatus status)
        {
            TouristId = touristId;
            ClubId = clubId;
            RequestedAt = requestedAt;
            Status = status;
            Validate();
        }

        private void Validate()
        {
            if (TouristId == 0) throw new ArgumentException("Invalid TouristId");
            if (ClubId == 0) throw new ArgumentException("Invalid ClubId");
            if (RequestedAt > DateTime.Now) throw new ArgumentException("Invalid RequestedAt");
        }
    }
}

public enum TouristClubJoinRequestStatus
{
    Pending,
    Accepted,
    Rejected,
    Cancelled
}

using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubJoinRequestService : IClubJoinRequestService
    {
        public ClubJoinRequestService()
        {
        }

        public Result<ClubJoinRequestDto> Send(ClubJoinRequestDto request)
        {
            try
            {
                var joinRequest = new ClubJoinRequest(request.TouristId, request.ClubId, request.RequestedAt, ClubJoinRequestStatus.Pending);
                return request;
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}

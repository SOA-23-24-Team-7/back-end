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
using AutoMapper;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubJoinRequestService : IClubJoinRequestService
    {
        private readonly IMapper _mapper;
        public ClubJoinRequestService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Result<ClubJoinRequestDto> Send(ClubJoinRequestDto request)
        {
            try
            {
                var joinRequest = _mapper.Map<ClubJoinRequest>(request);
                return request;
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}

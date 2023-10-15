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
        private readonly ICrudRepository<ClubJoinRequest> _requestRepository;
        public ClubJoinRequestService(IMapper mapper, ICrudRepository<ClubJoinRequest> requestRepository)
        {
            _mapper = mapper;
            _requestRepository = requestRepository;
        }

        public Result<ClubJoinRequestDto> Send(ClubJoinRequestDto request)
        {
            try
            {
                var joinRequest = _mapper.Map<ClubJoinRequest>(request);
                _requestRepository.Create(joinRequest);
                return request;
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}

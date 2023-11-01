using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class PublicFacilityRequestService : CrudService<PublicFacilityRequestResponseDto, PublicFacilityRequest>, IPublicFacilityRequestService
    {
        private readonly ICrudRepository<PublicFacilityRequest> _repository;
        public PublicFacilityRequestService(ICrudRepository<PublicFacilityRequest> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }
        public Result Reject(long requestId,string comment)
        {
            try
            {
                var request = _repository.Get(requestId);

                if (!isPending(request))
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);
                }

                request.Status = PublicStatus.Rejected;
                request.Comment = comment;

                _repository.Update(request);

                return Result.Ok().WithSuccess("Request rejected successfully.");
            }
            catch (KeyNotFoundException)
            {
                return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
            }
        }

        public Result Accept(long requestId)
        {
            try
            {
                var request = _repository.Get(requestId);

                if (!isPending(request))
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError(FailureCode.InvalidArgument);
                }

                request.Status = PublicStatus.Accepted;

                _repository.Update(request);

                return Result.Ok().WithSuccess("Request accepted successfully.");
            }
            catch (KeyNotFoundException)
            {
                return Result.Fail(FailureCode.NotFound).WithError(FailureCode.NotFound);
            }
        }

        private bool isPending(PublicFacilityRequest request)
        {
            return request.Status == PublicStatus.Pending;
        }
    }
}

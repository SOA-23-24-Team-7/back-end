using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ISubscriberService
    {
        Result<PagedResult<SubscriberResponseDto>> GetPaged(int page, int pageSize);
        Result<SubscriberResponseDto> Create<SubscriberCreateDto>(SubscriberCreateDto subscriber);
        Result<SubscriberResponseDto> SaveOrUpdate(SubscriberCreateDto subscriber);
        Result<SubscriberResponseDto> GetByUserId(int userId);

        public List<SubscriberResponseDto> GetAll();
    }
}

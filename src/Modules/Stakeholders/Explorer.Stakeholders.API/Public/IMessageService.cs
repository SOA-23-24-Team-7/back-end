using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IMessageService
    {
        Result<PagedResult<MessageResponseDto>> GetPaged(int page, int pageSize);
        Result<MessageResponseDto> Create<MessageDto>(MessageDto message);
        Result<MessageResponseDto> Get(long id);
        Result<MessageResponseDto> UpdateMessage(MessageDto message);
        public Result Delete(long id);

    }
}

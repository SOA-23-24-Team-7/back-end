using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class MessageService : CrudService<MessageResponseDto, Message>, IMessageService

    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(ICrudRepository<Message> crudRepository, IMessageRepository messageRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _messageRepository = messageRepository;
        }

        public Result<PagedResult<MessageResponseDto>> GetMessages(int page, int pageSize, long userId)
        {
            var result = _messageRepository.GetMessagesPagedById(page, pageSize, userId);
            return MapToDto<MessageResponseDto>(result);
        }


        public Result<MessageResponseDto> UpdateMessage(MessageDto message)
        {
            throw new NotImplementedException();
        }

      

        
    }
}

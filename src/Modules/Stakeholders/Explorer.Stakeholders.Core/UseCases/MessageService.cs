using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class MessageService : CrudService<MessageResponseDto, Message>, IMessageService

    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(ICrudRepository<Message> crudRepository, IMessageRepository messageRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _messageRepository = messageRepository;
        }

        public Result<PagedResult<MessageResponseWithUsernamesDto>> GetMessages(int page, int pageSize, long userId)
        {
            var result = _messageRepository.GetMessagesPagedById(page, pageSize, userId);
            return MapToDto<MessageResponseWithUsernamesDto>(result);
        }


        public Result<MessageResponseDto> UpdateMessage(MessageCreateDto message)
        {
            throw new NotImplementedException();
        }


        public Result<MessageResponseDto> UpdateStatus(MessageUpdateDto messageUpdateDto)
        {
            try
            {
                var message = CrudRepository.Get(messageUpdateDto.Id);
                message.UpdateStatus();
                CrudRepository.Update(message);

                return MapToDto<MessageResponseDto>(message);

            }
            catch
            {

                return Result.Fail(FailureCode.NotFound);
            }
        }

    }
}

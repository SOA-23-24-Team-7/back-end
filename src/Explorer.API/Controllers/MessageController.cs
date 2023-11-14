using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers
{
    [Route("api/messages")]
    public class MessageController: BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;


        public MessageController(IUserService userService, IMessageService messageService)
        {
            _userService = userService;
            _messageService= messageService;
        }

        [HttpGet("{recieverId:long}")]
        public ActionResult<PagedResult<MessageResponseWithUsernamesDto>> GetByRecieverId([FromQuery] int page, [FromQuery] int pageSize, long recieverId)
        {
            var result = _messageService.GetMessages(page, pageSize, recieverId);
            return CreateResponse(result);
        }

        [HttpPut("update-status")]
        public ActionResult<MessageResponseDto> UpdateStatus([FromBody] MessageUpdateDto message)
        {

            var result = _messageService.UpdateStatus(message);
            return CreateResponse(result);
        }


        [HttpPost("create")]
        public ActionResult<MessageResponseDto> Create([FromBody] MessageCreateDto message)
        {           
            var result = _messageService.Create(message);
            return CreateResponse(result);
        }

       
      
        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            var result = _messageService.Delete(id);
            return CreateResponse(result);
        }

        [HttpPut("{id:long}")]
        public ActionResult<MessageResponseDto> Update([FromBody] MessageUpdateDto message)
        {
            var result = _messageService.Update(message);
            return CreateResponse(result);
        }
    }
}

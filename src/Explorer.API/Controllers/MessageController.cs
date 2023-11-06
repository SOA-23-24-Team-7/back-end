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

         [HttpGet]
         public ActionResult<PagedResult<MessageResponseDto>> GetAllMessages([FromQuery] int page, [FromQuery] int pageSize)
         {
            
             var result = _messageService.GetPaged(page, pageSize);
             return CreateResponse(result);
         }
        [HttpPost("create")]
        public ActionResult<MessageResponseDto> Create([FromBody] MessageDto message)
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


        [HttpGet("{messageId:long}")]
        public ActionResult<PagedResult<MessageResponseDto>> Get(long messageId)
        {
            var result = _messageService.Get(messageId);
            return CreateResponse(result);
        }
    }
}

using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/users")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IWalletService _walletService;

        public UserController(IUserService userService, IWalletService walletService)
        {
            _userService = userService;
            _walletService = walletService;
        }

        [HttpGet("disable/{userId:long}")]
        public ActionResult<UserResponseDto> DisableAccount(long userId)
        {
            var result = _userService.DisableAccount(userId);
            if (result.IsSuccess && !result.IsFailed)
            {
                _walletService.DeleteForTourist(userId);
            }
            return CreateResponse(result);
        }
    }
}

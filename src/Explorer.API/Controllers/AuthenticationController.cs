using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Explorer.API.Controllers;

[Route("api/users")]
public class AuthenticationController : BaseApiController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IWalletService _walletService;

    public AuthenticationController(IAuthenticationService authenticationService, IWalletService walletService)
    {
        _authenticationService = authenticationService;
        _walletService = walletService;
    }

    [HttpPost]
    public ActionResult<RegistrationConfirmationTokenDto> RegisterTourist([FromBody] AccountRegistrationDto account)
    {
        var result = _authenticationService.RegisterTourist(account);
        if(result.IsSuccess && !result.IsFailed)
        {
            _walletService.Create(new Payments.API.Dtos.WalletCreateDto(result.Value.Id));
        }
        return CreateResponse(result);
    }

    [HttpPost("login")]
    public ActionResult<AuthenticationTokensDto> Login([FromBody] CredentialsDto credentials)
    {
        var result = _authenticationService.Login(credentials);
        return CreateResponse(result);
    }

    [HttpPost("reset-password")]
    public ActionResult<ResetPasswordTokenDto> GenerateResetPasswordLink([FromBody] ResetPasswordEmailDto resetPasswordEmail)
    {
        var result = _authenticationService.GenerateResetPasswordToken(resetPasswordEmail);
        return CreateResponse(result);
    }

    [HttpGet("confirm-registration")]
    public ActionResult ConfirmPassword([FromQuery] string confirm_registration_token)
    {
       
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtToken = tokenHandler.ReadJwtToken(confirm_registration_token);

        var usermname = jwtToken.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
        var confirm = jwtToken.Claims.FirstOrDefault(c => c.Type == "confirm")?.Value;
            
        var result = _authenticationService.ConfirmRegistration(usermname, confirm);
        
        return CreateResponse(result);
    }
}
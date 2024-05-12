using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.Blog.Core.Domain;

namespace Explorer.API.Controllers;

[Route("api/users")]
public class AuthenticationController : BaseApiController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IWalletService _walletService;
    private readonly IHttpClientService _httpClientService;

    public AuthenticationController(IAuthenticationService authenticationService, IWalletService walletService, IHttpClientService httpClientService)
    {
        _authenticationService = authenticationService;
        _walletService = walletService;
        _httpClientService = httpClientService;
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
    public async Task<String> Login([FromBody] CredentialsDto credentials)
    {
        var result = _authenticationService.Login(credentials);
        if(result == null)
        {
            return null;
        }
        string uri = _httpClientService.BuildUri(Protocol.HTTP, "localhost", 8082, "token");
        string jsonContent = System.Text.Json.JsonSerializer.Serialize(result);
        var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await _httpClientService.PostAsync(uri, requestContent);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
        else
        {
            return null;
        }
    }

    [HttpPost("reset-password")]
    public ActionResult<ResetPasswordTokenDto> GenerateResetPasswordLink([FromBody] ResetPasswordEmailDto resetPasswordEmail)
    {
        var result = _authenticationService.GenerateResetPasswordToken(resetPasswordEmail);
        return CreateResponse(result);
    }

    [HttpPatch("reset-password/new")]
    public ActionResult ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
    {
        string email = ExtractPayload(resetPasswordRequestDto.Token);
        var result = _authenticationService.ResetPassword(resetPasswordRequestDto, email);
        return Ok();
    }

    private string ExtractPayload(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        // Read token without validation
        var jwtToken = tokenHandler.ReadJwtToken(token);

        // Access claims directly from the token
        var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        return emailClaim;
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

using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IAuthenticationService
{
    Result<AuthenticationTokensDto> Login(CredentialsDto credentials);
    Result<RegistrationConfirmationTokenDto> RegisterTourist(AccountRegistrationDto account);
    Result<ResetPasswordTokenDto> GenerateResetPasswordToken(ResetPasswordEmailDto resetPasswordEmailDto);
    Result ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto, string email);
    Result<string> ConfirmRegistration(string username, string claim);
}
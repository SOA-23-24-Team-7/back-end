using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System.Text;
using BC = BCrypt.Net;

namespace Explorer.Stakeholders.Core.UseCases;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IEmailSender _emailSender;

    public AuthenticationService(IUserRepository userRepository, IPersonRepository personRepository, ITokenGenerator tokenGenerator, IEmailSender emailSender)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
        _personRepository = personRepository;
        _emailSender = emailSender;
    }

    public Result<AuthenticationTokensDto> Login(CredentialsDto credentials)
    {
        var user = _userRepository.GetActiveByName(credentials.Username);
        if (user == null || !(BC.BCrypt.Verify(credentials.Password, user.Password))) return Result.Fail(FailureCode.NotFound);
        long personId;
        try
        {
            personId = _userRepository.GetPersonId(user.Id);
        }
        catch (KeyNotFoundException)
        {
            personId = 0;
        }
        return _tokenGenerator.GenerateAccessToken(user, personId);
    }

    public Result<RegistrationConfirmationTokenDto> RegisterTourist(AccountRegistrationDto account)
    {
        if (_userRepository.Exists(account.Username)) return Result.Fail(FailureCode.NonUniqueUsername);

        try
        {
            string cryptedPassword = BC.BCrypt.HashPassword(account.Password);
            var user = _userRepository.Create(new User(account.Username, cryptedPassword, UserRole.Tourist, false));
            var person = _personRepository.Create(new Person(user.Id, account.Name, account.Surname, account.Email));
            //emailsending
            string token = getRegistrationConfirmationEmail(user, person);
            var returnValue = new RegistrationConfirmationTokenDto();
            returnValue.Id = user.Id;
            returnValue.RegistrationConfirmationToken = token;
            return returnValue;
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            // There is a subtle issue here. Can you find it?
        }
    }

    public Result<ResetPasswordTokenDto> GenerateResetPasswordToken(ResetPasswordEmailDto resetPasswordEmailDto)
    {
        var email = resetPasswordEmailDto.Email;
        if (!_personRepository.ExistsByEmail(email)) return Result.Fail(FailureCode.NotFound);

        try
        {
            var user = _personRepository.GetByEmail(email);
            var passwordResetTokenResult = _tokenGenerator.GenerateResetPasswordToken(user.UserId, email);
            var passwordResetToken = passwordResetTokenResult.Value.ResetPasswordToken;
            var passwordResetLink = "http://localhost:4200/reset-password-edit?reset_password_token=" + passwordResetToken;


            string subject = "Explorer - Password reset link";

            StringBuilder body = new StringBuilder();
            body.AppendLine($"Dear {user.Name} {user.Surname},");
            body.AppendLine($"<p>Here's your password reset <a href=\"{passwordResetLink}\">link</a>.</p>");
            body.AppendLine($"<p>Or you can copy this link into your browser: <a href=\"{passwordResetLink}\">{passwordResetLink}</a></p>");
            body.AppendLine($"Best regards,<br>");
            body.AppendLine($"Explorer team");

            _emailSender.SendEmailAsync(email, subject, body.ToString());

            return passwordResetTokenResult;
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            throw;
        }
    }

    private string cryptPassword(string password)
    {
        return BC.BCrypt.HashPassword(password);
    }

    public Result ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto, string email)
    {
        var person = _personRepository.GetByEmail(email);
        var user = _userRepository.Get(person.UserId);
        string cryptedPassword = BC.BCrypt.HashPassword(resetPasswordRequestDto.NewPassword);
        user.Password = cryptedPassword;
        user = _userRepository.Update(user);
        return Result.Ok();
    }
    
    private string getRegistrationConfirmationEmail(User user, Person person)
    {
        var registrationConfirmationToken = _tokenGenerator.GenerateRegistrationConfirmationToken(user).Value.ResetPasswordToken;
        string subject = "Explorer - Registration confirmation";
        var passwordResetLink = "https://localhost:44333/api/users/confirm-registration?confirm_registration_token=" + registrationConfirmationToken;

        StringBuilder body = new StringBuilder();
        body.AppendLine($"Dear {person.Name} {person.Surname},");
        body.AppendLine($"<p>Please click the following  <a href=\"{passwordResetLink}\">link</a>.</p> to confirm your registration.");
        body.AppendLine($"<p>Or you can copy this link into your browser: <a href=\"{passwordResetLink}\">{passwordResetLink}</a></p>");
        body.AppendLine($"Best regards,<br>");
        body.AppendLine($"Explorer team");

        _emailSender.SendEmailAsync(person.Email, subject, body.ToString());

        return registrationConfirmationToken;
    }

    public Result<string> ConfirmRegistration(string username, string claim)
    {
        var user = _userRepository.GetByUsername(username);
        if(user == null || !claim.Equals("true")) { Result.Fail(FailureCode.InvalidArgument); }

        //var updatedUser = _userRepository.Create(new User(user.Username, user.Password, UserRole.Tourist, false));
        _userRepository.EnableUser(user.Id);
        return "Registration was  succsessfully confirmed!" ;
    }
}
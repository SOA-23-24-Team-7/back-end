namespace Explorer.Stakeholders.API.Dtos;

public class ResetPasswordRequestDto
{
    public string Token { get; set; }
    public string NewPassword { get; set; }
}

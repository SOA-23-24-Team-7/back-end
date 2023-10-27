namespace Explorer.Stakeholders.API.Dtos;

public class ClubInvitationCreatedDto : ClubInvitationDto
{
    public long Id { get; set; }
    public DateTime TimeCreated { get; set; }
    public string? Status { get; set; }
}

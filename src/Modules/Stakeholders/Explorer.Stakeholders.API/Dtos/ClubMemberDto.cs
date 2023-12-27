namespace Explorer.Stakeholders.API.Dtos;

public class ClubMemberDto
{
    public long UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public long MembershipId { get; set; }
    public string ProfilePicture { get; set; }
}

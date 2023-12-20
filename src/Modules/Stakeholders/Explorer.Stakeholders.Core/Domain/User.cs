using Explorer.BuildingBlocks.Core.Domain;
using System.Xml.Linq;

namespace Explorer.Stakeholders.Core.Domain;

public class User : Entity
{
    public string Username { get; private set; }
    public string Password { get; set; }
    public UserRole Role { get; private set; }
    public string? ProfilePicture { get; private set; }
    public bool IsActive { get; set; }
    //public ICollection<Follower> Followers { get; set; } = new List<Follower>();
    //public ICollection<Follower> Following { get; set; } = new List<Follower>();

    public User(string username, string password, UserRole role, bool isActive)
    {
        Username = username;
        Password = password;
        Role = role;
        IsActive = isActive;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Invalid Surname");
    }

    public string GetPrimaryRoleName()
    {
        return Role.ToString().ToLower();
    }

    public void UpdateProfilePicture(string profilePicture)
    {
        ProfilePicture = profilePicture;
    }
}

public enum UserRole
{
    Administrator,
    Author,
    Tourist
}
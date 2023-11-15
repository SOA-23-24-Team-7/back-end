using Explorer.BuildingBlocks.Core.Domain;
using System.Net.Mail;

namespace Explorer.Stakeholders.Core.Domain.Problems;

public class Person : Entity
{
    public long UserId { get; private set; }
    public User User { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Email { get; init; }
    public string? Bio { get; private set; }
    public string? Motto { get; private set; }

    public Person(long userId, string name, string surname, string email)
    {
        UserId = userId;
        Name = name;
        Surname = surname;
        Email = email;
        Validate();
    }

    public void UpdatePerson(string name, string surname, string bio, string motto)
    {
        Name = name;
        Surname = surname;
        Bio = bio;
        Motto = motto;
        Validate();
    }

    private void Validate()
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId");
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Surname)) throw new ArgumentException("Invalid Surname");
        if (!MailAddress.TryCreate(Email, out _)) throw new ArgumentException("Invalid Email");
    }
}
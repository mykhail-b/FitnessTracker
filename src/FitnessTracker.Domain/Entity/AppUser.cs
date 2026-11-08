
namespace FitnessTracker.Domain.Entity;

public class AppUser
{
    public Guid Id { get; private set; }

    public string IdentityId { get; private set; } = null!;

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;

    public DateOnly BirthDate { get; private set; }

    private AppUser() { } // EF Core

    public AppUser(
        Guid id,
        string identityId,
        string firstName,
        string lastName,
        string email,
        DateOnly birthDate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(identityId);
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        Id = id;
        IdentityId = identityId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        BirthDate = birthDate;
    }

    public void UpdateProfile(
        string IdentityId,
        string firstName,
        string lastName,
        DateOnly birthDate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);

        if (birthDate > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Birth date cannot be in the future");

        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
    }
}
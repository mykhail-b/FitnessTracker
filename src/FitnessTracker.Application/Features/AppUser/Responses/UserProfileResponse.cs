namespace FitnessTracker.Application.Features.AppUser.Responses;

public record UserProfileResponse(
    Guid Id,
    string IdentityId,
    string FirstName,
    string LastName,
    string Email,
    DateOnly BirthDate);

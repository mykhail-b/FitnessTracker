using FitnessTracker.Application.Features.AppUser.Responses;
using MediatR;

namespace FitnessTracker.Application.Features.AppUser.Commands;

public record UpdateUserCommand(string IdentityId, string FirstName,
    string LastName,
    DateOnly BirthDate)
    : IRequest<UserProfileResponse?>;

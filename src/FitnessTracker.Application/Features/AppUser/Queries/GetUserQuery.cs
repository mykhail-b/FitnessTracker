using FitnessTracker.Application.Features.AppUser.Responses;
using MediatR;

namespace FitnessTracker.Application.Features.AppUser.Queries;

public record GetUserQuery(string IdentityId) : IRequest<UserProfileResponse?>;

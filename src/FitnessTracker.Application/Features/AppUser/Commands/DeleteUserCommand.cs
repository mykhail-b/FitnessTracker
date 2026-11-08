using MediatR;

namespace FitnessTracker.Application.Features.AppUser.Commands;

public record DeleteUserCommand(string IdentityId) : IRequest<bool>;

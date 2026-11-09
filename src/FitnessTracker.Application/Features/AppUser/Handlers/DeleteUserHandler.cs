using FitnessTracker.Application.Features.AppUser.Commands;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.AppUser.Handlers;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserProfileRepository _userProfileRepository;

    public DeleteUserHandler(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetTrackedByIdentityIdAsync(request.IdentityId, cancellationToken);
        if (user is null) return false;

        _userProfileRepository.Remove(user);
        await _userProfileRepository.SaveChangesAsync(cancellationToken);
        return true;
    }
}

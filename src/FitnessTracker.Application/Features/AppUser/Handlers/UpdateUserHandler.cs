using FitnessTracker.Application.Features.AppUser.Commands;
using FitnessTracker.Application.Features.AppUser.Responses;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.AppUser.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserProfileResponse?>
{
    private readonly IUserProfileRepository _userProfileRepository;

    public UpdateUserHandler(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task<UserProfileResponse?> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetTrackedByIdentityIdAsync(request.IdentityId, cancellationToken);
        if (user is null) return null;

        user.UpdateProfile(request.IdentityId, request.FirstName, request.LastName, request.BirthDate);
        await _userProfileRepository.SaveChangesAsync(cancellationToken);

        return new UserProfileResponse(
            user.Id,
            user.IdentityId,
            user.FirstName,
            user.LastName,
            user.Email,
            user.BirthDate);
    }
}

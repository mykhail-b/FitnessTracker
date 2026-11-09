using FitnessTracker.Application.Features.AppUser.Queries;
using FitnessTracker.Application.Features.AppUser.Responses;
using FitnessTracker.Domain.Repository;
using MediatR;

namespace FitnessTracker.Application.Features.AppUser.Handlers;

public class GetUserHandler : IRequestHandler<GetUserQuery, UserProfileResponse?>
{
    private readonly IUserProfileRepository _userProfileRepository;

    public GetUserHandler(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task<UserProfileResponse?> Handle(
        GetUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userProfileRepository.GetByIdentityIdAsync(request.IdentityId, cancellationToken);

        return user is null
            ? null
            : new UserProfileResponse(
                user.Id,
                user.IdentityId,
                user.FirstName,
                user.LastName,
                user.Email,
                user.BirthDate);
    }
}

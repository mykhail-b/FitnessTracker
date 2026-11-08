using FitnessTracker.Application.Features.AppUser.Queries;
using FitnessTracker.Application.Features.AppUser.Responses;
using FitnessTracker.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Application.Features.AppUser.Handlers;

public class GetUserHandler : IRequestHandler<GetUserQuery, UserProfileResponse?>
{
    private readonly AppDbContext _db;

    public GetUserHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<UserProfileResponse?> Handle(
        GetUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _db.UserProfiles
            .AsNoTracking()
            .SingleOrDefaultAsync(profile => profile.IdentityId == request.IdentityId, cancellationToken);

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

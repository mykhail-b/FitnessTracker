using FitnessTracker.Application.Features.AppUser.Commands;
using FitnessTracker.Application.Features.AppUser.Responses;
using FitnessTracker.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Application.Features.AppUser.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserProfileResponse?>
{
    private readonly AppDbContext _db;

    public UpdateUserHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<UserProfileResponse?> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _db.UserProfiles
            .SingleOrDefaultAsync(profile => profile.IdentityId == request.IdentityId, cancellationToken);
        if (user is null) return null;

        user.UpdateProfile(request.IdentityId, request.FirstName, request.LastName, request.BirthDate);
        await _db.SaveChangesAsync(cancellationToken);

        return new UserProfileResponse(
            user.Id,
            user.IdentityId,
            user.FirstName,
            user.LastName,
            user.Email,
            user.BirthDate);
    }
}

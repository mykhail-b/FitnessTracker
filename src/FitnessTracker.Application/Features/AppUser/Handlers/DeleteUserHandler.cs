using FitnessTracker.Application.Features.AppUser.Commands;
using FitnessTracker.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Application.Features.AppUser.Handlers;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly AppDbContext _db;

    public DeleteUserHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _db.UserProfiles
            .SingleOrDefaultAsync(profile => profile.IdentityId == request.IdentityId, cancellationToken);
        if (user is null) return false;

        _db.UserProfiles.Remove(user);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}

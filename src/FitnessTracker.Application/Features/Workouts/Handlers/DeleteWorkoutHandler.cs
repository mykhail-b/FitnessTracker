using FitnessTracker.Application.Features.Workouts.Commands;
using FitnessTracker.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Application.Features.Workouts.Handlers;

public class DeleteWorkoutHandler : IRequestHandler<DeleteWorkoutCommand, bool>
{
    private readonly AppDbContext _db;

    public DeleteWorkoutHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
    {
        var userId = await _db.UserProfiles
            .Where(profile => profile.IdentityId == request.IdentityId)
            .Select(profile => (Guid?)profile.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (userId is null) return false;

        var workout = await _db.Workouts
            .SingleOrDefaultAsync(
                item => item.Id == request.WorkoutId && item.UserId == userId,
                cancellationToken);
        if (workout is null) return false;

        _db.Workouts.Remove(workout);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
